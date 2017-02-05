using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.Models
{
    public class CommunityCampMemberRepository : ICommunityCampMemberRepository
    {
        private readonly IConfiguration _config;
        private SqlConnection con;

        public CommunityCampMemberRepository(IConfiguration config)
        {
            _config = config;
            con = new SqlConnection(_config                       
                        .GetSection("ConnectionStrings")
                        .GetSection("DefaultConnection").Value);
        }

        public void AddMember(CommunityCampJoinMember model)
        {
            con.Execute("Insert Into CommunityCampJoinMembers " + " (CommunityName, Name, Mobile, Email, Size, CreationDate) " + " Values (@CommunityName, @Name, @Mobile, @Email, @Size, GetDate())", model);
        }

        public void DeleteMember(CommunityCampJoinMember model)
        {
            con.Execute("Delete CommunityCampJoinMembers Where " + " CommunityName = @CommunityName and Name = @Name And " + "Mobile = @Mobile And Email = @Email", model);
        }

        public List<CommunityCampJoinMember> GetAll()
        {
            return con.Query<CommunityCampJoinMember>(
                "Select * From CommunityCampJoinMembers Order By Id Asc").ToList();

            
            //string sql = "Select * From CommunityCampJoinMembers Order By Id Asc";

            //return con.Query<CommunityCampJoinMember>(sql).ToList();
        }
    }
}

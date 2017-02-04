using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.Models
{
    public class TechRepository : ITechRepository
    {
        private IConfiguration _config;
        private SqlConnection db;

        public TechRepository(IConfiguration config)
        {
            _config = config;
            db = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        }

        public void AddTech(Tech model)
        {
            string sql = "Insert Into Teches (Title) Values (@Title)";
            var id = this.db.Execute(sql, model);
        }

        public List<Tech> GetTeches()
        {
            string sql = "Select Id, Title From Teches Order By Id Asc";
            return this.db.Query<Tech>(sql).ToList();
        }
    }
}

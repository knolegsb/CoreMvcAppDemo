using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.Models
{
    public interface ICommunityCampMemberRepository
    {
        List<CommunityCampJoinMember> GetAll();
        void AddMember(CommunityCampJoinMember model);
        void DeleteMember(CommunityCampJoinMember model);
    }
}

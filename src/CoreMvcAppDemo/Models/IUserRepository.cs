using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.Models
{
    public interface IUserRepository
    {
        void AddUser(string userId, string password);
        UserViewModel GetUserByUserId(string userId);
        bool IsCorrectUser(string userId, string password);
        void ModifyUser(int uid, string userId, string password);
    }
}

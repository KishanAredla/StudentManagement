using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnit.Practice.Tests.Practice
{
    public class UserManager
    {
        private readonly IUserService _userService;

        public UserManager(IUserService userService)
        {
            _userService = userService;
        }

        public string GetDisplayName(int id)
        {
            return "User: " + _userService.GetUserName(id);
        }
    }

}

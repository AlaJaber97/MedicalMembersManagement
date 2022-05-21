using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Modals.Identity
{
    public class User : IdentityUser<int>
    {
        public User()
        {

        }
        public User(string username) : base(username)
        {

        }
    }
}

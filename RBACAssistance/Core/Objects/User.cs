using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBACAssistance.Core.Objects
{
    public class User
    {
        string userName { get; set; }
        Role userRole { get; set; }

        public User()
        {

        }

        public void SetUserName(string name)
        {
            userName = name;
        }

        public void SetUserRole(Role role)
        {
            userRole = role;
        }

        public string GetUserName()
        {
            return userName;
        }

        public Role GetUserRole()
        {
            return userRole;
        }

    }
}

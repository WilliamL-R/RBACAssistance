using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBACAssistance.Core
{
    public class Role
    {
        string roleName;
        List<Resource> resourceAccess;

        public Role(string roleName)
        {
            this.roleName = roleName;
            this.resourceAccess = new List<Resource>(); ;
        }

        public void SetRoleName(string name)
        {
            roleName = name;
        }

        public void AddResourceAccess(Resource resource)
        {
            resourceAccess.Add(resource);
        }

        public string GetRoleName()
        {
            return roleName;
        }

        public List<Resource> GetResourceAccess()
        {
            return resourceAccess;
        }

    }
}
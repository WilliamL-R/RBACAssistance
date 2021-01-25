using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBACAssistance.Core
{
    public class Resource
    {
        protected string resourceName { get; set; }
        public Resource(string resourceName)
        {
            this.resourceName = resourceName;
        }

        public void SetResourceName(string name)
        {
            resourceName = name;
        }

        public string GetResourceName()
        {
            return resourceName;
        }
    }
}
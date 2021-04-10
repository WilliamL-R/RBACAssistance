using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBACAssistance.Core.Objects;

namespace RBACAssistance.Core.RoleSuggestion
{
    public class ResourceSuggestion
    {
        const int WARNING_LIMIT = 2;
        public List<string> ResourceSuggest(RoleList roleList, ResourceList resList)
        {
            List<string> overaccessedResources = new List<string>();
            List<string> sensitiveResources =new List<string>();
            List<Role> roles = roleList.GetAsList();
            List<Resource> resources = resList.GetAsList();

            foreach(Resource res in resources)
            {
                ResourceSensitivity resSens = res.GetResourceSensitivity();
                if ( resSens == ResourceSensitivity.HighlySensitive|| resSens == ResourceSensitivity.Sensitive)
                {
                    bool inList = false;
                    foreach(string name in sensitiveResources)
                    {
                        if(res.GetResourceName() == name)
                        {
                            inList = true;
                        }
                    }
                    if (inList == true)
                    {
                        continue;
                    }
                    else
                    {
                        sensitiveResources.Add(res.GetResourceName());
                    }
                }
            }
             
            //This will count the amount of roles with access to resources.
            Dictionary<string,int> resourceAccess = new Dictionary<string,int>();

            foreach(Role role in roles)
            {
                List<Resource> access = role.GetResourceAccess();
                foreach(Resource res in access)
                {
                    //Add resource to dictionary
                    if (sensitiveResources.Contains(res.GetResourceName())){
                        if (resourceAccess.ContainsKey(res.GetResourceName()))
                        {
                            resourceAccess[res.GetResourceName()] = resourceAccess[res.GetResourceName()] + 1; 
                        }
                        else
                        {
                            resourceAccess.Add(res.GetResourceName(), 1);
                        }
                    }
                }
            }

            List<string> keyList = new List<string>(resourceAccess.Keys);
            foreach (string res in keyList)
            {
                if(resourceAccess[res] >= WARNING_LIMIT)
                {
                    overaccessedResources.Add(res);
                }
            }
            return overaccessedResources;
        }
    }
}

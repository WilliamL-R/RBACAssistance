using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBACAssistance.Core.Objects;

namespace RBACAssistance.Core.RoleSuggestion
{
    public class RoleReader
    {
        public int RoleSuggest(RoleList roleList)
        {
            int accessIdenticalCount = 0;
            Dictionary<string, string> alreadyChecked = new Dictionary<string, string>();

            for (int roleIndexOne = 0; roleIndexOne < roleList.GetCount(); roleIndexOne++)
            {
                Role roleOne = roleList.ElementAt(roleIndexOne);

                for (int roleIndexTwo = 0; roleIndexTwo < roleList.GetCount(); roleIndexTwo++)
                {
                    if (roleIndexOne == roleIndexTwo)
                    {
                        continue;
                    }

                    Role roleTwo = roleList.ElementAt(roleIndexTwo);
                    string valueCheck = roleOne.GetRoleName();
                    if(roleOne.GetResourceAccess().Count == roleTwo.GetResourceAccess().Count)
                    {
                        if (alreadyChecked.ContainsKey(roleTwo.GetRoleName()))
                        {
                            if (alreadyChecked.TryGetValue(roleTwo.GetRoleName(),out valueCheck))
                            {
                                continue;
                            }
                        }
                        if (CheckResourceAccessIsSame(roleOne, roleTwo))
                        {
                            accessIdenticalCount++;
                            //This has been checked for similarities, don't add to the counter
                            alreadyChecked.Add(roleOne.GetRoleName(), roleTwo.GetRoleName());
                        }
                    }
                }
            }
            return accessIdenticalCount;
        }

        private bool CheckResourceAccessIsSame(Role roleOne, Role roleTwo)
        {
            //Check if resourceNames are the same and are the same match
            List<Resource> roleOneResources = roleOne.GetResourceAccess();
            List<Resource> roleTwoResources = roleTwo.GetResourceAccess();

            HashSet<string> resOneSet = new HashSet<string>();
            HashSet<string> resTwoSet = new HashSet<string>();

            foreach(Resource res in roleOneResources)
            {
                resOneSet.Add(res.GetResourceName());
            }
            foreach(Resource res in roleTwoResources)
            {
                resTwoSet.Add(res.GetResourceName());
            }

            bool existsCheck = resOneSet.SetEquals(resTwoSet);

            Console.WriteLine(resOneSet.Count);
            Console.WriteLine(resTwoSet.Count);


            return existsCheck;
        }
    }
}

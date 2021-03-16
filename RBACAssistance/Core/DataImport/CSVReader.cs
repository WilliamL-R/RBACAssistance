using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RBACAssistance.Core.Objects;

namespace RBACAssistance.Core.DataImport
{
    public class CSVReader
    {
        public object[] BindDataCSV(string filePath, RoleList roleList, ResourceList resourceList, UserList userList)
        {
            //Check if file is being used with IOException
            string[] lines = System.IO.File.ReadAllLines(filePath);
            roleList.ClearList();
            resourceList.ClearList();
            if (lines.Length > 0)
            {
                //Data entry (don't add data tables, add roles and resources.)
                User newUser = new User();
                string userRoleName = null;
                string resourceName = null;
                Role role = null;
                for (int i = 1; i < lines.Length; i++)
                {

                    string[] dataFields = lines[i].Split(',');

                    //Prints line
                    foreach (var item in dataFields)
                    {
                        Console.WriteLine(item.ToString());
                    }

                    int columnCount = dataFields.Length;

                    for (int j = 0; j < columnCount; j++)
                    {
                        //User's Name
                        if (j == 0)
                        {
                            newUser.SetUserName(dataFields[j]);
                            continue;
                        }
                        //User's Role
                        if(j == 1)
                        {
                            userRoleName = dataFields[j];
                            continue;
                        }
                        //IsSenior
                        if(j == 2)
                        {
                            role = new Role(userRoleName, bool.Parse(dataFields[j]));
                            roleList.AddRoleToList(role);
                            newUser.SetUserRole(role);
                            userList.AddUserToList(newUser);
                            continue;
                        }

                        if(j % 2 != 0)
                        {
                            //Is a resource name
                            resourceName = dataFields[j];
                        }
                        else
                        {
                            //Resource is blank entirely, csv is empty and stop the loop.
                            if(resourceName == "")
                            {
                                break;
                            }
                            //Is resource sensitivity and we can now add it to the list.
                            Enum.TryParse(dataFields[j], out ResourceSensitivity resourceSensitivity);
                            Resource resource = new Resource(resourceName, resourceSensitivity);
                            if (resourceList.isResourceRepeated(resource))
                            {
                                //Already in the list, discard.
                                continue;
                            } 
                            resourceList.AddResourceToList(resource);
                            role.AddResourceAccess(resource);
                        }
                    }
                }
            }
            object[] listArrays = new object[] { roleList, resourceList, userList };
            return listArrays;
        }
    }
}
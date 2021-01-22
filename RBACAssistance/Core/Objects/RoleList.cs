﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBACAssistance.Core.Objects
{
    public class RoleList : IEnumerable
    {
        List<Role> roleList;
        
        public RoleList()
        {
            this.roleList = new List<Role>();
        }

        public void AddRoleToList(Role role)
        {
            roleList.Add(role);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Role val in roleList)
            {
                yield return val;
            }
        }

        public Role ElementAt(int index)
        {
            Role item = roleList.ElementAt<Role>(index);
            return item;
        }

        public int GetCount()
        {
            return roleList.Count;
        }
    }
}

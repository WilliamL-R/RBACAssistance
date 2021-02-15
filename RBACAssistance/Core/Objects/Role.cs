﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBACAssistance.Core.Objects;

namespace RBACAssistance.Core
{
    public class Role
    {
        string roleName { get; set; }
        List<Resource> resourceAccess { get; set; }
        bool isSenior { get; set; }

        public Role(string roleName, bool isSenior)
        {
            this.roleName = roleName;
            this.resourceAccess = new List<Resource>(); ;
            this.isSenior = isSenior;
        }

        public bool CheckRoleAccess(Resource chosenResource)
        {
            var resSen = chosenResource.GetResourceSensitivity();
            if (resSen == ResourceSensitivity.HighlySensitive || resSen == ResourceSensitivity.Sensitive)
            {
                //Warn user of sensitivity issues.
                return true;
            }

            return false;
        }

        #region Accessors
        public void SetRoleName(string name)
        {
            roleName = name;
        }

        public void AddResourceAccess(Resource resource)
        {
            resourceAccess.Add(resource);
        }

        public void AddIsSenior(bool senior)
        {
            isSenior = senior;
        }

        public string GetRoleName()
        {
            return roleName;
        }

        public List<Resource> GetResourceAccess()
        {
            return resourceAccess;
        }

        public bool GetIsSenior()
        {
            return isSenior;
        }
        #endregion

    }
}
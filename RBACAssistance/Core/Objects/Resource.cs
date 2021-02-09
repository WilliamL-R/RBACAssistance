﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBACAssistance.Core.Objects;
namespace RBACAssistance.Core
{
    public class Resource
    {
        protected string resourceName { get; set; }
        protected ResourceSensitivity resourceSensitivty { get; set; }

        public Resource(string resourceName, ResourceSensitivity resourceSensitivity)
        {
            this.resourceName = resourceName;
            this.resourceSensitivty = resourceSensitivity;
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
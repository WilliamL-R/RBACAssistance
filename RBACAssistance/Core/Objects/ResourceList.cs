using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBACAssistance.Core.Objects
{
    public class ResourceList : IEnumerable
    {
        List<Resource> resourceList;

        public ResourceList()
        {
            this.resourceList = new List<Resource>();
        }
        public List<Resource> GetAsList()
        {
            return resourceList;
        }
        public void AddResourceToList(Resource resource)
        {
            resourceList.Add(resource);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Resource val in resourceList)
            {
                yield return val;
            }
        }
        public Resource ElementAt(int index)
        {
            try
            {
                Resource item = resourceList.ElementAt<Resource>(index);
                return item;
            }catch(ArgumentOutOfRangeException e)
            {
                return null;
            }
        }

        public bool isResourceRepeated(Resource resource)
        {
            foreach (Resource item in resourceList)
            {                
                if (item.GetResourceName() == resource.GetResourceName())
                    return true;
            }
            return false;
        }

        public int GetCount()
        {
            return resourceList.Count;
        }

        public void ClearList()
        {
            resourceList.Clear();
        }
    }
}

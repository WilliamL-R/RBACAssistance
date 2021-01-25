using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using RBACAssistance.Core.Objects;

namespace RBACAssistance.Core.XML
{
    public class XMLWriter
    {

        public void WriteXML(RoleList rol, ResourceList rel)
        {
            List<Role> roleList =  rol.GetAsList();
            List<Resource> resourceList = rel.GetAsList();
            var xmlFromLINQ = new XElement("Roles",
                from r in roleList
                select new XElement("Role",
                new XElement("RoleName", r.GetRoleName()),
                new XElement("RoleAccess", 
                    from res in r.GetResourceAccess()
                    select new XElement("ResourceAccess",
                    new XElement("ResourceName", res.GetResourceName())))
                ));

            using (StringWriter sw = new StringWriter())
            {
                xmlFromLINQ.Save(sw);
                Console.WriteLine(sw.ToString());
            }
        }
    }
}

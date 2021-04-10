using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;
using RBACAssistance.Core.Objects;
using Microsoft.VisualStudio.GraphModel;
namespace RBACAssistance.Core.XML
{
    public class XMLWriter
    {

        public void WriteXML(RoleList rol, ResourceList rel)
        {
            List<Role> roleList =  rol.GetAsList();
            List<Resource> resourceList = rel.GetAsList();
            var xmlFromLINQ = new XElement("Root",
                from r in roleList
                select new XElement("Role",
                new XElement("RoleName", r.GetRoleName()),
                new XElement("RoleAccess", 
                    from res in r.GetResourceAccess()
                    select new XElement("ResourceName", res.GetResourceName()))
                ),
                from resl in resourceList
                select new XElement("Resource",
                new XElement("ResourceName", resl.GetResourceName())
                ));
            
            using (StringWriter sw = new StringWriter())
            {
                string fileName = "RBACDoc.xml";
                string path = Path.Combine(Environment.CurrentDirectory, fileName);
                xmlFromLINQ.Save(path);
                Console.WriteLine(path.ToString());
            }
        }
        public void WriteDGML(RoleList rol, ResourceList rel)
        {
            List<Role> roleList = rol.GetAsList();
            List<Resource> resourceList = rel.GetAsList();
            Dictionary<Role, List<Resource>> roleLinks = new Dictionary<Role, List<Resource>>();

            foreach (Role r in roleList)
            {
                roleLinks.Add(r, r.GetResourceAccess());
            }

            XNamespace ns = "http://schemas.microsoft.com/vs/2009/dgml";
            var root = new XElement(ns + "DirectedGraph", new XAttribute("xmlns", "http://schemas.microsoft.com/vs/2009/dgml"),
                new XElement(ns +"Nodes",
                from r in roleList
                select new XElement( ns + "Node", new XAttribute("Id", r.GetRoleName()), new XAttribute("Label", r.GetRoleName()), new XAttribute("Category", "Role")),
                from res in resourceList
                select new XElement(ns + "Node", new XAttribute("Id", res.GetResourceName()), new XAttribute("Label", res.GetResourceName()), new XAttribute("Category", "Resource"))),
                new XElement(ns +"Links"),
                new XElement( ns + "Categories",
                    new XElement(ns + "Category", new XAttribute("Id", "Role"), new XAttribute("Background", "Orange")),
                    new XElement(ns + "Category", new XAttribute("Id", "Resource"), new XAttribute("Background", "Yellow"))
                ));

            //IEnumerable<XElement> linksElement = root.DescendantsAndSelf();
            XElement linkElement = root.Element(ns + "Links");
            Console.WriteLine(linkElement.ToString());
            foreach (var roleKey in roleLinks.Keys)
            {
                List<Resource> roleAccess = roleLinks[roleKey];
                foreach (Resource res in roleAccess)
                {
                    linkElement.Add(new XElement(ns + "Link", new XAttribute("Source", roleKey.GetRoleName()), new XAttribute("Target", res.GetResourceName())));
                }
            };

            root.Add(new XAttribute("Title", "RBACModel"));
            root.Add(new XAttribute("Background", "White"));
            string fileName = "RBACDoc.dgml";

            using (StringWriter sw = new StringWriter())
            {
                string path = Path.Combine(Environment.CurrentDirectory, fileName);
                root.Save(path);
                Console.WriteLine(path.ToString());
            }

            string testCMD = String.Format("dgmlImage {0}", fileName);
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

          //  cmd.StandardInput.WriteLine(@"cd ..\..\..\packages\DgmlImage.1.2.0.1\tools");
            cmd.StandardInput.WriteLine(testCMD);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
    }
}

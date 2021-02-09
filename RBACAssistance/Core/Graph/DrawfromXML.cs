using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using RBACAssistance.Core;

namespace RBACAssistance.Core.Graph
{
    class DrawfromXML
    {
        /* 
    public struct Node
    {
        [XmlAttribute]
        public string Id;
        [XmlAttribute]
        public string Label;

        public Node(string id, string label)
        {
            this.Id = id;
            this.Label = label;
        }
    }
   */
        public struct Link
        {
            [XmlAttribute]
            public string Source;
            [XmlAttribute]
            public string Target;
            [XmlAttribute]
            public string Label;

            public Link(string source, string target, string label)
            {
                this.Source = source;
                this.Target = target;
                this.Label = label;
            }
        }
        public List<Role> Roles { get; protected set; }
        public List<Resource> Resource { get; protected set; }
        public List<Link> Links { get; protected set; }

        public struct Graph
        {
            public Role[] Roles;
            public Resource[] Resources;
            public Link[] Links;
        }

        public DrawfromXML()
        {
            Roles = new List<Role>();
            Resource = new List<Resource>();
            Links = new List<Link>();
        }

        public void AddNode(Role r)
        {
            this.Roles.Add(r);
        }

        public void AddLink(Link l)
        {
            this.Links.Add(l);
        }

        public void AddResource(Resource res)
        {
            this.Resource.Add(res);
        }

        public void Serialize(string xmlpath)
        {
            Graph g = new Graph();
            g.Roles = this.Roles.ToArray();
            g.Resources = this.Resource.ToArray();
            g.Links = this.Links.ToArray();


            XmlRootAttribute root = new XmlRootAttribute("DirectedGraph");
            root.Namespace = "http://schemas.microsoft.com/vs/2009/dgml";
            XmlSerializer serializer = new XmlSerializer(typeof(Graph), root);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter xmlWriter = XmlWriter.Create(xmlpath, settings))
            {
                serializer.Serialize(xmlWriter, g);
            }
        }
    }
}

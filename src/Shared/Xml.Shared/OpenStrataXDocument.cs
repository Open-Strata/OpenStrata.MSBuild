using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace OpenStrata.Xml
{
    public abstract class OpenStrataXDocument : XDocument
    {



        public abstract string GetDefaultXmlnsOnCreate { get; }
        public abstract string RootNodeName { get; }

        public virtual string OpenStrataXmlns => "http://schema.openstrata.org/xml/2021/10";
        public virtual string OpenStrataXmlnsPrefix => "os1";

        protected virtual string XsiXmlns => "https://www.w3.org/2001/XMLSchema-instance";
        protected virtual string XsiXmlnsPrefix => "xsi";


        protected virtual string XsdXmlns => "https://www.w3.org/2001/XMLSchema";
        protected virtual string XsdXmlnsPrefix => "xsd";

        protected virtual bool UseXsiXmlns => false;
        protected virtual bool UseXsdXmlns => false;
        protected virtual bool UseOpenStrataXmlns => false;

        private XNamespace defaultNameSpace;

        protected OpenStrataXDocument()
        {
        }

        public XNamespace DefaultNameSpace
        {
            get
            {
                if (defaultNameSpace == null)
                {
                    defaultNameSpace = Root.GetDefaultNamespace();
                }
                return defaultNameSpace;
            }
        }

        //private XElement _root;
        public new XElement Root
        {
            get
            {
                if (base.Root == null)
                {
                    var _ = CreateRootNode()
                                .AttachTo(this);
                }
                return base.Root;
            }
        }

        public override XmlNodeType NodeType => base.NodeType;

        public override void WriteTo(XmlWriter writer) => base.WriteTo(writer);

        protected virtual XElement CreateRootNode()
        {
            return new XElement(XName.Get(RootNodeName, GetDefaultXmlnsOnCreate))
                .AddXmlnsAttribute(XsiXmlnsPrefix, XsiXmlns, UseXsiXmlns)
                .AddXmlnsAttribute(XsdXmlnsPrefix, XsdXmlns, UseXsdXmlns)
                .AddXmlnsAttribute(OpenStrataXmlnsPrefix, OpenStrataXmlns, UseOpenStrataXmlns);
        }

        protected XName GetXname (string localName)
        {
            return DefaultNameSpace.GetName(localName);
        }

        protected XName XsiNilName()
        {
            return XName.Get("nil", XsiXmlns);
        }


        public void Save (string fileName, bool ensureDirExists = false)
        {
            //Ensuring the root node has been created....
            _ = this.Root;

            if (ensureDirExists)
            {
                 var fileinfo = new FileInfo(fileName);
                 if (!fileinfo.Directory.Exists){
                    fileinfo.Directory.Create();
                 }
            } 

            base.Save(fileName);
        }

        public  new void Save(string fileName, SaveOptions options) 
        {
            //Ensuring the root node has been created....
            _ = this.Root;
            base.Save(fileName, options);
        }



    }

}

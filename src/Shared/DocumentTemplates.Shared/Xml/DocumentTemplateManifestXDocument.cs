using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using OpenStrata.Xml;

namespace DocumentTemplates.Shared.Xml
{
    public class DocumentTemplateManifestXDocument : OpenStrataXDocument<DocumentTemplateManifestXDocument, DocumentTemplateManifestXElement>
    {
        public override string GetDefaultXmlnsOnCreate => "";
        public override string RootNodeName => "DocTemplateManifest";
        protected override DocumentTemplateManifestXElement InitRoot(XElement baseElement)
        {
            return new DocumentTemplateManifestXElement(baseElement);
        }

        public string Schema
        {
            get
            {
                return Root.Schema.Value;
            }
            set
            {
                Root.Schema.Value = value;
            }
        }

        public string EntityLogicalName
        {
            get { return Root.EntityLogicalName.Value; }
            set { Root.EntityLogicalName.Value = value; }
        }

        public int EntityObjectTypeCode
        {
            get { return int.Parse(Root.EntityObjectTypeCode.Value); }
            set { Root.EntityObjectTypeCode.Value = value.ToString(); }
        }
        public string ItemXml
        {
            get { return Root.ItemXml.Value; }
            set { Root.ItemXml.Value = value; }
        }

        public string ItemPropsXml
        {
            get { return Root.ItemPropsXml.Value; }
            set { Root.ItemPropsXml.Value = value; }
        }

    }
}

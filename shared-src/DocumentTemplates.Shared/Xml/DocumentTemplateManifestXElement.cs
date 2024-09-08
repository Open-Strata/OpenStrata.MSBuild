using System;
using System.Collections.Generic;
using System.Text;
using OpenStrata.Xml;
using System.Xml.Linq;

namespace DocumentTemplates.Shared.Xml
{
    public class DocumentTemplateManifestXElement : CustomXElement
    {
        public DocumentTemplateManifestXElement(XElement copyFrom) : base(copyFrom)
        {
        }

        public DocumentTemplateManifestXElement() : base(XName.Get("DocumentTemplateManifest", ""))
        {

        }

        private XAttribute _schema;
        public XAttribute Schema => this.LazyLoad("schema", _schema, out _schema);

        private XElement _entityLogicalName;
        public XElement EntityLogicalName => this.LazyLoad("EntityLogicalName", _entityLogicalName, out _entityLogicalName);

        private XElement _entityObjectTypeCode;
        public XElement EntityObjectTypeCode => this.LazyLoad("EntityObjectTypeCode", _entityObjectTypeCode, out _entityObjectTypeCode);

        private XElement _itemXml;
        public XElement ItemXml => this.LazyLoad("ItemXml", _itemXml, out _itemXml);

        private XElement _itemPropsXml;
        public XElement ItemPropsXml => this.LazyLoad("ItemPropsXml", _itemPropsXml, out _itemPropsXml);

    }
}

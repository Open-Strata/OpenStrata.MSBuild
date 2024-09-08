using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strati.Manifest.Xml
{
    public class DocumentTemplateXElement : CustomXElement
    {
        public DocumentTemplateXElement(XElement copyFrom) : base(copyFrom)
        {
        }

        public DocumentTemplateXElement() : base(XName.Get("DocumentTemplate", ""))
        {
        }

        private XAttribute _name;
        public XAttribute Name => this.LazyLoad("Name", _name, out _name);

    }
}
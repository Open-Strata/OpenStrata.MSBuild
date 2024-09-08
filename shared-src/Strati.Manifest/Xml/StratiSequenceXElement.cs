using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strati.Manifest.Xml
{
    public class StratiSequenceXElement : CustomXElement
    {
        public StratiSequenceXElement(XElement copyFrom) : base(copyFrom)
        {
        }

        public StratiSequenceXElement() : base(XName.Get("Strati",""))
        {
        }

        public StratiSequenceXElement(string uniqueNameValue) : this()
        {
            UniqueName.Value = uniqueNameValue;
        }

        private XAttribute _uniquename;
        public XAttribute UniqueName => this.LazyLoad("UniqueName", _uniquename, out _uniquename);

    }
}

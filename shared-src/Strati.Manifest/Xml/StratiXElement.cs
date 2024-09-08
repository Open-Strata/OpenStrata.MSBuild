using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strati.Manifest.Xml
{
    public class StratiXElement : CustomXElement
    {
        public StratiXElement(XElement copyFrom) : base(copyFrom)
        {
        }

        public StratiXElement() : base(XName.Get("Strati", ""))
        {
        }

        private XAttribute _packageId;
        public XAttribute PackageId => this.LazyLoad("PackageId", _packageId, out _packageId);

        private XAttribute _version;
        public XAttribute Version => this.LazyLoad("Version", _version, out _version);

        private XAttribute _uniquename;
        public XAttribute UniqueName => this.LazyLoad("UniqueName", _uniquename, out _uniquename);

    }
}

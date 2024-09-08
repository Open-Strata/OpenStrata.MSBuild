using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strata.Manifest.Xml
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

        private GitInfoXElement _gitinfo;
        public GitInfoXElement GitInfo => this.LazyLoad("GitInfo", _gitinfo, out _gitinfo);

    }
}

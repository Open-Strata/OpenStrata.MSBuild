using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strata.Manifest.Xml
{
    public class StrataManifestXElement : CustomXElement
    {
        public StrataManifestXElement(XElement copyFrom) : base(copyFrom)
        {
        }

        public StrataManifestXElement() : base(XName.Get("StrataManifest",""))
        {
        }

        private XAttribute _manifesttype;
        public XAttribute ManifestType => this.LazyLoad("Type", _manifesttype, out _manifesttype);

        private XAttribute _msbuildversion;
        public XAttribute MSBuildVersion => this.LazyLoad("MSBuildVersion", _msbuildversion, out _msbuildversion);

        private XElement _strata;
        public XElement Strata => this.LazyLoadRetypeChildren<StratiXElement>("Strata", XName.Get("Strati", ""), _strata, out _strata);

    }
}

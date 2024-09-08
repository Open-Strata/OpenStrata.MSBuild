using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strati.Manifest.Xml
{
    public class ImportStrataManifestXElement : CustomXElement
    {
        public ImportStrataManifestXElement(XElement copyFrom) : base(copyFrom)
        {
        }

        public ImportStrataManifestXElement() : base(XName.Get("ImportStrataManifest", ""))
        {
        }

        private XElement _importStrata;
        public XElement ImportStrata => this.LazyLoadRetypeChildren<StratiManifestXElement>("ImportStrata", XName.Get("StratiManifest", ""), _importStrata, out _importStrata);

        private XElement _stratisequence;
        public XElement StratiSequence => this.LazyLoadRetypeChildren<StratiSequenceXElement>("StratiSequence", XName.Get("Strati", ""), _stratisequence, out _stratisequence);

    }
}

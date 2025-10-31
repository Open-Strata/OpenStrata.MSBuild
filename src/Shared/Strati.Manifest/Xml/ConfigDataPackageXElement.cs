using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strati.Manifest.Xml
{
    public class ConfigDataPackageXElement : CustomXElement
    {
        public ConfigDataPackageXElement(XElement copyFrom) : base(copyFrom)
        {
        }

        public ConfigDataPackageXElement() : base(XName.Get("ConfigDataPackage", ""))
        {
        }

        private XAttribute _configdatapackagefilename;
        public XAttribute FileName => this.LazyLoad("FileName", _configdatapackagefilename, out _configdatapackagefilename);

        private XAttribute _localImportSequence;
        public XAttribute LocalImportSequence => this.LazyLoad("LocalImportSequence", _localImportSequence, out _localImportSequence);

        private XAttribute _configDataHashFile;
        public XAttribute ConfigDataHashFile => this.LazyLoad("ConfigDataHashFile", _configDataHashFile, out _configDataHashFile);


    }
}

using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strati.Manifest.Xml
{
    public class DataverseSolutionXElement : CustomXElement
    {
        public DataverseSolutionXElement(XElement copyFrom) : base(copyFrom)
        {
        }

        public DataverseSolutionXElement() : base(XName.Get("DataverseSolutionFile", ""))
        {
        }

        private XAttribute _uniquename;
        public XAttribute UniqueName => this.LazyLoad("UniqueName", _uniquename, out _uniquename, false);

        private XAttribute _solutionpackagefilename;
        public XAttribute SolutionPackageFileName => this.LazyLoad("SolutionPackageFilename", _solutionpackagefilename, out _solutionpackagefilename);

        private XAttribute _localImportSequence;
        public XAttribute LocalImportSequence => this.LazyLoad("LocalImportSequence", _localImportSequence, out _localImportSequence);

        private XAttribute _overwriteUnmanaged;
        public XAttribute OverwriteUnmanaged => this.LazyLoad("overwriteunmanagedcustomizations", _overwriteUnmanaged, out _overwriteUnmanaged);

        private XAttribute _publishAndActivate;
        public XAttribute PublishAndActivate => this.LazyLoad("publishworkflowsandactivateplugins", _publishAndActivate, out _publishAndActivate);


    }
}

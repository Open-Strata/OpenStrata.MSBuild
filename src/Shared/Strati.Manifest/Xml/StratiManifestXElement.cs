using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strati.Manifest.Xml
{
    public class StratiManifestXElement : CustomXElement
    {
        public StratiManifestXElement(XElement copyFrom) : base(copyFrom)
        {
        }

        public StratiManifestXElement() : base(XName.Get("StratiManifest",""))
        {
        }

        private XAttribute _packageId;
        public XAttribute PackageId => this.LazyLoad("PackageId", _packageId, out _packageId);

        private XAttribute _version;
        public XAttribute Version => this.LazyLoad("Version", _version, out _version);

        private XAttribute _uniquename;
        public XAttribute UniqueName => this.LazyLoad("UniqueName", _uniquename, out _uniquename);

        //private XAttribute _overwriteUnmanaged;
        //public XAttribute OverwriteUnmanaged => this.LazyLoad("overwriteunmanagedcustomizations", _overwriteUnmanaged, out _overwriteUnmanaged);

        //private XAttribute _publishAndActivate;
        //public XAttribute PublishAndActivate => this.LazyLoad("publishworkflowsandactivateplugins", _publishAndActivate, out _publishAndActivate);

        private XElement _dataverseSolutions;
        public XElement DataverseSolutions => this.LazyLoadRetypeChildren<DataverseSolutionXElement>("DataverseSolutions", XName.Get("DataverseSolution", ""), _dataverseSolutions, out _dataverseSolutions);

        private XElement _configdatapackages;
        public XElement ConfigDataPackages => this.LazyLoadRetypeChildren<ConfigDataPackageXElement>("ConfigDataPackages", XName.Get("ConfigDataPackage", ""), _configdatapackages, out _configdatapackages);

        private XElement _deploymentExtensions;
        public XElement DeploymentExtensions => this.LazyLoadRetypeChildren<DeploymentExtensionXElement>("DeploymentExtensions", XName.Get("DeploymentExtension", ""), _deploymentExtensions, out _deploymentExtensions);

        private XElement _powerPagesSites;
        public XElement PowerPagesSites => this.LazyLoadRetypeChildren<PowerPagesXElement>("PowerPagesSites", XName.Get("PowerPagesSite", ""), _powerPagesSites, out _powerPagesSites);

        private XElement _documentTemplates;
        public XElement DocumentTemplates => this.LazyLoadRetypeChildren<DocumentTemplateXElement>("DocumentTemplates", XName.Get("DocumentTemplate", ""), _documentTemplates, out _documentTemplates);


        private XElement _strata;
        public XElement Strata => this.LazyLoadRetypeChildren<StratiXElement>("Strata", XName.Get("Strati", ""), _strata, out _strata);




    }
}

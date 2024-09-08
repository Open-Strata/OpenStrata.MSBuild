using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strati.Manifest.Xml
{
    public class StratiManifestXDocument : OpenStrataXDocument<StratiManifestXDocument, StratiManifestXElement>
    {
        public override string GetDefaultXmlnsOnCreate => "";

        public override string RootNodeName => "StratiManifest";

        public StratiManifestXDocument()
        {
            _ = DataverseSolutions;
            _ = ConfigDataPackages;
            _ = DeploymentExtensions;
        }

        protected override StratiManifestXElement InitRoot(XElement baseElement)
        {
            return new StratiManifestXElement(baseElement);
        }

        //private bool _rootRetyped = false;
        //public new StratiManifestXElement Root
        //{
        //    get
        //    {
        //        if(!_rootRetyped)
        //        {
        //            _ =  new StratiManifestXElement(base.Root);
        //            _rootRetyped = true;
        //            // base.Root.ReplaceWith(_root);
        //            _ = DataverseSolutions;
        //            _ = ConfigDataPackages;
        //            _ = DeploymentExtensions;
        //        }
        //        return (StratiManifestXElement)base.Root;;
        //    }
        //}


        public XAttribute PackageId => Root.PackageId;

        public XAttribute Version => Root.Version;

        public XAttribute UniqueName => Root.UniqueName;

        //public XAttribute OverwriteUnmanaged => Root.OverwriteUnmanaged;

        //public XAttribute PublishAndActivate => Root.PublishAndActivate;

        public XElement DataverseSolutions => Root.DataverseSolutions;

        public XElement ConfigDataPackages => Root.ConfigDataPackages;

        public XElement DeploymentExtensions => Root.DeploymentExtensions;

        public XElement PowerPageSites => Root.PowerPagesSites;

        public XElement DocumentTemplates => Root.DocumentTemplates;


        public XElement Strata => Root.Strata;



    }
}


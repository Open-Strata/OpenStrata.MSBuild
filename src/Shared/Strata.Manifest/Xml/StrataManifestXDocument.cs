using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strata.Manifest.Xml
{
    public class StrataManifestXDocument : OpenStrataXDocument<StrataManifestXDocument, StrataManifestXElement>
    {
        public override string GetDefaultXmlnsOnCreate => "";

        public override string RootNodeName => "StrataManifest";

        public StrataManifestXDocument()
        {
        }

        protected override StrataManifestXElement InitRoot(XElement baseElement)
        {
            return new StrataManifestXElement(baseElement);
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

        public XElement Strata => Root.Strata;

    }
}


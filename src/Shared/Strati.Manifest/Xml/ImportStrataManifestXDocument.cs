using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strati.Manifest.Xml
{
    public class ImportStrataManifestXDocument : OpenStrataXDocument<ImportStrataManifestXDocument, ImportStrataManifestXElement>
    {
        public override string GetDefaultXmlnsOnCreate => "";

        public override string RootNodeName => "ImportStrataManifest";

        //private ImportStrataManifestXElement _root;
        //public new ImportStrataManifestXElement Root
        //{
        //    get
        //    {
        //        if (_root == null)
        //        {
        //            _root = new ImportStrataManifestXElement(base.Root);
        //           // this.ReplaceNodes(_root);
        //        }
        //        return _root;
        //    }
        //}

        protected override ImportStrataManifestXElement InitRoot(XElement baseElement)
        {
            return new ImportStrataManifestXElement(baseElement);
        }

        public XElement ImportStrata => Root.ImportStrata;

        public XElement StratiSequence => Root.StratiSequence;


    }
}

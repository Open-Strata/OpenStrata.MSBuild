using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

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



        public List<StratiSequenceXElement> GetStratiSeqence()
        {
            return StratiSequence.Elements(XName.Get("Strati"))
                .Select(e => (StratiSequenceXElement)e)
                .ToList();
        }

        public List<DataverseSolutionXElement> GetDataverseSolutionFileByUniqueName(string uniqueName)
        {
            return ImportStrata.XPathSelectElements($"StratiManifest/DataverseSolutions/DataverseSolutionFile[@UniqueName='{uniqueName}']")
                .Select(e => (DataverseSolutionXElement)e)
                .ToList();
        }


    }
}

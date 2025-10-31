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

            var stratiSequence = StratiSequence.Elements("Strati");

            var result = new List<StratiSequenceXElement>();

            foreach (var element in stratiSequence)
            {
                result.Add(new StratiSequenceXElement(element));
            }

            return result;

        }

        public List<DataverseSolutionXElement> GetDataverseSolutionFileByUniqueName(string uniqueName)
        {
            var solutionfiles = ImportStrata.XPathSelectElements($"StratiManifest/DataverseSolutions/DataverseSolutionFile[@UniqueName='{uniqueName}']");

            var result = new List<DataverseSolutionXElement>();

            foreach (var element in solutionfiles)
            {
                result.Add(new DataverseSolutionXElement(element));
            }

            return result;
        }


    }
}

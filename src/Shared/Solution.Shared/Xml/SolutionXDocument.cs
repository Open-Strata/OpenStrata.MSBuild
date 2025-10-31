using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace OpenStrata.Solution.Xml
{
    public class SolutionXDocument : MetadataXmlBase<SolutionXDocument>
    {

        public override string RootNodeName => "ImportExportXml";


        private SolutionManifestXElement _solutionManifest;
        public SolutionManifestXElement SolutionManifest 
                => Root.LazyLoad("SolutionManifest", _solutionManifest, out _solutionManifest);

        //private XAttribute _version;
        //public XAttribute Version => Root.LazyLoad("version", _version, out _version);

        private XAttribute _solutionpackageversion;
        public XAttribute SolutionPackageVersion => Root.LazyLoad("SolutionPackageVersion", _solutionpackageversion, out _solutionpackageversion);

        private XAttribute _languagecode;
        public XAttribute LanguageCode => Root.LazyLoad("languagecode", _languagecode, out _languagecode);

        private XAttribute _generatedBy;
        public XAttribute GeneratedBy => Root.LazyLoad("generatedBy", _generatedBy, out _generatedBy);

        public XElement MissingDependencies => SolutionManifest.MissingDependencies;

        public override string PathFromRoot => "Other\\Solution.xml";

        public class SolutionManifestXElement : CustomXElement
        {
            public SolutionManifestXElement() : base(XName.Get("SolutionManifest", ""))
            {
            }

            private XElement _uniquename;
            public XElement UniqueName => this.LazyLoad("UniqueName", _uniquename, out _uniquename);

            private XElement _version;
            public XElement Version => this.LazyLoad("Version", _version, out _version);

            private XElement _managed;
            public XElement Managed => this.LazyLoad("Managed", _managed, out _managed);

            private XElement _publisher;
            public XElement Publisher => this.LazyLoad("Publisher", _publisher, out _publisher);

            private XElement _missingDependencies;
            public XElement MissingDependencies => this.LazyLoad("MissingDependencies", _publisher, out _publisher);


            public bool ReplacePublisherNodeWithPublisherXmlFile (string publisherXmlPath, out string message, out Exception exception)
            {

                exception = null;
                message = null;

                try
                {

                    if (File.Exists(publisherXmlPath))
                    {
                        var publisherXDoc = XDocument.Load(publisherXmlPath);
                        if (publisherXDoc.Root.Name.LocalName != "Publisher")
                        {
                            message = $"Publisher XML file {publisherXmlPath} is not a valid Publisher XML document.  Publisher node will not be replaced.";
                            return false;
                        }

                        this.Publisher.ReplaceWith(publisherXDoc.Root);

                        return true;

                    }
                    else
                    {
                        message = $"Publisher XML file {publisherXmlPath} cannot be found.  Publisher node will not be replaced.";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    exception = ex;
                    message = ex.Message;

                    return false;

                }
            }




        }

        public static SolutionXDocument LoadFromRootPath(string rootPath)
        {
            var docPath = Path.Combine(rootPath, "Other", "Solution.xml");
            return SolutionXDocument.Load(docPath);
        }


    }

}

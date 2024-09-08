using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Deployment.Shared.Xml
{
    public class ImportConfigXDocument : OpenStrataXDocument<ImportConfigXDocument>
    {
        public override string GetDefaultXmlnsOnCreate => "";

        protected override bool UseXsdXmlns => true;
        protected override bool UseXsiXmlns => true;

        public override string RootNodeName => "configdatastorage";
        public static string installsampledata = "installsampledata";

        public static string waitforsampledatatoinstall = "waitforsampledatatoinstall";
        public static string agentdesktopzipfile = "agentdesktopzipfile";
        public static string agentdesktopexename = "agentdesktopexename";
        public static string crmmigdataimportfile = "crmmigdataimportfile";

        public static string solutions = "solutions";
        public static string configsolutionfile = "configsolutionfile";
        public static string solutionpackagefilename = "solutionpackagefilename";
        public static string overwriteunmanagedcustomizations = "overwriteunmanagedcustomizations";
        public static string publishworkflowsandactivateplugins = "publishworkflowsandactivateplugins";

        public static string filestoimport = "filestoimport";
        public static string filename = "filename";
        public static string filetype = "filetype";
        public static string associatedmap = "associatedmap";
        public static string importtoentity = "importtoentity";
        public static string datadelimiter = "datadelimiter";
        public static string fielddelimiter = "fielddelimiter";
        public static string enableduplicatedetection = "enableduplicatedetection";
        public static string isfirstrowheader = "isfirstrowheader";
        public static string isrecordownerateam = "isrecordownerateam";
        public static string owneruser = "owneruser";
        public static string waitforimporttocomplete = "waitforimporttocomplete";


        public static string configimportfile = "configimportfile";
        public static string zipimportdetails = "zipimportdetails";
        public static string zipimportdetail = "zipimportdetail";

        public static string filesmapstoimport = "filesmapstoimport";
        public static string configimportmapfile = "configimportmapfile";

        private XElement _solutions;
        public XElement Solutions => Root.LazyLoad(solutions, _solutions, out _solutions);

        private XElement _filesToImport;
        public XElement FilesToImport => Root.LazyLoad(filestoimport, _filesToImport, out _filesToImport);

        private XElement _filesMapsToImport;
        public XElement FilesMapsToImport => Root.LazyLoad(filesmapstoimport, _filesMapsToImport, out _filesMapsToImport);

        private XAttribute _configDataFile;
        public XAttribute ConfigDataFile => Root.LazyLoad(crmmigdataimportfile, _configDataFile, out _configDataFile);

        private XAttribute _installsampledata;
        public XAttribute InstallSampleData => Root.LazyLoad(installsampledata, _installsampledata, out _installsampledata, false);

        private XAttribute _waitforsampledatatoinstall;
        public XAttribute WaitForSampleDataToInstall => Root.LazyLoad(waitforsampledatatoinstall, _waitforsampledatatoinstall, out _waitforsampledatatoinstall);

        private XAttribute _agentdesktopzipfile;
        public XAttribute AgentDesktopZipFile => Root.LazyLoad(agentdesktopzipfile, _agentdesktopzipfile, out _agentdesktopzipfile);

        private XAttribute _agentdesktopexename;
        public XAttribute AgentDesktopExeName => Root.LazyLoad(agentdesktopexename, _agentdesktopexename, out _agentdesktopexename);

        public class ConfigSolutionFile : XElement
        {
            public ConfigSolutionFile(XElement other) : base(other)
            {
            }
            public ConfigSolutionFile(XElement parent, string filename, string defaultnamespace = "") : base(XName.Get(configsolutionfile, defaultnamespace))
            {
                SolutionPackageFileName.Value = filename;
                _ = this.AttachTo(parent);
            }

            private XAttribute _solutionpackagefilename;
            public XAttribute SolutionPackageFileName => this.LazyLoad(solutionpackagefilename, _solutionpackagefilename, out _solutionpackagefilename);

            private XAttribute _overwriteUnmanaged;
            public XAttribute OverwriteUnmanaged => this.LazyLoad(overwriteunmanagedcustomizations, _overwriteUnmanaged, out _overwriteUnmanaged);

            private XAttribute _publishAndActivate;
            public XAttribute PublishAndActivate => this.LazyLoad(publishworkflowsandactivateplugins, _publishAndActivate, out _publishAndActivate);

        }

        public class ConfigImportFile : XElement
        {
            public ConfigImportFile(XElement other) : base(other)
            {
            }

            public ConfigImportFile(XElement parent, string fileName, string defaultnamespace = "") : base(XName.Get(configimportfile,defaultnamespace))
            {
                FileName.Value = filename;
                _ = this.AttachTo(parent);
            }

            private XAttribute _filename;
            public XAttribute FileName => this.LazyLoad(filename, _filename, out _filename);

            private XAttribute _filetype;
            public XAttribute Filetype => this.LazyLoad(filetype, _filetype, out _filetype);

            private XAttribute _associatedmap;
            public XAttribute AssociatedMap => this.LazyLoad(associatedmap, _associatedmap, out _associatedmap);
            
            private XAttribute _importtoentity;
            public XAttribute ImportToEntity => this.LazyLoad(importtoentity, _importtoentity, out _importtoentity);

            private XAttribute _datadelimiter;
            public XAttribute DataDelimiter => this.LazyLoad(datadelimiter, _datadelimiter, out _datadelimiter);

            private XAttribute _fielddelimiter;
            public XAttribute FieldDelimiter => this.LazyLoad(fielddelimiter, _fielddelimiter, out _fielddelimiter);

            private XAttribute _enableduplicatedetection;
            public XAttribute EnableDuplicateDetection => this.LazyLoad(enableduplicatedetection, _enableduplicatedetection, out _enableduplicatedetection);

            private XAttribute _isfirstrowheader;
            public XAttribute IsFirstRowHeader => this.LazyLoad(isfirstrowheader, _isfirstrowheader, out _isfirstrowheader);

            private XAttribute _isrecordownerateam;
            public XAttribute IsRecordOwnerATeam => this.LazyLoad(isrecordownerateam, _isrecordownerateam, out _isrecordownerateam);

            private XAttribute _owneruser;
            public XAttribute OwnerUser => this.LazyLoad(owneruser, _owneruser, out _owneruser);

            private XAttribute _waitforimporttocomplete;
            public XAttribute WaitForImportToComplete => this.LazyLoad(waitforimporttocomplete, _waitforimporttocomplete, out _waitforimporttocomplete);



        }

        public class ZipImportDetail : XElement
        {
            public ZipImportDetail(XElement other) : base(other)
            {
            }
            public ZipImportDetail(XElement parent, string filename, string defaultnamespace = "") : base(XName.Get(zipimportdetail, defaultnamespace))
            {
                FileName.Value = filename;
                _ = this.AttachTo(parent);
            }

            private XAttribute _filename;
            public XAttribute FileName => this.LazyLoad(filename, _filename, out _filename);

            private XAttribute _filetype;
            public XAttribute Filetype => this.LazyLoad(filetype, _filetype, out _filetype);

            private XAttribute _importtoentity;
            public XAttribute ImportToEntity => this.LazyLoad(importtoentity, _importtoentity, out _importtoentity);
        }

        public class ConfigImportMapFile : XElement
        {
            public ConfigImportMapFile(XElement other) : base(other)
            {
            }
            public ConfigImportMapFile(XElement parent, string filename, string defaultnamespace = "") : base(XName.Get(configimportmapfile, defaultnamespace))
            {
                FileName.Value = filename;
                _ = this.AttachTo(parent);
            }

            private XAttribute _filename;
            public XAttribute FileName => this.LazyLoad(filename, _filename, out _filename);

        }
    }
}

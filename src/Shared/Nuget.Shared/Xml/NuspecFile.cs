using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using OpenStrata.Xml;


namespace OpenStrata.Nuget
{
    public class NuspecFile : OpenStrataXDocument<NuspecFile>
    {

        public const string Dependencies = "dependencies";
        public const string Group = "group";
        public const string TargetFramework = "targetFramework";
        public const string Dependency = "dependency";
        public const string References = "references";
        public const string Reference = "reference";
        public const string File = "file";
        public const string FrameworkAssemblies = "frameworkAssemblies";
        public const string FrameworkAssembly = "frameworkAssembly";
        public const string AssemblyName = "assemblyName";
        public const string Language = "language";
        public const string ContentFiles = "contentFiles";
        public const string Files = "files";
        public const string BuildAction = "buildAction";
        public const string Flatten = "flatten";
        public const string CopyToOutput = "copyToOutput";
        public const string IncludeFlags = "include";
        public const string ExcludeFlags = "exclude";
        public const string LicenseUrl = "licenseUrl";
        public const string Repository = "repository";
        public const string Icon = "icon";
        public const string Readme = "readme";
        public const string metadata = "metadata";

        public override string GetDefaultXmlnsOnCreate => "http://schemas.microsoft.com/packaging/2011/10/nuspec.xsd";

        public override string RootNodeName => "package";

        private XElement _metadataNode;
        public XElement MetadataNode => Root.LazyLoad(metadata, _metadataNode, out _metadataNode);


    }
}

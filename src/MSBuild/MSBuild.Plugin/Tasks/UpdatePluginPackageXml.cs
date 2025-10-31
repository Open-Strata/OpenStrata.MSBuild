using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using OpenStrata.Xml;
using Plugin.Shared.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace OpenStrata.MSBuild.Plugin.Tasks
{
    public class UpdatePluginPackageXml : BaseTask
    {

        [Required]
        public string XmlFilePath { get; set; }

        [Required]
        public string uniquename { get; set; }

        [Required]
        public string pluginpackageid { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string package { get; set; }

        [Required]
        public string version { get; set; }

        public string AutoUpdateVersion { get; set; } = "true";        


        public override bool ExecuteTask()
        {

            var packageXdoc = PluginPackageXDocument.Load(XmlFilePath);

            packageXdoc.uniquename.Value = uniquename;
            packageXdoc.pluginpackageid.Value = pluginpackageid;
            packageXdoc.name.Value = name;
            packageXdoc.package.Value = package;
            packageXdoc.version.Value = version;
            if (AutoUpdateVersion.AsBoolean(true)) packageXdoc.version.Value = version;

            File.WriteAllText(XmlFilePath, packageXdoc.ToString());

           // packageXdoc.Save(XmlFilePath);

            return true;
        }
    }
}

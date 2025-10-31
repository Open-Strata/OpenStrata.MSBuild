using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenStrata.Deployment.Sdk.Common.Upgrade
{
    [Export(typeof(IImportPackageStrataFeatureExtension))]
    public class UpgradeMigrationStepsFeatureExtension : PackageStrataExtensionBase, IImportPackageStrataFeatureExtension
    {
        protected override bool AppliesToThisSolution(string solutionName)
        {
            return true;
        }


        private List<string> GetApplicableConfigData(string solution)
        {

            var configDataFiles = new List<string>();

            var items = ImportStrataManifest.Root.Descendants("DataverseSolutionFile")
                                 .Where(dsf => dsf.Attribute("UniqueName").Value == solution)
                                 ?.FirstOrDefault()
                                 ?.Ancestors("StratiManifest")
                                 ?.FirstOrDefault()
                                 ?.Element("ConfigDataPackages")
                                 ?.Elements("ConfigDataPackage")
                                 ?.OrderBy(cdp => int.Parse(cdp.Attribute("LocalImportSequence").Value));

            if (items != null)
            {
                foreach (XElement configDataElement in items)
                {
                    configDataFiles.Add(configDataElement.Attribute("FileName").Value);
                }
            }

            return configDataFiles;

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStrata.Deployment.Sdk.Common.ImportConfig
{

    [Export(typeof(IImportPackageStrataFeatureExtension))]
    public class ImportConfigSettingsFeatureExtension : PackageStrataExtensionBase, IImportPackageStrataFeatureExtension
    {
        protected override bool AppliesToThisSolution(string solutionName)
        {
            return true;
        }

    }
}

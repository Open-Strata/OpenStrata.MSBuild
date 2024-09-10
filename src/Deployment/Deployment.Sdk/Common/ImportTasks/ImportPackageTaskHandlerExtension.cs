using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStrata.Deployment.Sdk.Common.ImportTasks
{
    [Export(typeof(IImportPackageStrataFeatureExtension))]
    public class ImportPackageTaskHandlerExtension : PackageStrataExtensionBase, IImportPackageStrataFeatureExtension
    {
        protected override bool AppliesToThisSolution(string solutionName)
        {
            return true;
        }
    }
}

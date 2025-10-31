using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStrata.Deployment.Sdk
{
    public class PackageStrataFeatureBase : PackageStrataExtensionBase
    {
        protected override bool AppliesToThisSolution(string solutionName)
        {
            return true;
        }
    }
}

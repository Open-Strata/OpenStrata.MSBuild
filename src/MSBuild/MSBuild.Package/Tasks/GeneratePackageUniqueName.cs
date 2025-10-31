using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using OpenStrata.Strati.Manifest;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenStrata.MSBuild.Package.Tasks
{
    public class GeneratePackageUniqueName : BaseTask
    {

        [Required] 
        public string ProjectName { get; set; }


        [Output]
        public string PackageUniqueName { get; set; }

        public override bool ExecuteTask()
        {
            PackageUniqueName = ManifestTools.GenerateManifestUniqueName(ProjectName, "package");
            return true;
        }
    }
}

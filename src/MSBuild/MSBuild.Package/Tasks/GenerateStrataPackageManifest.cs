using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using OpenStrata.Strata.Manifest;
using OpenStrata.Strata.Manifest.Xml;
using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenStrata.MSBuild.Package.Tasks
{
    public class GenerateStrataPackageManifest : BaseTask
    {

        [Required]
        public string SolutionDir { get; set; }

        public string ostrataVersion { get; set; }

        [Required]
        public ITaskItem[] PackageReferenceStrati { get; set; }

        public override bool ExecuteTask()
        {

            var strataManifest = new StrataManifestXDocument();

            var ManifestPath = Path.Combine(SolutionDir, "ostrata.package.manifest");

            //TODO:  Process Strati Dependencies

            strataManifest.Root.ManifestType.Value = ManifestType.Package.ToString();

            strataManifest.Root.MSBuildVersion.Value = ostrataVersion;

            foreach (ITaskItem item in PackageReferenceStrati)
            {
                strataManifest.ProcessStratiDependency(item);
            }

            strataManifest.Save(ManifestPath);

            return true;
        }

    }

    public static class GenerateStrataManifestExtensions
    {



        public static void ProcessStratiDependency(this StrataManifestXDocument manifest, ITaskItem dependencyItem)
        {

            var strati = new StratiXElement();

            strati.PackageId.Value = dependencyItem.ItemSpec;
            strati.Version.Value = dependencyItem.GetMetadata("Version") ?? string.Empty;

            strati.GitInfo.RepoUrl.Value = dependencyItem.GetMetadata("GitRepositoryUrl") ?? string.Empty;
            strati.GitInfo.Commit.Value = dependencyItem.GetMetadata("Commit") ?? string.Empty;
            strati.GitInfo.CommitDate.Value = dependencyItem.GetMetadata("CommitDate") ?? string.Empty;

            manifest.Strata.Add(strati);

        }

    }

}

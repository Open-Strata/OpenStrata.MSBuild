using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using OpenStrata.Strati.Manifest;
using OpenStrata.Strati.Manifest.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.MSBuild.Package.Tasks
{
    public class GenerateLocalStratiManifest : BaseTask
    {

        [Required]
        public string IntermediateOutputPath { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string ProjectVersion { get; set; }

        [Required]
        public string PackageUniqueName { get; set; }


        [Required]
        public ITaskItem[] ProjectReferenceStrati { get; set; }

        [Required]
        public ITaskItem[] PackageReferenceStrati { get; set; }

        [Output]
        public string CreatedStratiManifestPath { get; set; }



        public override bool ExecuteTask()
        {

            var stratiManifest = new StratiManifestXDocument();

            stratiManifest.Root.Add(new XAttribute("isLocal", true));

            CreatedStratiManifestPath = Path.Combine(IntermediateOutputPath, "strati.manifest");

            stratiManifest.PackageId.Value = ProjectName;
            stratiManifest.Version.Value = ProjectVersion;
            stratiManifest.UniqueName.Value = PackageUniqueName;

            //Process Project References

            foreach (ITaskItem item in ProjectReferenceStrati)
            {
                var itemType = item.GetMetadata("ProjectType")?.ToLower();

                switch (itemType?.ToLower())
                {
                    case "solution":
                        stratiManifest.ProcessSolution(item);
                        break;
                    case "configdata":
                        stratiManifest.ProcessConfigData(item);
                        break;
                    case "deployment":
                        stratiManifest.ProcessDeployment(item);
                        break;
                    case "powerpages":
                        stratiManifest.ProcessPowerPages(item);
                        break;
                    case "documenttemplates":
                        stratiManifest.ProcessDocumentTemplates(item);
                        break;
                    default:
                        Log.LogWarning($"Item {item.ItemSpec} does not have a recognized Project Type of \"{itemType}\"");
                        break;
                }
            }


            //TODO:  Process Strati Dependencies

            foreach (ITaskItem item in PackageReferenceStrati)
            {
                stratiManifest.ProcessStratiDependency(item);
            }

            stratiManifest.Save(CreatedStratiManifestPath);

            return true;
        }

    }

    public static class GenerateLocalStratiManifestExtensions
    {

        public static void ProcessSolution(this StratiManifestXDocument manifest, ITaskItem solutionItem)
        {

            var solution = new DataverseSolutionXElement();

            solution.SolutionPackageFileName.Value = Path.GetFileName(solutionItem.ItemSpec);
            solution.UniqueName.Value = solutionItem.GetMetadata("UniqueName") ?? string.Empty;
            solution.LocalImportSequence.Value = solutionItem.GetMetadata("LocalImportSequence") ?? string.Empty;

            manifest.DataverseSolutions.Add(solution);

        }

        public static void ProcessConfigData(this StratiManifestXDocument manifest, ITaskItem configDataItem)
        {

            var configdata = new ConfigDataPackageXElement();

            configdata.FileName.Value = Path.GetFileName(configDataItem.ItemSpec);
            configdata.LocalImportSequence.Value = configDataItem.GetMetadata("LocalImportSequence") ?? string.Empty;

            manifest.ConfigDataPackages.Add(configdata);

        }

        public static void ProcessDeployment(this StratiManifestXDocument manifest, ITaskItem deploymentItem)
        {
            var extension = new DeploymentExtensionXElement();

            extension.RunTimeAssembly.Value = Path.GetFileName(deploymentItem.ItemSpec);
            extension.LocalImportSequence.Value = deploymentItem.GetMetadata("LocalImportSequence") ?? string.Empty;

            manifest.DeploymentExtensions.Add(extension);
        }

        public static void ProcessStratiDependency(this StratiManifestXDocument manifest, ITaskItem dependencyItem)
        {

            var strati = new StratiXElement();

            strati.PackageId.Value = dependencyItem.ItemSpec;
            strati.Version.Value = dependencyItem.GetMetadata("Version") ?? string.Empty;
            strati.UniqueName.Value = dependencyItem.GetMetadata("UniqueName") ?? ManifestTools.GenerateManifestUniqueName(dependencyItem.ItemSpec);


            manifest.Strata.Add(strati);

        }

        public static void ProcessPowerPages(this StratiManifestXDocument manifest, ITaskItem powerpagesItem)
        {

            string deploymentYml = powerpagesItem.GetMetadata("DeploymentYml");


            if (!string.IsNullOrEmpty(deploymentYml))
            {
                var extension = new PowerPagesXElement();

                extension.ContentZipFile.Value = Path.GetFileName(powerpagesItem.ItemSpec);
                //extension.LocalImportSequence.Value = deploymentItem.GetMetadata("LocalImportSequence") ?? string.Empty;

                extension.DeploymentYml.Value = Path.GetFileName(deploymentYml);

                manifest.PowerPageSites.Add(extension);

            }

        }

        public static void ProcessDocumentTemplates(this StratiManifestXDocument manifest, ITaskItem documentTemplateItem)
        {

            var doctemplate = new DocumentTemplateXElement();

            doctemplate.Name.Value = Path.GetFileName(documentTemplateItem.ItemSpec);

            manifest.DocumentTemplates.Add(doctemplate);

        }



    }
}

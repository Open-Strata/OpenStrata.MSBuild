using Deployment.Shared.Xml;
using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using OpenStrata.Strati.Manifest.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using static Deployment.Shared.Xml.ImportConfigXDocument;

namespace OpenStrata.MSBuild.Package.Tasks
{
    public class UpdateImportConfig : BaseTask
    {

        [Required]
        public string ImportStrataManifestPath { get; set; }

        [Required]
        public string ImportConfigStartFromPath { get; set; }

        [Required]
        public string OutputDir { get; set; }

        public bool OverwriteUnmanaged { get; set; }

        public bool PublishAndActivate { get; set; }

        [Required]
        public string PackageDataFolder { get; set; }



        [Output]
        public string ImportConfigPath { get; set; }

        [Output]
        public string CalculatedAppSourceAnchorSolution { get; set; }


        public override bool ExecuteTask()
        {

            ImportConfigPath = Path.Combine(OutputDir, PackageDataFolder, "ImportConfig.xml");

            Log.LogMessage($"Updating import config: final destination is {ImportConfigPath}");

            var importConfig = File.Exists(ImportConfigStartFromPath) ? ImportConfigXDocument.Load(ImportConfigStartFromPath) : new ImportConfigXDocument();

            if (!File.Exists(ImportStrataManifestPath))
            {
                return TaskFailed($"Unable to find Import Strata Manfifest at {ImportStrataManifestPath}");
            }

            var importStratiManifest = ImportStrataManifestXDocument.Load(ImportStrataManifestPath);

            Log.LogMessage($"Loaded Import Strata Manfifest at {ImportStrataManifestPath}");

            Log.LogMessage(importStratiManifest.Root.ToString());

            var stratiSequence = importStratiManifest.StratiSequence.Elements("Strati");

            Log.LogMessage($"Import Strata Manfifest contains {stratiSequence.Count()} Strati Sequence items.");

            //Load Solutions

            foreach (XElement stratiSeq in stratiSequence.ToArray())
            {
                Log.LogMessage(stratiSeq.ToString());

                var stratiUN = stratiSeq.Attribute("UniqueName").Value;

                Log.LogMessage($"Processing strati {stratiUN}");

                var stratiManifest = importStratiManifest.ImportStrata
                       .Elements("StratiManifest")
                       .Where(sm => sm.Attribute("UniqueName").Value == stratiUN)
                       .FirstOrDefault();

                if (stratiManifest == null)
                {
                    return TaskFailed($"Did not find a strati manifest with the unique name \"{stratiUN}\"");
                }

                Log.LogMessage($"Processing strati {stratiUN}: Located strati manifest with unique name  \"{stratiUN}\"");

                XElement[] solutionFiles = stratiManifest
                                              .Element("DataverseSolutions")
                                              .Elements("DataverseSolutionFile") 
                                              ?.OrderBy(dsf => int.Parse(dsf.Attribute("LocalImportSequence").Value))
                                              ?.ToArray();

                Log.LogMessage($"Processing strati {stratiUN}: Found {solutionFiles.Length} solutions");

                foreach (XElement solutionFile in solutionFiles)
                {
                    var solutionPackageFileName = solutionFile.Attribute("SolutionPackageFilename")?.Value;

                    Log.LogMessage($"Processing strati {stratiUN}: Processing solution \"{solutionPackageFileName}\" ");

                    var cfsf = new ConfigSolutionFile(importConfig.Solutions, solutionPackageFileName);

                    cfsf.OverwriteUnmanaged.Value = OverwriteUnmanaged.ToString().ToLower();
                    cfsf.PublishAndActivate.Value = PublishAndActivate.ToString().ToLower();

                    CalculatedAppSourceAnchorSolution = solutionPackageFileName;
                }
            }

            importConfig.Save(ImportConfigPath);

            return true;

        }
    }
}

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
    public class AssignPDSolutionStratiImportOrder : BaseTask
    {

        [Required]
        public string ImportStrataManifestPath { get; set; }

        public int ImportOrderStartsWith { get; set; }  = 1;      

        public ITaskItem[] StratiSolutions { get; set; } = Array.Empty<ITaskItem>();

        [Output]
        public ITaskItem[] SolutionsImportOrder { get; set; } 

        public override bool ExecuteTask()
        {

            Log.LogMessage($"Running AssignPDSolutionStratiImportOrder");

            var solImportOrderList = new List<ITaskItem>();

            var ordernumber = ImportOrderStartsWith;

            var importStratiManifest = ImportStrataManifestXDocument.Load(ImportStrataManifestPath);

            Log.LogMessage($"Loaded Import Strata Manfifest at {ImportStrataManifestPath}");

            Log.LogMessage(importStratiManifest.Root.ToString());

            var stratiSequence = importStratiManifest.StratiSequence.Elements("Strati");

            Log.LogMessage($"Import Strata Manfifest contains {stratiSequence.Count()} Strati Sequence items.");

            foreach (XElement stratiSeq in stratiSequence.ToArray())
            {
                Log.LogMessage($"Processing strati {stratiSeq.ToString()} as sequence {ordernumber}");

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

                    Log.LogMessage($"Processing strati solution {solutionPackageFileName} as sequence {ordernumber}");

                    var pdSolutionItems = StratiSolutions
                                            .Where(i => (new FileInfo(i.ItemSpec)).Name.ToLower() == solutionPackageFileName.ToLower());

                    foreach (ITaskItem item in pdSolutionItems)
                    {
                        Log.LogMessage($"Processing itaskitem  {item.ItemSpec} as sequence {ordernumber}");
                        item.SetMetadata("ImportOrder", $"{ordernumber}");

                        solImportOrderList.Add(item);

                        ordernumber++;
                    }
                }
            }

            SolutionsImportOrder = solImportOrderList.ToArray();

            return true;

        }
    }
}

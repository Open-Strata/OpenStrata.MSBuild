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

            var ordernumber = ImportOrderStartsWith;

            var strataManifest = ImportStrataManifestXDocument.Load(ImportStrataManifestPath);

            var solImportOrderList = new List<ITaskItem>();

            foreach (StratiSequenceXElement strati in strataManifest.GetStratiSeqence())
            {
                Log.LogMessage($"Processing strati {strati.UniqueName.Value} as sequence {ordernumber}");

                foreach (DataverseSolutionXElement solution in strataManifest.GetDataverseSolutionFileByUniqueName(strati.UniqueName.Value))
                {
                    Log.LogMessage($"Processing strati solution {solution.SolutionPackageFileName.Value} as sequence {ordernumber}");

                    var pdSolutionItems = StratiSolutions
                                            .Where(i => (new FileInfo(i.ItemSpec)).Name.ToLower() == solution.SolutionPackageFileName.Value.ToLower());

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

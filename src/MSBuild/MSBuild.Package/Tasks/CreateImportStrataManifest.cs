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
    public class CreateImportStrataManifest : BaseTask
    {

        [Required]
        public string OutputDir { get; set; }

        [Required]
        public string PackageDataFolder { get; set; }


        //[Required]
        //public ITaskItem[] ProjectReferenceStrati { get; set; }

        [Required]
        public ITaskItem[] PackageReferenceStrati { get; set; }


        [Required]
        public string LocalStratiManifestPath { get; set; }

        public bool OverwriteUnmanaged { get; set; }

        public bool PublishAndActivate { get; set; }


        [Output]
        public string CreatedImportStrataManifestPath { get; set; }

        public override bool ExecuteTask()
        {
            var strataManifest = new ImportStrataManifestXDocument();

            CreatedImportStrataManifestPath = Path.Combine(OutputDir, PackageDataFolder, "importstrata.manifest");

            var CISMPFileInfo = new FileInfo(CreatedImportStrataManifestPath);

            foreach (ITaskItem item in PackageReferenceStrati)
            {

                var manifestPath = item.GetMetadata("ManifestPath");

                if (File.Exists(manifestPath))
                {

                //   stratiManifest.ProcessStratiManifest(item);

                var stratiManifest = StratiManifestXDocument.Load(manifestPath);

                //Log.LogMessage($"StratiManifest XML At {item.ItemSpec}");
                //Log.LogMessage(stratiManifest.Root.ToString());

                strataManifest.ImportStrata.Add(new StratiManifestXElement(stratiManifest.Root));

                }

            }


            if (File.Exists(LocalStratiManifestPath))
            {
                var localManifest = StratiManifestXDocument.Load(LocalStratiManifestPath);
                strataManifest.ImportStrata.Add(new StratiManifestXElement(localManifest.Root));
            }



            //Log.LogMessage($"Import Strata Manifest XML");
            //Log.LogMessage(strataManifest.Root.ToString());

            StratiSequenceFactory.Generate(strataManifest, LogMessage);

            strataManifest.Save(CreatedImportStrataManifestPath, true);

            return true;

        }


        //private void LogMessage (string msg)
        //{
        //    Log.LogMessage(msg);
        //}
       
    }

    public static class CreateImportStrataManifestExtension
    {
        public static void ProcessStratiManifest(this ImportStrataManifestXDocument manifest, ITaskItem stratiManifestItem)
        {

            var stratiManifest = StratiManifestXDocument.Load(stratiManifestItem.ItemSpec);
            manifest.ImportStrata.Add(new XElement(stratiManifest.Root));

        }
    }
}

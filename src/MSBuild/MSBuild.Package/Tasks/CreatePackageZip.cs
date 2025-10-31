using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace OpenStrata.MSBuild.Package.Tasks
{
    public class CreatePackageZip : BaseTask
    {

        [Required]
        public string OutputDir { get; set; }

        [Required]
        public string PackageDataFolder { get; set; }

        [Required]
        public ITaskItem[] RunTimeFiles { get; set; }

        public ITaskItem[] StratiPackageFiles { get; set; }

        [Required]
        public string PackageOutputPath { get; set; }

        [Required]
        public string ContentTypesXmlPath { get; set; }

        [Output]
        public string PackageZipPath { get; set; }



        public override bool ExecuteTask()
        {

            PackageZipPath = Path.Combine(PackageOutputPath, "package.zip");

            using (FileStream packageZip = File.Open(PackageZipPath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(packageZip, ZipArchiveMode.Create))
                {

                    Log.LogMessage($"Creating entry for {Path.GetFileName(ContentTypesXmlPath)} as \"[Content_Types].xml\".");

                    var contentType = archive.CreateEntryFromFile(ContentTypesXmlPath, "[Content_Types].xml" );


                    foreach (ITaskItem runTimeFile in RunTimeFiles)
                    {
                        Log.LogMessage($"Creating entry for {Path.GetFileName(runTimeFile.ItemSpec)}.");

                        archive.CreateEntryFromFile(runTimeFile.ItemSpec, Path.GetFileName(runTimeFile.ItemSpec));
                    }

                    var dataFolder = new DirectoryInfo(Path.Combine(OutputDir, PackageDataFolder));

                    AddArchiveEntriesFromDirectory(archive, dataFolder, PackageDataFolder);

                    // Added for additional flexibility to add new project types.
                    AddStratiPackageFiles(archive, StratiPackageFiles);

                }

            }

            return true;

        }

        protected void AddStratiPackageFiles(ZipArchive archive, ITaskItem[] items)
        {
            if (items == null || items.Length == 0) { return; }

            foreach (ITaskItem item in items)
            {
                string StratiType = item.GetMetadata("StratiType");
                string UniqueName = item.GetMetadata("UniqueName");

                switch ( StratiType )
                {
                    case "solution":
                    case "configdata":
                        // Do nothing as these items are already addresssed.
                        break;
                    default:
                        archive.CreateEntryFromFile(item.ItemSpec, Path.Combine(UniqueName, StratiType,Path.GetFileName(item.ItemSpec)));
                        break;
                }
            }
        }

        protected void AddArchiveEntriesFromDirectory(ZipArchive archive, DirectoryInfo directory, string archiveDir)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                Log.LogMessage($"Creating entry for {Path.Combine(archiveDir, file.Name)}.");

                archive.CreateEntryFromFile(file.FullName, Path.Combine(archiveDir, file.Name));
            }

            foreach (DirectoryInfo subDir in directory.GetDirectories())
            {
                AddArchiveEntriesFromDirectory(archive, subDir, Path.Combine (archiveDir,subDir.Name));
            }
        }

    }
}

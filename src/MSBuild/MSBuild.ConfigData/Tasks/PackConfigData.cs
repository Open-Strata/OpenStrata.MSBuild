using Microsoft.Build.Framework;
using OpenStrata.ConfigData;
using OpenStrata.MSBuild.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace OpenStrata.MSBuild.ConfigData.Tasks
{
    public class PackConfigData : BaseTask
    {

        [Required]
        public string ConfigDataRootDir { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string OutDir { get; set; }

        public ITaskItem[] CommonFieldsToRemove { get; set; }


        [Output]
        public ITaskItem[] PackedConfigDataFiles { get; set; }

        public override bool ExecuteTask()
        {

            PackedConfigDataFiles = new ITaskItem[0];

            var rootDir = new DirectoryInfo(ConfigDataRootDir);

            Log.LogMessage($"OpenStrata : Attempting to Pack ConfigData root directory {ConfigDataRootDir}");

            var packedFiles = new List<string>();

            if (!rootDir.Exists)
            {
                return TaskFinishedWithWarning($"OpenStrata : Cannot find specified ConfigData root directory {ConfigDataRootDir}");
            }

            foreach (DirectoryInfo subDir in rootDir.GetDirectories())
            {



                var dirName = subDir.Name.ToLower();

                switch (dirName.ToLower())
                {
                    case "bin":
                    case "obj":
                        Log.LogMessage($"OpenStrata : Skipping sub directory {subDir.Name}");

                        break;
                    default:

                        CleanupConfigDataFiles(subDir);

                        var subDirPart = dirName.ToLower() == "data" ? string.Empty : $".{dirName}";

                        var OutZipPath = Path.Combine(OutDir, $"{ProjectName}{subDirPart}.zip");

                        Log.LogMessage($"OpenStrata : Attempting to Pack Config Data Located at subdir {subDir.Name} into {OutZipPath}");

                        if (!ZipTools.TryPackConfigData(OutZipPath, subDir.FullName, Log.Log, out string message, out Exception ex))
                        {
                            if (ex != null)
                            {
                                return TaskFailed(ex);
                            }
                            Log.LogMessage(message);
                        }
                        else
                        {
                            Log.LogMessage($"OpenStrata : Finished packing subdir {subDir.Name} into {OutZipPath}");
                            packedFiles.Add(OutZipPath);
                        }
                        break;
                }
            }


            if (packedFiles.Count == 0)
            {
                Log.LogMessage($"OpenStrata : Was not able to find valid config data in the directories of of {ConfigDataRootDir}");
                return true;
               // return TaskFinishedWithWarning($"OpenStrata : Was not able to find valid config data in the directories of of {ConfigDataRootDir}");
            }

            PackedConfigDataFiles = new ITaskItem[packedFiles.Count];

            int i = 0;

            Log.LogMessage($"OpenStrata : Identified and created {packedFiles.Count} config data packages in root directory {ConfigDataRootDir}");

            foreach (string packedFile in packedFiles)
            {
                PackedConfigDataFiles[i] = new Microsoft.Build.Utilities.TaskItem(packedFile);
                i++;
            }

            return true;
        }


        private void CleanupConfigDataFiles(DirectoryInfo dir)
        {
            foreach (var file in dir.GetFiles())
            {
                string xpath = null;
                var dirty = false;

                if (file.Name.ToLower() == "data_schema.xml")
                {
                    xpath = "/entities/entity/fields/field[@name = '{0}']";
                }
                else if (file.Name.ToLower() == "data.xml")
                {
                    xpath = "/entities/entity/records/record/field[@name = '{0}']";
                }

                if (!string.IsNullOrEmpty(xpath))
                {
                    var xdoc = XDocument.Load(file.FullName);
                    foreach (ITaskItem i in CommonFieldsToRemove)
                    {
                        var delList = xdoc.XPathSelectElements(string.Format(xpath, i.ItemSpec));
                        foreach (var node in delList)
                        {
                            node.Remove();
                            dirty = true;
                        }
                    }
                    if (dirty) xdoc.Save(file.FullName);
                }
            }
        }


    }
}

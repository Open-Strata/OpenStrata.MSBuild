using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Framework;
using System.Text.RegularExpressions;
using System.IO;

namespace OpenStrata.MSBuild.Tasks
{
    public class FixUpMergeDiffVersionMarkup : BaseTask
    {

        static string VersionDiffRegEx = @"<<<<<<< HEAD(?<versionelement>[\s\S]*?<[Vv]ersion>[0-9\.]*<\/[Vv]ersion>[\s\S]*?)=======[\s\S]*?>>>>>>> [0-9a-z]*";
        static string VersionDiffRegExReplace = @"<<<<<<< HEAD[\s\S]*?{0}[\s\S]*?=======[\s\S]*?>>>>>>> [0-9a-z]*";

        public ITaskItem[] CommonMergeVersionDiffFiles { get; set; }

        public override bool ExecuteTask()
        {

            if (CommonMergeVersionDiffFiles != null)
            {

                foreach (var file in CommonMergeVersionDiffFiles)
                {
                    FixUpFile(file.ItemSpec);
                }

            }

            return true;
        }

        public void FixUpFile(string path)
        {

            var fi = new FileInfo(path);

            if (fi.Exists)
            {

                string readText = File.ReadAllText(path);

                RegexOptions options = RegexOptions.Multiline;

                var fileUpdated = false;

                //Match regexMatch = Regex.Match(readText, VersionDiffRegEx, options);

                // Console.WriteLine($"Regex Success:  {regexMatch.Success}");

                foreach (Match m in Regex.Matches(readText, VersionDiffRegEx, options))
                {

                    this.LogMessage($"Found version diff markup in file {path}.  Attempting to fix file.");

                    var replacementText = m.Groups["versionelement"].Value;

                    var replacementRegex = String.Format(VersionDiffRegExReplace, replacementText.Replace(".", "\\."));

                    readText = Regex.Replace(readText, replacementRegex, replacementText.Trim(new char[] { '\r', '\n' }));

                    this.LogMessage($"Removed version diff markup from file {path}.");

                    fileUpdated = true;

                }

                if (fileUpdated) File.WriteAllText(path, readText);

            }

        }
    }
}

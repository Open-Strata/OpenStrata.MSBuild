// Copyright (c) Open-Strata contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project or repository root for license information.


using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using OpenStrata.MSBuild.Tasks;
using OpenStrata.Strati.Manifest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenStrata.MSBuild.Stratify.Tasks
{
    public class GenerateBuildTransitiveProps : BaseTask
    {


        [Required]
        public string PackageId { get; set; }

        [Required]
        public string PackageVersion { get; set; }

        public string UniqueName { get; set; }

        public string GitRepositoryUrl { get; set; }

        public string GitCommit { get; set; }

        public string GitCommitDate { get; set; }

        public bool OverwriteUnmanaged { get; set; }

        public bool PublishAndActivate { get; set; }

        [Required]
        public string NuspecOutPath { get; set; }

        [Required]
        public ITaskItem[] TemplatePaths { get; set; }

        [Output]
        public ITaskItem[] CreatedPropsPaths { get; set; }

        public override bool ExecuteTask()
        {
            var cppList = new List<ITaskItem>();

            foreach (var item in TemplatePaths) {

                var TemplatePath = item.ItemSpec;

                 var propsText = File.ReadAllText(TemplatePath);

                    var fi = new FileInfo(TemplatePath);
                    //TODO:  Add processing to props if required.

                var CreatedPropsPath = Path.Combine(NuspecOutPath, $"{fi.Name.Replace("packageid", PackageId)}");

                item.SetMetadata("FileObjPath", CreatedPropsPath);

                cppList.Add(new TaskItem(CreatedPropsPath));

              if (String.IsNullOrEmpty(UniqueName.Trim()))
                   UniqueName = ManifestTools.GenerateManifestUniqueName(PackageId);

              File.WriteAllText(CreatedPropsPath, propsText
                   .Replace("$packageid$", PackageId)
                   .Replace("$uniquename$", UniqueName)
                   .Replace("$packageversion$", PackageVersion)
                   .Replace("IncludeStrati_uniquename", $"IncludeStrati_{UniqueName}".Replace(".", "_"))
                   .Replace("$gitrepositoryurl$", GitRepositoryUrl)
                   .Replace("$gitcommit$", GitCommit)
                   .Replace("$gitcommitdate$", GitCommitDate)
                   .ReplaceTrueOrEmpty("$overwriteunmanaged$", OverwriteUnmanaged)
                   .ReplaceTrueOrEmpty("$publishandactivate$", PublishAndActivate)
                   );

            }

            CreatedPropsPaths = cppList.ToArray();

            return true;
        }
    }


    public static class GenerateBuildTransitivePropsExtensions
    {
        public static string ReplaceTrueOrEmpty(this string value, string replacement, bool boolValue)
        {
            if (boolValue) return value.Replace(replacement, "true");
            else return value.Replace(replacement, "");
        }

    }
}

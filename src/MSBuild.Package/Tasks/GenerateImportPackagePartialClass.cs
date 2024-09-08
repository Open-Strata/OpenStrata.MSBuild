using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenStrata.MSBuild.Package.Tasks
{
    public class GenerateImportPackagePartialClass : BaseTask
    {

        [Required]
        public string DefaultNameSpace { get; set; }

        [Required]
        public string ImportPackageShortName { get; set; }

        [Required]
        public string ImportPackageLongName { get; set; }

        [Required]
        public string ImportPackageDescription { get; set; }

        [Required]
        public string ImportPackageDataFolder { get; set; }

        [Required]
        public string CodeTemplatePath { get; set; }

        [Required]
        public string OutputDir { get; set; }

        [Output]
        public string OutputCodeFile { get; set; }

        public override bool ExecuteTask()
        {

            var codeTemplateText = File.ReadAllText(CodeTemplatePath);

            OutputCodeFile = Path.Combine(OutputDir, $"ImportPackage.msbuild.cs");

            Directory.CreateDirectory((new FileInfo(OutputCodeFile).DirectoryName));

            //Write Props
            File.WriteAllText(OutputCodeFile, codeTemplateText
                .Replace("$defaultnamespace$", DefaultNameSpace)
                .Replace("$importpackageshortname$", ImportPackageShortName)
                .Replace("$importpackagelongname$", ImportPackageLongName)
                .Replace("$importpackagedescription$", ImportPackageDescription)
                .Replace("$importpackagedatafolder$", ImportPackageDataFolder));

            return true;
        }
    }
    
}

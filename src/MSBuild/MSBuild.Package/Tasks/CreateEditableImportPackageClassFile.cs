using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenStrata.MSBuild.Package.Tasks
{
    public class CreateEditableImportPackageClassFile : BaseTask
    {
        [Required]
        public string DefaultNameSpace { get; set; }

        [Required]
        public string CodeTemplatePath { get; set; }

        [Required]
        public string OutputDir { get; set; }

        [Output]
        public string OutputCodeFile { get; set; }

        public override bool ExecuteTask()
        {

            var codeTemplateText = File.ReadAllText(CodeTemplatePath);

            OutputCodeFile = Path.Combine(OutputDir, $"ImportPackage.cs");

            //Write Props
            File.WriteAllText(OutputCodeFile, codeTemplateText
                .Replace("$defaultnamespace$", DefaultNameSpace));

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using OpenStrata.MSBuild;


namespace OpenStrata.MSBuild.Tasks
{
    public class CreateStrongNameKeyFile : BaseTask
    {

        [Required]
        public string Path { get; set; }

        public override bool ExecuteTask()
        {

            AssemblySigningKeyFile.Create(Path);


            return true;

        }
    }
}

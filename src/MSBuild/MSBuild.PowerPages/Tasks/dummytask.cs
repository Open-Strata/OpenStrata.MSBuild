using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenStrata.MSBuild.PowerPages.Tasks
{
    public class dummytask : BaseTask
    {
        public override bool ExecuteTask()
        {
            return true;
        }
    }
}

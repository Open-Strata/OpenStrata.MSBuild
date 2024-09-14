using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using OpenStrata.Solution.Xml;
using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenStrata.MSBuild.Solution.Tasks
{
    public class SetSolutionInfo : BaseTask
    {
        [Required]
        public string RootPath { get; set; }

        //[Required]
        //public string DataverseSolutionUniqueName { get; set; }

        [Required]
        public string DataverseSolutionVersion { get; set; }

        //[Required]
        //public string DataverseSolutionManaged { get; set; }

        public string PublisherXmlPath { get; set; }

        public override bool ExecuteTask()
        {

            var solXDoc = SolutionXDocument.LoadFromRootPath(RootPath);


            solXDoc.SolutionManifest.Version.Value = DataverseSolutionVersion;

            if (!solXDoc.SolutionManifest.ReplacePublisherNodeWithPublisherXmlFile(PublisherXmlPath, out string message, out Exception exception))
            {
                Log.LogMessage(message);
            }

            solXDoc.SaveToRoot(RootPath);

            return true;

        }
    }
}

using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using OpenStrata.Solution.Publisher;
using OpenStrata.Solution.Xml;
using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenStrata.MSBuild.Solution.Tasks
{
    public class GetSolutionInfo : BaseTask
    {

        [Required]
        public string RootPath { get; set; }

        [Output]
        public string DataverseSolutionUniqueName { get; set; }

        [Output]
        public string DataverseSolutionVersion { get; set; }

        [Output]
        public string DataverseSolutionManaged { get; set; }

        [Output]
        public string DataverseSolutionPublisherUniqueName { get; set; }

        [Output]
        public string DataverseSolutionCustomizationPrefix { get; set; }

        public override bool ExecuteTask()
        {

            var solXDoc = SolutionXDocument.LoadFromRootPath(RootPath);

            DataverseSolutionUniqueName = solXDoc.SolutionManifest.UniqueName.Value;
            DataverseSolutionVersion = solXDoc.SolutionManifest.Version.Value;
            DataverseSolutionManaged = solXDoc.SolutionManifest.Managed.Value;
            DataverseSolutionPublisherUniqueName = solXDoc.SolutionManifest.Publisher.GetOrCreateElement("UniqueName").Value;
            DataverseSolutionCustomizationPrefix = solXDoc.SolutionManifest.Publisher.GetOrCreateElement("CustomizationPrefix").Value;

            return true;

        }

    }
}

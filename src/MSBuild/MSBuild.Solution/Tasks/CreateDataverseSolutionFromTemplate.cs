// Copyright (c) Open-Strata contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project or repository root for license information.

using OpenStrata.MSBuild.Tasks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using OpenStrata.Solution.Publisher;

namespace OpenStrata.MSBuild.Solution.Tasks
{

    public class CreateDataverseSolutionFromTemplate : BaseTask
    {
        /// <summary>
        /// This is a sample documentation item.
        /// </summary>
        public string TemplatePath { get; set; }

        public string SolutionName { get; set; }

        public string PublisherUniqueName { get; set; }

        public string CustomizationPrefix { get; set; }

        public override bool ExecuteTask()
        {
            var OptionValuePrefix = PublisherTools.GenerateOptionValuePrefixForPublisher(CustomizationPrefix);

            var solutionContent = File.ReadAllText(TemplatePath);
            File.Delete(TemplatePath);

            var finalFileName = Path.Combine(Path.GetDirectoryName(TemplatePath), "Solution.xml");


            string dsUniqueName;

            if (!PublisherTools.FixedSolutionUniqueName(SolutionName, out dsUniqueName, out string message))
            {
                return this.TaskFailed(message);
            }

            string pubUniqueName;

            if (!PublisherTools.FixedPublisherUniqueName (PublisherUniqueName, out pubUniqueName, out message))
            {
                return this.TaskFailed(message);
            }



            File.WriteAllText(finalFileName,
            solutionContent.Replace("$solutionName$", SolutionName)
                           .Replace("$solutionUniqueName$", dsUniqueName)
                           .Replace("$publisherName$", PublisherUniqueName)
                           .Replace("$publisherUniqueName$", pubUniqueName)
                           .Replace("$customizationPrefix$", CustomizationPrefix)
                           .Replace("$customizationOptionValuePrefix$", OptionValuePrefix));

            return true;
        }
    }

}

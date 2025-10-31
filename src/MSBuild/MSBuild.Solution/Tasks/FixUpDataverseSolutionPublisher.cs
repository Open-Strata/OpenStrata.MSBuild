// Copyright (c) Open-Strata contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project or repository root for license information.


using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenStrata.MSBuild.Solution.Tasks
{
    public class FixUpDataverseSolutionPublisher : BaseTask
    {
        [Required]
        public string PublisherUniqueName { get; set; }

        [Output]
        public string FixedPublisherUniqueName { get; set; }

        public override bool ExecuteTask()
        {
            //TODO: write code to fix publisher name
            FixedPublisherUniqueName = PublisherUniqueName;

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStrata.Deployment.Sdk
{
    interface ISolutionUpgradeMigrationStep
    {
        string PrimarySolutionName {get;}

        bool StepAppliesForSolution(string solutionName, string oldVersion , string newVersion, Guid oldSolutionId , Guid newSolutionId );

        void RunSolutionUpgradeMigrationStep(string solutionName, string oldVersion, string newVersion, Guid oldSolutionId, Guid newSolutionId);

    }
}

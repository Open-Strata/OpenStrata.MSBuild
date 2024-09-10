using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStrata.Deployment.Sdk
{
    public interface IImportPackageStrataExtension
    {

        bool AfterPrimaryImport();


        bool BeforeImportStage();

        //string GetNameOfImport(bool plural);

        void InitializeCustomExtension(ImportPackageStrataBase thisExtension);

       // Microsoft.Uii.Common.Entities.ApplicationRecord BeforeApplicationRecordImport(Microsoft.Uii.Common.Entities.ApplicationRecord app);

        int OverrideConfigurationDataFileLanguage(int selectedLanguage, List<int> availableLanguages);

        //string GetFinanceOperationsConfiguration(IEnumerable<string> financeOperationsConfigurationList);
        //string OverrideFinanceOperationsConfigurationSettings(string configurationDocument, string prefix, IEnumerable<string> allowedValues);

        UserRequestedImportAction OverrideSolutionImportDecision(UserRequestedImportAction userRequestedImportAction, string solutionUniqueName, Version organizationVersion, Version packageSolutionVersion, Version inboundSolutionVersion, Version deployedSolutionVersion, ImportAction systemSelectedImportAction);

        void ApplyOrganizationSettings();

        void PreSolutionImport(string solutionName, bool solutionOverwriteUnmanagedCustomizations, bool solutionPublishWorkflowsAndActivatePlugins, out bool overwriteUnmanagedCustomizations, out bool publishWorkflowsAndActivatePlugins);

        void RunSolutionUpgradeMigrationStep(string solutionName, string oldVersion, string newVersion, Guid oldSolutionId, Guid newSolutionId);

        bool AppliesToThisSolution(string solutionName);

    }
}

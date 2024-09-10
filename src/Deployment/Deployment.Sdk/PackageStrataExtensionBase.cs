//using Microsoft.Uii.Common.Entities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase;
using OpenStrata.Strati.Manifest.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Workflow.Activities;

namespace OpenStrata.Deployment.Sdk
{

    public abstract class PackageStrataExtensionBase : IImportPackageStrataExtension
    {

        //Making this internal So default feature extensions can access it.
        // Such as Config Data Settings, advanced logging, etc.
        internal ImportPackageStrataBase _importExtension;

        #region ImportExtensionPropertyWrappers

        protected int StaleImportProgressOverride => _importExtension.StaleImportProgressOverride;

        protected int SolutionStatusPollingFrequencyOverride => _importExtension.SolutionStatusPollingFrequencyOverride;

        protected int SolutionImportBlockedRetryCountOverride => _importExtension.SolutionImportBlockedRetryCountOverride;

        protected int SolutionImportBlockedWaitOverride => _importExtension.SolutionImportBlockedWaitOverride;

        protected bool UseAsyncForSolutionImport => _importExtension.UseAsyncForSolutionImport;

        protected bool UseAsyncForSolutionDeleteAndPromote => _importExtension.UseAsyncForSolutionDeleteAndPromote;

        protected bool EnableBatchMode => _importExtension.EnableBatchMode;

        protected int PrefetchRecordLimitCount => _importExtension.PrefetchRecordLimitCount;

        protected int RequestedBatchSize => _importExtension.RequestedBatchSize;

        protected ImportStrataManifestXDocument ImportStrataManifest => _importExtension.ImportStrataManifest;

        protected string AbsoluteImportPackageDataFolderPath => _importExtension?.AbsoluteImportPackageDataFolderPath;

        //
        // Summary:
        //     Logging Interface.
        protected TraceLogger PackageLog => _importExtension?.PackageLog;

        //
        // Summary:
        //     Returns a pointer to the CRM instance.
        protected CrmServiceClient CrmSvc => _importExtension?.CrmSvc;

        ////Skipping this for now.
        ////
        //// Summary:
        ////     Parent dispatcher for displaying UI elements.

        public System.Windows.Threading.Dispatcher RootControlDispatcher => _importExtension.RootControlDispatcher;

        //
        // Summary:
        //     Returns the long name of the import process.
        protected string ImportLongName => _importExtension?.GetImportLongName;
        //
        // Summary:
        //     Returns the folder name of the import.
        protected string ImportPackageFolderName => _importExtension?.GetImportPackageFolderName;
        //
        // Summary:
        //     Gets package description import.
        protected string ImportPackageDescription => _importExtension?.GetImportPackageDescription;

        //
        // Summary:
        //     Settings that will be passed to package.
        protected Dictionary<string, object> RuntimeSettings => _importExtension?.RuntimeSettings;

        //
        // Summary:
        //     Allows data import process to be skipped if true, the process is skipped, if
        //     false, it is not. Default is false. for this to function, it needs to be set
        //     in or before the PreImport function call
        protected bool DataImportBypass => _importExtension.DataImportBypass;

        //
        // Summary:
        //     Allows a package developer to skip data validation on Complex data imports The
        //     net effect is that import is much faster at the cost of the potential data collisions
        protected bool OverrideDataImportSafetyChecks => _importExtension.OverrideDataImportSafetyChecks;

        //
        // Summary:
        //     Overrides the timestamp specified in the exported data.
        protected DateTime? OverrideDataTimestamp => _importExtension?.OverrideDataTimestamp;

        //
        // Summary:
        //     Returns the location of the executing package
        protected string ImportPackageLocation => _importExtension?.CurrentPackageLocation;

        //
        // Summary:
        //     Returns the Title Link Text that is show on the completed page.

        protected virtual string GetExtraProgramLaunchLinkText => _importExtension?.GetExtraProgramLaunchLinkText;

        //
        // Summary:
        //     Returns the long name of the import process.
        protected string LongNameOfImport => _importExtension.GetLongNameOfImport;
        //
        // Summary:
        //     Returns the name of the Import package data folder.
        protected string ImportPackageDataFolderName => _importExtension.GetImportPackageDataFolderName;
        //
        // Summary:
        //     Description of the import package.
        protected string ImportPackageDescriptionText => _importExtension.GetImportPackageDescriptionText;
        //
        // Summary:
        //     Overrides the dateMode of all fields during import.
        protected DataMigrationDateMode? OverrideDateMode => _importExtension.OverrideDateMode;


        protected void AttachAddNewProgressItemHandler (EventHandler<ProdgressDataItemEventArgs> handler)
        {
            _importExtension.AddNewProgressItem += handler;
        }

        protected void RemoveAddNewProgressItemHandler(EventHandler<ProdgressDataItemEventArgs> handler)
        {
            _importExtension.AddNewProgressItem -= handler;
        }

        #endregion

        #region AfterPrimaryImport

        bool IImportPackageStrataExtension.AfterPrimaryImport()
        {
            PackageLog.Log($"OpenStrata : AfterPrimaryImport : {this.GetType().FullName}");
            //TODO:  Do some logging and error handling.
            return AfterPrimaryImport();
        }

        protected virtual bool AfterPrimaryImport()
        {
            return true;
        }

        #endregion

        //#region BeforeApplicationRecordImport

        //ApplicationRecord IImportPackageStrataExtension.BeforeApplicationRecordImport(ApplicationRecord app)
        //{
        //    PackageLog.Log($"OpenStrata : BeforeApplicationRecordImport : {this.GetType().FullName}");
        //    //TODO:  Do some logging and error handling.
        //    return BeforeApplicationRecordImport(app);
        //}

        //protected virtual ApplicationRecord BeforeApplicationRecordImport(ApplicationRecord app)
        //{
        //    PackageLog.Log($"OpenStrata : BeforeApplicationRecordImport : {this.GetType().FullName}");
        //    return app;
        //}

        //#endregion
         
        #region BeforeImportStage

        bool IImportPackageStrataExtension.BeforeImportStage()
        {
            PackageLog.Log($"OpenStrata : BeforeImportStage : {this.GetType().FullName}");
            return BeforeImportStage();
        }

        protected virtual bool BeforeImportStage()
        {
            return true;
        }

        #endregion

        #region GetNameOfImport

        // This is defined at the local package creation.  There will be a need to provide some form of identification for this extension.
        //string IImportPackageStrataExtension.GetNameOfImport(bool plural)
        //{
        //    throw new NotImplementedException();
        //}


        #endregion

        #region OverrideConfigurationDataFileLanguage
        int IImportPackageStrataExtension.OverrideConfigurationDataFileLanguage(int selectedLanguage, List<int> availableLanguages)
        {
            PackageLog.Log($"OpenStrata : OverrideConfigurationDataFileLanguage : {this.GetType().FullName}");
            return OverrideConfigurationDataFileLanguage(selectedLanguage, availableLanguages);
        }

        protected virtual int OverrideConfigurationDataFileLanguage(int selectedLanguage, List<int> availableLanguages)
        {
            return selectedLanguage;
        }

        #endregion

        #region ApplyOrganizationSettings

        void IImportPackageStrataExtension.ApplyOrganizationSettings()
        {
            PackageLog.Log($"OpenStrata : ApplyOrganizationSettings : {this.GetType().FullName}");
            ApplyOrganizationSettings();
        }

        protected virtual void ApplyOrganizationSettings(){}

        protected void ApplyOrgDbOrgSetting<T>(string orgdbsetting, Nullable<T> value, bool continueOnError = true, bool skipNullValues = true)
            where T : struct
        {
            if (value == null && skipNullValues)
            {
                PackageLog.Log($"OpenStrata : ApplyOrganizationSetting : {this.GetType().FullName} : Skipping OrgDbOrgSetting {orgdbsetting}.  No value provided.");
                return;
            }
            else if (value == null) 
            {
                ApplyOrgDbOrgSettingXml(orgdbsetting, "", continueOnError);
            } 
            else 
            {
                ApplyOrgDbOrgSettingXml(orgdbsetting, value.Value.ToString(), continueOnError);
            }

        }
        protected void ApplyOrgDbOrgSetting(string orgdbsetting, object value, bool continueOnError = true, bool skipNullValues = true)
        {
            if (value == null && skipNullValues)
            {
                PackageLog.Log($"OpenStrata : ApplyOrganizationSetting : {this.GetType().FullName} : Skipping OrgDbOrgSetting {orgdbsetting}.  No value provided.");
                return;
            }
            else if (value == null)
            {
                ApplyOrgDbOrgSettingXml(orgdbsetting, "", continueOnError);
            }
            else
            {
                ApplyOrgDbOrgSettingXml(orgdbsetting, value.ToString(), continueOnError);
            }
        }
        private void ApplyOrgDbOrgSettingXml(string orgdbsetting, string txtValue, bool continueOnError = true)
        {
            ApplyOrganizationSetting("orgdborgsettings", $"<OrgSettings><{orgdbsetting}>{txtValue}</{orgdbsetting}></OrgSettings>",continueOnError,false);
        }
        protected void ApplyOrgSettingOptionSetValue(string logicalName, int? choiceValue, bool continueOnError = true, bool skipNullValues = true)
        {
            if (choiceValue == null)
            {
                ApplyOrganizationSetting(logicalName, null, continueOnError, skipNullValues);
            }
            else
            {
                ApplyOrganizationSetting(logicalName, new OptionSetValue(choiceValue.Value), continueOnError);
            }
        }
        protected void ApplyOrgSetting<T>(string logicalName, Nullable<T> value, bool continueOnError = true, bool skipNullValues = true)
            where T : struct
        {
            if (value == null)
            {
                ApplyOrganizationSetting(logicalName, null, continueOnError, skipNullValues);
            }
            else
            {
                ApplyOrganizationSetting(logicalName, value.Value, continueOnError);
            }
        }
        protected void ApplyOrganizationSetting(string logicalName, object value, bool continueOnError = true, bool skipNullValues = true)
        {
            if (value == null && skipNullValues) 
            {
                PackageLog.Log($"OpenStrata : ApplyOrganizationSetting : {this.GetType().FullName} : Skipping {logicalName}.  No value provided.");
                return;
            }
            try
            {
                var orgEntity = new Entity("organization", CrmSvc.OrganizationDetail.OrganizationId);

                PackageLog.Log($"OpenStrata : ApplyOrganizationSetting : {this.GetType().FullName} : Attempting to apply setting {logicalName} = {value}");

                orgEntity.Attributes.Add(logicalName, value);

                this.CrmSvc.Update(orgEntity);

                PackageLog.Log($"OpenStrata : ApplyOrganizationSetting : {this.GetType().FullName} :  Successfully applied setting {logicalName} = {value}");

            }
            catch (Exception ex)
            {
                if (continueOnError)
                {
                    PackageLog.Log($"OpenStrata : ApplyOrganizationSetting : {this.GetType().FullName} :  Non-Fatal EXCEPTION : Apply setting {logicalName} = {value} failed.");
                    PackageLog.Log(ex);
                    PackageLog.Log($"OpenStrata : ApplyOrganizationSetting : {this.GetType().FullName} : Continuing Processing");
                }
                else
                {
                    PackageLog.Log($"OpenStrata : ApplyOrganizationSetting : {this.GetType().FullName} :  FATAL EXCEPTION : Apply setting {logicalName} = {value} failed.");
                    PackageLog.Log(ex);
                    throw (ex);
                }
            }
        }

        #endregion

        #region PreSolutionImport


        void IImportPackageStrataExtension.PreSolutionImport(string solutionName, bool solutionOverwriteUnmanagedCustomizations, bool solutionPublishWorkflowsAndActivatePlugins, out bool overwriteUnmanagedCustomizations, out bool publishWorkflowsAndActivatePlugins)
        {
            PackageLog.Log($"OpenStrata : PreSolutionImport : {this.GetType().FullName}");
             PreSolutionImport( solutionName,  solutionOverwriteUnmanagedCustomizations, solutionPublishWorkflowsAndActivatePlugins, out  overwriteUnmanagedCustomizations, out  publishWorkflowsAndActivatePlugins);
        }

        protected virtual void PreSolutionImport(string solutionName, bool solutionOverwriteUnmanagedCustomizations, bool solutionPublishWorkflowsAndActivatePlugins, out bool overwriteUnmanagedCustomizations, out bool publishWorkflowsAndActivatePlugins)
        {
            overwriteUnmanagedCustomizations = solutionOverwriteUnmanagedCustomizations;
            publishWorkflowsAndActivatePlugins = solutionPublishWorkflowsAndActivatePlugins;
        }

        #endregion

        #region RunSolutionUpgradeMigrationStep


        void IImportPackageStrataExtension.RunSolutionUpgradeMigrationStep(string solutionName, string oldVersion, string newVersion, Guid oldSolutionId, Guid newSolutionId)
        {
            PackageLog.Log($"OpenStrata : RunSolutionUpgradeMigrationStep : {this.GetType().FullName}");
            RunSolutionUpgradeMigrationStep(solutionName, oldVersion, newVersion, oldSolutionId, newSolutionId);
        }

        protected virtual void RunSolutionUpgradeMigrationStep(string solutionName, string oldVersion, string newVersion, Guid oldSolutionId, Guid newSolutionId) { }

        #endregion

        #region InitializeCustomExtension

        void IImportPackageStrataExtension.InitializeCustomExtension(ImportPackageStrataBase thisExtension)
        {
            _importExtension = thisExtension;
            PackageLog.Log($"OpenStrata : InitializeCustomExtension : {this.GetType().FullName}");
            InitializeCustomExtension();
        }
        protected virtual void InitializeCustomExtension() { }

        #endregion

        #region OverrideSolutionImportDecision

        UserRequestedImportAction IImportPackageStrataExtension.OverrideSolutionImportDecision(UserRequestedImportAction userRequestedImportAction, string solutionUniqueName, Version organizationVersion, Version packageSolutionVersion, Version inboundSolutionVersion, Version deployedSolutionVersion, ImportAction systemSelectedImportAction)
        {
            PackageLog.Log($"OpenStrata : OverrideSolutionImportDecision : {this.GetType().FullName}");
            return OverrideSolutionImportDecision(userRequestedImportAction, solutionUniqueName, organizationVersion, packageSolutionVersion, inboundSolutionVersion, deployedSolutionVersion, systemSelectedImportAction);
        }

        protected virtual UserRequestedImportAction OverrideSolutionImportDecision(UserRequestedImportAction userRequestedImportAction, string solutionUniqueName, Version organizationVersion, Version packageSolutionVersion, Version inboundSolutionVersion, Version deployedSolutionVersion, ImportAction systemSelectedImportAction)
        {
            return userRequestedImportAction;
        }

        #endregion

        #region AppliesToThisSolution

        bool IImportPackageStrataExtension.AppliesToThisSolution(string solutionName)
        {
            PackageLog.Log($"OpenStrata : AppliesToThisSolution : {this.GetType().FullName}");
            return AppliesToThisSolution(solutionName);
        }

        protected abstract bool AppliesToThisSolution(string solutionName);

        #endregion

        #region ImportExtenstion internal method wrappers


        ////
        //// Summary:
        ////     Raised a failed event to the caller.
        ////
        //// Parameters:
        ////   message:
        ////
        ////   Ex:
        ////
        ////   stage:
        //// IMPORTANT NOTE:  This is a method orginating from Microsoft.CrmSdk.XrmTooling.PackageDeployment.Core
        ////                  https://www.nuget.org/packages/Microsoft.CrmSdk.XrmTooling.PackageDeployment.Core/9.1.0.40?_src=template
        ////                 This is marked as no longer listed on nuget.org, but as of 10/23/2021 is still included
        ////                  in the pac command line template.///
        //protected void RaiseFailEvent(string message, Exception Ex, PDStage stage)
        //{
        //    //should consider whether or not stage should be managed and not be required for this method.
        //    _importExtension.RaiseFailEvent(message, Ex, stage);
        //}


        ////
        //// Summary:
        ////     Raise a Status updated event.
        ////
        //// Parameters:
        ////   message:
        ////
        ////   status:
        ////
        ////   stage:
        //// IMPORTANT NOTE:  This is a method orginating from Microsoft.CrmSdk.XrmTooling.PackageDeployment.Core
        ////                  https://www.nuget.org/packages/Microsoft.CrmSdk.XrmTooling.PackageDeployment.Core/9.1.0.40?_src=template
        ////                 This is marked as no longer listed on nuget.org, but as of 10/23/2021 is still included
        ////                  in the pac command line template.
        //protected void RaiseUpdateEvent(string message, ProgressPanelItemStatus status, PDStage stage)
        //{
        //    _importExtension.RaiseUpdateEvent(message, status, stage);
        //}


        //
        // Summary:
        //     Create and add new progress item.
        //
        // Parameters:
        //   message:
        protected void CreateProgressItem(string message)
        {
            _importExtension._CreateProgressItem(message);
        }

        //
        // Summary:
        //     Gets the Id for a Dialog by Name.
        //
        // Parameters:
        //   name:
        //     Dialog name
        //
        //   isDialog:
        //     Verify is it dailog or not.
        //
        // Returns:
        //     Process id.
        protected Guid GetProcessId(string name, bool isDialog)
        {
            return _importExtension._GetProcessId(name, isDialog);
        }

        //
        // Summary:
        //     Checks to see if the Team is already associated to the requested security role.
        //
        // Parameters:
        //   guTeamId:
        //
        //   guRoleId:
        protected bool IsRoleAssoicatedWithTeam(Guid guTeamId, Guid guRoleId)
        {
            return _importExtension._IsRoleAssoicatedWithTeam(guTeamId, guRoleId);
        }


        //
        // Summary:
        //     Is a Workflow Active.
        //
        // Parameters:
        //   wfId:
        protected bool IsWorkflowActive(Guid wfId)
        {
            return _importExtension._IsWorkflowActive(wfId);
        }


        #endregion


    }
}

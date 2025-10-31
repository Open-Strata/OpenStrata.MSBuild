//using Microsoft.Uii.Common.Entities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase;
using Microsoft.Xrm.Sdk.Messages;
using OpenStrata.Strati.Manifest.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Workflow.Activities;
using OpenStrata.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

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
            return _importExtension._IsRoleAssociatedWithTeam(guTeamId, guRoleId);
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

#region PackageHelpers

        protected void SetOrganizationSetting(string logicalName, object value)
        {
            SetOrganizationSetting(logicalName, value, false);
        }

        protected void SetOrganizationSetting(string logicalName, object value, bool stopOnError)
        {
            try
            {

                var orgEntity = new Entity("organization", CrmSvc.OrganizationDetail.OrganizationId);

                PackageLog.Log($"UPDATING Organization:  Setting {logicalName} = {value}");

                orgEntity.Attributes.Add(logicalName, value);

                this.CrmSvc.Update(orgEntity);

                PackageLog.Log($"UPDATED Organization:  Set {logicalName} = {value}");
            }
            catch (Exception ex)
            {
                {

                    if (stopOnError)
                    {
                        PackageLog.Log($"ERROR: Critical Error Updating Organization:  Set {logicalName} = {value}", System.Diagnostics.TraceEventType.Error);
                        PackageLog.Log($"{ex.GetType()} : {ex.Message}", System.Diagnostics.TraceEventType.Error);
                        throw ex;
                    }
                    else
                    {
                        PackageLog.Log($"Warning: Non-Critical Error Updating Organization when attempting to Set {logicalName} = {value}", System.Diagnostics.TraceEventType.Warning);
                        PackageLog.Log($"{ex.GetType()} : {ex.Message}", System.Diagnostics.TraceEventType.Warning);
                    }
                }
            }

        }


        protected void EnsureUnamangedSolution() { }

        protected Publisher EnsurePublisher(string uniqueName, string customizationPrefix, string friendlyName = null, string description = null, string emailAddress = null, string supportingWebsiteUrl = null)
        {
            return CrmSvc.EnsurePublisher(uniqueName, customizationPrefix, friendlyName, description, emailAddress, supportingWebsiteUrl);
        }

        protected Solution EnsureUnmanagedSolution(string uniqueName, string publisherUniqueName, string publisherCustomizationPrefix, string friendlyName = null, string description = null, string version = "1.0.0")
        {
            return CrmSvc.EnsureUnmanagedSolution(uniqueName, publisherUniqueName, publisherCustomizationPrefix, friendlyName, description, version);
        }

        protected virtual workflow_statecode SetWorkflowState(Solution solution, Workflow workflow)
        {
            return workflow_statecode.Activated;
        }

        private workflow_statecode TurnOnWorkflowInternalCheck (Solution solution, Workflow workflow)
        {
            var friendlyName = workflow.Name?.ToLower() ?? string.Empty;

            return friendlyName.Contains("keep-off") ||
                   friendlyName.StartsWith("†") ||
                   friendlyName.StartsWith("obsolete") ||
                   friendlyName.EndsWith("template") ? workflow_statecode.Draft : workflow_statecode.Activated;
        }

        protected void TurnOnSolutionWorkflows(string solutionUniqueName, int retryCount = 3)
        {
            if (CrmSvc.TryRetrieveSolution(solutionUniqueName, out Solution solution))
            {
                TurnOnSolutionWorkflows(solution,retryCount);
            }
        }

        protected void TurnOnSolutionWorkflows(Solution solution, int retryCount = 3)
        {

            var fetchXml = String.Format(@"
<fetch top=""50"">
  <entity name=""workflow"">
    <attribute name=""name"" />
    <attribute name=""statecode"" />
    <attribute name=""type"" />
    <attribute name=""uniquename"" />
    <attribute name=""workflowid"" />
    <filter>
      <link-entity name=""solutioncomponent"" from=""objectid"" to=""workflowid"" link-type=""any"">
        <filter>
          <condition attribute=""componenttype"" operator=""eq"" value=""29"" />
          <condition attribute=""solutionid"" operator=""eq"" value=""{0}"" />
        </filter>
      </link-entity>
    </filter>
  </entity>
</fetch>
", solution.Id);

            var workflows = CrmSvc.RetrieveMultiple(new FetchExpression(fetchXml)).Entities.ToDictionary(entity => entity.Id);

            var retry = 1;

            PackageLog.Log($"Turning on solution {solution.FriendlyName} workflows.  {workflows.Count} will be considered.");



            while (workflows.Count > 0 && retry <= retryCount)
            {
                var publishWorkflowList = new List<Workflow>();

                if (retry > 1) 
                {
                    PackageLog.Log($"Turning on solution {solution.FriendlyName} workflows.  Attempt {retry}. {workflows.Count} remain to be considered.", System.Diagnostics.TraceEventType.Warning);
                }

                foreach (var entity in workflows.Values.ToArray())
                {
                    var workFlow = (Workflow)entity;
                    var wfDisposition = TurnOnWorkflowInternalCheck(solution, workFlow);
                    if (wfDisposition != workflow_statecode.Draft)
                    {
                        wfDisposition = SetWorkflowState(solution, workFlow);
                    }

                    if (wfDisposition != workFlow.StateCode
                            && TrySetWorkflowStatus(workFlow, wfDisposition))
                    {
                        // remove from further processing...
                        // this is intended to account for child flow dependencies.
                            if (workFlow.StateCode == workflow_statecode.Activated)
                                             publishWorkflowList.Add(workFlow);

                            workflows.Remove(workFlow.Id);
                    }
                    else 
                    {
                        PackageLog.Log($"Ignoring workflow {workFlow.Name}");
                        workflows.Remove(workFlow.Id);
                    }

                }

                publishWorkflowList.ForEach(wf => CrmSvc.PublishWorkflow(wf));

                retry++;
            }
        }

        protected bool  TrySetWorkflowStatus(Workflow workflow, workflow_statecode statecode)
        {
            try
            {
                PackageLog.Log($"Attempting to set {workflow.Name} to {statecode}");
                workflow.StateCode = statecode;
                CrmSvc.Update(workflow);
                return true;
            }
            catch(Exception ex)
            {
                PackageLog.Log($"Unable to set {workflow.Name} to {statecode}", System.Diagnostics.TraceEventType.Warning);
                PackageLog.Log($"{ex.GetType()} : {ex.Message}");
                return false;
            }
        }

        protected void AddConnectionReferenceToSolution(Solution solution, string uniqueName)
        {
            try
            {
                PackageLog.Log($"Adding {uniqueName} connection reference to config solution");

                if (CrmSvc.TryRetrieveFirstOrNull("connectionreference",
                                         new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "connectionreferenceid" }),
                                         "connectionreferencelogicalname", uniqueName, out Entity connRef) &&
                    CrmSvc.TryRetrieveFirstOrNull("solutioncomponent",
                                                  new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "componenttype" }),
                                                   "objectid",connRef.Id, out Entity conrefSolComp))
                    {
                        
                        solution.EnsureExistingComponent(CrmSvc, ((OptionSetValue)conrefSolComp["componenttype"]).Value, connRef.Id);
                    }
                else
                {
                    PackageLog.Log($"Connection reference {uniqueName} does not exist in the environment", System.Diagnostics.TraceEventType.Warning);
                }
            }
            catch (Exception ex)
            {
                PackageLog.Log($"Unable to add {uniqueName} connection reference to {solution.FriendlyName}", System.Diagnostics.TraceEventType.Warning);
                PackageLog.Log($"{ex.GetType()} : {ex.Message}", System.Diagnostics.TraceEventType.Warning);
            }
        }

        protected object AddEnvironmentVariableToSolution(Solution solution, string uniqueName)
        {
            object value = null;

            try
            {
                PackageLog.Log($"Adding {uniqueName} environment variable to {solution.FriendlyName}");

                if (CrmSvc.TryRetrieveFirstOrNull("environmentvariabledefinition",
                                         new Microsoft.Xrm.Sdk.Query.ColumnSet(true),
                                         "schemaname", uniqueName, out Entity envVarRef))
                {
                    solution.EnsureExistingComponent(CrmSvc, componenttype.EnvironmentVariableDefinition, envVarRef.Id);

                    if (CrmSvc.TryRetrieveFirstOrNull("environmentvariablevalue",
                                              new Microsoft.Xrm.Sdk.Query.ColumnSet(true),
                                              "environmentvariabledefinitionid", envVarRef.Id, out Entity envVarValueRef))
                    {
                        value = envVarValueRef.Contains("value") ? envVarValueRef["value"] : null;
                        solution.EnsureExistingComponent(CrmSvc, componenttype.EnvironmentVariableValue, envVarValueRef.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                PackageLog.Log($"Unable to add {uniqueName} environment variable to {solution.FriendlyName}", System.Diagnostics.TraceEventType.Warning);
                PackageLog.Log($"{ex.GetType()} : {ex.Message}", System.Diagnostics.TraceEventType.Warning);
            }

            return value;
        }

        protected Workflow AddWorkflowToSolution(Solution solution, Guid workflowId, bool publish = true)
        {
            Workflow workflow = null;

            try
            {
                PackageLog.Log($"Adding {workflowId} workflow to {solution.FriendlyName}");

                if (CrmSvc.TryRetrieveFirstOrNull(Workflow.EntityLogicalName,
                                         new Microsoft.Xrm.Sdk.Query.ColumnSet(true),
                                         "workflowid", workflowId, out  workflow))
                {
                    solution.EnsureExistingComponent(CrmSvc, componenttype.Workflow, workflow.Id);
                    if (publish)
                    {
                        CrmSvc.PublishWorkflow(workflow);
                    }
                }
            }
            catch (Exception ex)
            {
                PackageLog.Log($"Unable to add workflow {workflowId} environment variable to {solution.FriendlyName}", System.Diagnostics.TraceEventType.Warning);
                PackageLog.Log($"{ex.GetType()} : {ex.Message}", System.Diagnostics.TraceEventType.Warning);
            }

            return workflow;
        }

        protected bool TryAddExistingWorkflowToSolution(Solution solution, Guid workflowId, out Workflow workflow)
        {
            workflow = null;
            try
            {
                PackageLog.Log($"Attempting to add workflow {workflowId} to {solution.FriendlyName}");

                if (CrmSvc.TryRetrieveFirstOrNull(Workflow.EntityLogicalName,
                                         new Microsoft.Xrm.Sdk.Query.ColumnSet(true),
                                         "workflowid", workflowId, out workflow))
                {
                    PackageLog.Log($"Adding  {workflow.Name} Flow to {solution.FriendlyName}");
                    solution.EnsureExistingComponent(CrmSvc, componenttype.Workflow, workflow.Id);
                    PackageLog.Log($"Successully  {workflow.Name} Flow to {solution.FriendlyName}");
                    return true;
                }
                else
                {
                    PackageLog.Log($"Unalge to locate a workflow with the id {workflowId}", System.Diagnostics.TraceEventType.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                PackageLog.Log($"Unable to add workflow {workflowId} to {solution.FriendlyName}", System.Diagnostics.TraceEventType.Warning);
                PackageLog.Log($"{ ex.GetType()} : { ex.Message}", System.Diagnostics.TraceEventType.Warning);
                return false;
            }
        }

        protected bool TryCopyWorkflow(Solution solution, Guid sourceWorkflowId, Guid destinationWorkflowId, string name, Dictionary<string, string> replaceText, out Workflow workflow, bool turnOnAfterCopy = true)
        {

            workflow = null;

            try
            {

                if (CrmSvc.TryRetrieveFirstOrNull(Workflow.EntityLogicalName, new Microsoft.Xrm.Sdk.Query.ColumnSet(true), "workflowid", sourceWorkflowId, out Workflow sourceWf))
                {

                   // solution.EnsureExistingComponent(CrmSvc, componenttype.Workflow, sourceWf.Id);

                    PackageLog.Log($"Attempting to copy workflow {sourceWorkflowId} to {name} , {destinationWorkflowId}");
                    if (CrmSvc.TryRetrieveFirstOrNull(Workflow.EntityLogicalName, new Microsoft.Xrm.Sdk.Query.ColumnSet(true), "workflowid", destinationWorkflowId, out workflow))
                    {
                        PackageLog.Log($"Destination workflow with id {destinationWorkflowId} already exists.");
                        solution.EnsureExistingComponent(CrmSvc, componenttype.Workflow, destinationWorkflowId);
                        workflow.Name = name;
                        workflow.Description = name;
                        workflow.ClientData = sourceWf.ClientData;
                    }
                    else
                    {
                        PackageLog.Log($"Copying {sourceWf.Name} to {name}");

                        workflow = new Workflow()
                        {
                            Id = destinationWorkflowId,
                            Name = name,
                            Description = name,
                            Type = workflow_type.Definition,
                            ClientData = sourceWf.ClientData,
                            PrimaryEntity = "none",
                            Category = workflow_category.ModernFlow,
                        }.Create(CrmSvc);

                        solution.EnsureExistingComponent(CrmSvc, componenttype.Workflow, workflow.Id);
                    }

                    PackageLog.Log($"Attempting to update workflow  {workflow.Name}");

                    var updateWorkflow = new Workflow()
                    {
                        Id = workflow.Id,
                        Name = name,
                        Description = name,
                        ClientData = workflow.ClientData,
                    };


                    PackageLog.Log($"Processing Workflow client data.  Changing {replaceText.Count} strings");
                    foreach (var key in replaceText.Keys)
                    {
                        PackageLog.Log($"Replacing '{key}' with '{replaceText[key]}'");
                        updateWorkflow.ClientData = updateWorkflow.ClientData.Replace(key, replaceText[key]);
                    }

                    PackageLog.Log($"XAML:\r\n{updateWorkflow.ClientData}");

                    CrmSvc.Update(updateWorkflow);

                    PackageLog.Log($"Attempting to publish workflow {updateWorkflow.Name}");
                    CrmSvc.PublishWorkflow(updateWorkflow);

                    if (turnOnAfterCopy)
                    {
                        TrySetWorkflowStatus(workflow, workflow_statecode.Activated);
                    }

                    return true;

                }
                else
                {
                    PackageLog.Log("Template workflow not found", System.Diagnostics.TraceEventType.Warning);
                    return false;
                }

            }
            catch (Exception ex)
            {
                PackageLog.Log("Unable to process workflow copy operation. ", System.Diagnostics.TraceEventType.Warning);
                PackageLog.Log($"{ex.GetType()} : {ex.Message}", System.Diagnostics.TraceEventType.Warning);
                return false;
            }

        }


#endregion




    }
}

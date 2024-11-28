//using Microsoft.Uii.Common.Entities;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase;
using OpenStrata.Strati.Manifest.Xml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStrata.Deployment.Sdk
{


    public abstract class ImportPackageStrataBase : ImportExtension
    {
        //protected IReadOnlyCollection<IImportPackageStrataExtension> Extensions = PackageStrata.Instance.Extensions;

        //public ImportPackageStrataBase() : base()
        //{
        //    PackageLog.Log($"OpenStrata : ImportPackageStrataBase : New()");


           

        //}

        private List<IImportPackageStrataExtension> _extensions;
        private List<IImportPackageStrataExtension> Extensions
        {
            get
            {
                if (_extensions == null)
                {
                    _extensions = ExtensionsFactory.InstantiateExtensions(this);
                }
                return _extensions;
            }
        }

        private ImportPackageStrataExtensionsFactory _extensionFactory;
        protected ImportPackageStrataExtensionsFactory ExtensionsFactory
        {
            get
            {
                if (_extensionFactory == null)
                {
                    _extensionFactory = new ImportPackageStrataExtensionsFactory();

                }
                return _extensionFactory;
            }
        }


        private ImportStrataManifestXDocument _stratamanifest;
        public ImportStrataManifestXDocument ImportStrataManifest 
        {
            get
            {
                if (_stratamanifest == null)
                {

                    PackageLog.Log($"OpenStrata : ImportStrata.Manifest path is {ImportStratiManifestPath}");

                    if (!File.Exists(ImportStratiManifestPath))
                    {
                        PackageLog.Log($"OpenStrata : Cannot find ImportStrata.Manifest at location {ImportStratiManifestPath}");
                    }
                    else
                    {
                        PackageLog.Log($"OpenStrata : Found ImportStrata.Manifest at location {ImportStratiManifestPath}");
                    }

                    _stratamanifest = ImportStrataManifestXDocument.Load(ImportStratiManifestPath);

                    PackageLog.Log(_stratamanifest.Root.ToString());
                }
                return _stratamanifest;
            }
        }

        protected virtual string ImportStratiManifestPath 
        {
            get
            {
                return Path.Combine(AbsoluteImportPackageDataFolderPath, "importstrata.manifest");
            }
        }

        public string AbsoluteImportPackageDataFolderPath
        {
            get
            {
                return Path.Combine(this.CurrentPackageLocation, this.GetImportPackageDataFolderName);
            }
        }

        #region ConfigSetting Wrappers

        public int StaleImportProgressOverride
        {
            get
            {
                if (ConfigurationManager.AppSettings["StaleImportProgressOverride"] == null)
                {
                    ConfigurationManager.AppSettings["StaleImportProgressOverride"] = "50";
                }
                if (int.TryParse(ConfigurationManager.AppSettings["StaleImportProgressOverride"], out int parseResult))
                {
                    return parseResult;
                }
                return 50;
            }
            set
            {
                ConfigurationManager.AppSettings["StaleImportProgressOverride"] = value.ToString();
            }
        }

        public int SolutionStatusPollingFrequencyOverride
        {
            get
            {
                if (ConfigurationManager.AppSettings["SolutionStatusPollingFrequencyOverride"] == null)
                {
                    ConfigurationManager.AppSettings["SolutionStatusPollingFrequencyOverride"] = "5";
                }
                if (int.TryParse(ConfigurationManager.AppSettings["SolutionStatusPollingFrequencyOverride"], out int parseResult))
                {
                    return parseResult;
                }
                return 5;
            }
            set
            {
                ConfigurationManager.AppSettings["SolutionStatusPollingFrequencyOverride"] = value.ToString();
            }
        }

        public int SolutionImportBlockedRetryCountOverride
        {
            get
            {
                if (ConfigurationManager.AppSettings["SolutionImportBlockedRetryCountOverride"] == null)
                {
                    ConfigurationManager.AppSettings["SolutionImportBlockedRetryCountOverride"] = "10";
                }
                if (int.TryParse(ConfigurationManager.AppSettings["SolutionImportBlockedRetryCountOverride"], out int parseResult))
                {
                    return parseResult;
                }
                return 10;
            }
            set
            {
                ConfigurationManager.AppSettings["SolutionImportBlockedRetryCountOverride"] = value.ToString();
            }
        }

        public int SolutionImportBlockedWaitOverride
        {
            get
            {
                if (ConfigurationManager.AppSettings["SolutionImportBlockedWaitOverride"] == null)
                {
                    ConfigurationManager.AppSettings["SolutionImportBlockedWaitOverride"] = "60";
                }
                if (int.TryParse(ConfigurationManager.AppSettings["SolutionImportBlockedWaitOverride"], out int parseResult))
                {
                    return parseResult;
                }
                return 60;
            }
            set
            {
                ConfigurationManager.AppSettings["SolutionImportBlockedWaitOverride"] = value.ToString();
            }
        }

        public bool UseAsyncForSolutionImport
        {
            get
            {
                if (ConfigurationManager.AppSettings["SolutionImportBlockedWaitOverride"] == null)
                {
                    ConfigurationManager.AppSettings["SolutionImportBlockedWaitOverride"] = "false";
                }
                if (bool.TryParse(ConfigurationManager.AppSettings["SolutionImportBlockedWaitOverride"], out bool parseResult))
                {
                    return parseResult;
                }
                return false;
            }
            set
            {
                ConfigurationManager.AppSettings["SolutionImportBlockedWaitOverride"] = value.ToString();
            }
        }

        public bool UseAsyncForSolutionDeleteAndPromote
        {
            get
            {
                if (ConfigurationManager.AppSettings["UseAsyncForSolutionDeleteAndPromote"] == null)
                {
                    ConfigurationManager.AppSettings["UseAsyncForSolutionDeleteAndPromote"] = "false";
                }
                if (bool.TryParse(ConfigurationManager.AppSettings["UseAsyncForSolutionDeleteAndPromote"], out bool parseResult))
                {
                    return parseResult;
                }
                return false;
            }
            set
            {
                ConfigurationManager.AppSettings["UseAsyncForSolutionDeleteAndPromote"] = value.ToString();
            }
        }

        public bool EnableBatchMode
        {
            get
            {
                if (ConfigurationManager.AppSettings["EnableBatchMode"] == null)
                {
                    ConfigurationManager.AppSettings["EnableBatchMode"] = "false";
                }
                if (bool.TryParse(ConfigurationManager.AppSettings["EnableBatchMode"], out bool parseResult))
                {
                    return parseResult;
                }
                return false;
            }
            set
            {
                ConfigurationManager.AppSettings["EnableBatchMode"] = value.ToString();
            }
        }

        public int PrefetchRecordLimitCount
        {
            get
            {
                if (ConfigurationManager.AppSettings["PrefetchRecordLimitCount"] == null)
                {
                    ConfigurationManager.AppSettings["PrefetchRecordLimitCount"] = "60";
                }
                if (int.TryParse(ConfigurationManager.AppSettings["PrefetchRecordLimitCount"], out int parseResult))
                {
                    return parseResult;
                }
                return 60;
            }
            set
            {
                ConfigurationManager.AppSettings["PrefetchRecordLimitCount"] = value.ToString();
            }
        }

        public int RequestedBatchSize
        {
            get
            {
                if (ConfigurationManager.AppSettings["RequestedBatchSize"] == null)
                {
                    ConfigurationManager.AppSettings["RequestedBatchSize"] = "10";
                }
                if (int.TryParse(ConfigurationManager.AppSettings["RequestedBatchSize"], out int parseResult))
                {
                    return parseResult;
                }
                return 10;
            }
            set
            {
                ConfigurationManager.AppSettings["RequestedBatchSize"] = value.ToString();
            }
        }

// Potential Properties from ImportConfig.
//        AsyncRibbonProcessingMode
//crmmigdataimportfile
//installsampledata
//waitforsampledatatoinstall

        #endregion


        #region AfterPrimaryImport()
        protected virtual bool PreAfterPrimaryImport()
            {
                return true;
            }
            public override bool AfterPrimaryImport()
            {
                var result = true;
                //TODO:  Add Async processing       
                foreach (IImportPackageStrataExtension extension in Extensions)
                {
                    if (!extension.AfterPrimaryImport()) return false;
                }
                return result;
            }

            protected virtual bool PostAfterPrimaryImport()
            {
                return true;
            }
        #endregion

        //#region BeforeApplicationRecordImport(ApplicationRecord app)


        //protected virtual ApplicationRecord PreBeforeApplicationRecordImport(ApplicationRecord app)
        //{
        //    return app;
        //}

        //public sealed override ApplicationRecord BeforeApplicationRecordImport(ApplicationRecord app)
        //{
        //    var thisApp = PreBeforeApplicationRecordImport(app);

        //    //TODO:  Add Async processing
        //    foreach (IImportPackageStrataExtension extension in Extensions)
        //    {
        //        extension.BeforeApplicationRecordImport(thisApp);
        //    }

        //    return PostBeforeApplicationRecordImport(thisApp);
        //}

        //protected virtual ApplicationRecord PostBeforeApplicationRecordImport(ApplicationRecord app)
        //{
        //    return app;
        //}

        //#endregion

        #region BeforeImportStage()

        protected virtual bool PreBeforeImportStage()
        {
            return true;
        }

        public override sealed bool BeforeImportStage()
        {
            if (PreBeforeImportStage())
            {
                //TODO:  Add Async processing       
                foreach (IImportPackageStrataExtension extension in Extensions)
                {
                    if (!extension.BeforeImportStage()) return false;
                }
            }
            return PostBeforeImportStage();
        }
        protected virtual bool PostBeforeImportStage()
        {
            return true;
        }

        #endregion

        #region InitializeCustomExtension()

        protected virtual void PreInitializeCustomExtension() { }
 
        public sealed override void InitializeCustomExtension()
        {

            PreInitializeCustomExtension();

            //TODO:  Add Async processing
            foreach (IImportPackageStrataExtension extension in Extensions)
            {
                extension.InitializeCustomExtension(this);
            }

            PostInitializeCustomExtension();
        }

        protected virtual void PostInitializeCustomExtension() { }


        #endregion

        #region OverrideConfigurationDataFileLanguage

        protected virtual int PreOverrideConfigurationDataFileLanguage(int selectedLanguage, List<int> availableLanguages)
        {
            return selectedLanguage;
        }

        public sealed override int OverrideConfigurationDataFileLanguage(int selectedLanguage, List<int> availableLanguages)
        {
            var language = selectedLanguage;
            language =  base.OverrideConfigurationDataFileLanguage(language, availableLanguages);
            language = PreOverrideConfigurationDataFileLanguage(language, availableLanguages);

            foreach (IImportPackageStrataExtension extension in Extensions)
            {
               language = extension.OverrideConfigurationDataFileLanguage(language, availableLanguages);
            }

            language = PostOverrideConfigurationDataFileLanguage(language, availableLanguages);

            return language;
        }

        protected virtual int PostOverrideConfigurationDataFileLanguage(int selectedLanguage, List<int> availableLanguages)
        {
            return selectedLanguage;
        }


        #endregion

        #region GetFinanceOperationsConfiguration(IEnumerable<string> financeOperationsConfigurationList)

        //public override string GetFinanceOperationsConfiguration(IEnumerable<string> financeOperationsConfigurationList)
        //{
        //    //TODO:  Add Async processing
        //    //TODO:  Look how this action is handled now.  Strange that the input is an IEnumerable<string> and the output is string.
        //    //WARNING:  This is definately note the correct way to handle this.
        //    foreach (IImportPackageStrataExtension extension in Extensions)
        //    {
        //        extension.GetFinanceOperationsConfiguration(financeOperationsConfigurationList);
        //    }
        //    return financeOperationsConfigurationList.ToString();
        //}

        #endregion

        #region OverrideFinanceOperationsConfigurationSettings




        //public sealed override string OverrideFinanceOperationsConfigurationSettings(string configurationDocument, string prefix, IEnumerable<string> allowedValues)
        //{
        //    return base.OverrideFinanceOperationsConfigurationSettings(configurationDocument, prefix, allowedValues);
        //}


        #endregion

        #region OverrideSolutionImportDecision

        protected virtual UserRequestedImportAction PreOverrideSolutionImportDecision(UserRequestedImportAction userRequestedImportAction, string solutionUniqueName, Version organizationVersion, Version packageSolutionVersion, Version inboundSolutionVersion, Version deployedSolutionVersion, ImportAction systemSelectedImportAction)
        {
            return userRequestedImportAction;
        }

        public sealed override UserRequestedImportAction OverrideSolutionImportDecision(string solutionUniqueName, Version organizationVersion, Version packageSolutionVersion, Version inboundSolutionVersion, Version deployedSolutionVersion, ImportAction systemSelectedImportAction)
        {
            var userRequestedAction = base.OverrideSolutionImportDecision(solutionUniqueName, organizationVersion, packageSolutionVersion, inboundSolutionVersion, deployedSolutionVersion, systemSelectedImportAction);
            userRequestedAction = PreOverrideSolutionImportDecision(userRequestedAction,solutionUniqueName, organizationVersion, packageSolutionVersion, inboundSolutionVersion, deployedSolutionVersion, systemSelectedImportAction);

            //TODO:  Add Async processing
            foreach (IImportPackageStrataExtension extension in Extensions)
            {
                if (extension.AppliesToThisSolution(solutionUniqueName))
                {
                    userRequestedAction = extension.OverrideSolutionImportDecision(userRequestedAction, solutionUniqueName, organizationVersion, packageSolutionVersion, inboundSolutionVersion, deployedSolutionVersion, systemSelectedImportAction);
                }
            }


            return PostOverrideSolutionImportDecision(userRequestedAction, solutionUniqueName, organizationVersion, packageSolutionVersion, inboundSolutionVersion, deployedSolutionVersion, systemSelectedImportAction);
        }

        protected virtual UserRequestedImportAction PostOverrideSolutionImportDecision(UserRequestedImportAction userRequestedImportAction, string solutionUniqueName, Version organizationVersion, Version packageSolutionVersion, Version inboundSolutionVersion, Version deployedSolutionVersion, ImportAction systemSelectedImportAction)
        {
            return userRequestedImportAction;
        }

        #endregion

        #region PreSolutionImport()

        protected virtual void PrePreSolutionImport(string solutionName, bool solutionOverwriteUnmanagedCustomizations, bool solutionPublishWorkflowsAndActivatePlugins, out bool overwriteUnmanagedCustomizations, out bool publishWorkflowsAndActivatePlugins)
        {
            overwriteUnmanagedCustomizations = solutionOverwriteUnmanagedCustomizations;
            publishWorkflowsAndActivatePlugins = solutionPublishWorkflowsAndActivatePlugins;
        }

#pragma warning disable CS0672 // Member overrides obsolete member
        public sealed override void PreSolutionImport(string solutionName, out bool overwriteUnmanagedCustomizations, out bool publishWorkflowsAndActivatePlugins)
#pragma warning restore CS0672 // Member overrides obsolete member
        {
            base.PreSolutionImport(solutionName,false,true, out overwriteUnmanagedCustomizations, out publishWorkflowsAndActivatePlugins);

        }

        public sealed override void PreSolutionImport(string solutionName, bool solutionOverwriteUnmanagedCustomizations, bool solutionPublishWorkflowsAndActivatePlugins, out bool overwriteUnmanagedCustomizations, out bool publishWorkflowsAndActivatePlugins)
        {
            base.PreSolutionImport(solutionName, solutionOverwriteUnmanagedCustomizations, solutionPublishWorkflowsAndActivatePlugins, out overwriteUnmanagedCustomizations, out publishWorkflowsAndActivatePlugins);

            PrePreSolutionImport(solutionName, overwriteUnmanagedCustomizations, publishWorkflowsAndActivatePlugins, out overwriteUnmanagedCustomizations, out publishWorkflowsAndActivatePlugins);

            //TODO:  Add Async processing
            foreach (IImportPackageStrataExtension extension in Extensions)
            {
                if (extension.AppliesToThisSolution(solutionName)) { 
                    extension.PreSolutionImport(solutionName, overwriteUnmanagedCustomizations, publishWorkflowsAndActivatePlugins, out overwriteUnmanagedCustomizations, out publishWorkflowsAndActivatePlugins);
                }
            }

            PostPreSolutionImport(solutionName, overwriteUnmanagedCustomizations, publishWorkflowsAndActivatePlugins, out overwriteUnmanagedCustomizations, out publishWorkflowsAndActivatePlugins);

        }

        protected virtual void PostPreSolutionImport(string solutionName, bool solutionOverwriteUnmanagedCustomizations, bool solutionPublishWorkflowsAndActivatePlugins, out bool overwriteUnmanagedCustomizations, out bool publishWorkflowsAndActivatePlugins)
        {
            overwriteUnmanagedCustomizations = solutionOverwriteUnmanagedCustomizations;
            publishWorkflowsAndActivatePlugins = solutionPublishWorkflowsAndActivatePlugins;
        }


        #endregion

        #region RunSolutionUpgradeMigrationStep()

        protected virtual void PreRunSolutionUpgradeMigrationStep(string solutionName, string oldVersion, string newVersion, Guid oldSolutionId, Guid newSolutionId) { }

        public sealed override void RunSolutionUpgradeMigrationStep(string solutionName, string oldVersion, string newVersion, Guid oldSolutionId, Guid newSolutionId)
        {
            base.RunSolutionUpgradeMigrationStep(solutionName, oldVersion, newVersion, oldSolutionId, newSolutionId);

            PreRunSolutionUpgradeMigrationStep(solutionName, oldVersion, newVersion, oldSolutionId, newSolutionId);

            //TODO:  Add Async processing
            foreach (IImportPackageStrataExtension extension in Extensions)
            {
                if (extension.AppliesToThisSolution(solutionName))
                {
                    extension.RunSolutionUpgradeMigrationStep(solutionName, oldVersion, newVersion, oldSolutionId, newSolutionId);
                }
            }

            PostRunSolutionUpgradeMigrationStep(solutionName, oldVersion, newVersion, oldSolutionId, newSolutionId);
        }

        protected virtual void PostRunSolutionUpgradeMigrationStep(string solutionName, string oldVersion, string newVersion, Guid oldSolutionId, Guid newSolutionId) { }

        #endregion


        #region InternalWrapperForProtectedMethods

        //
        // Summary:
        //     Create and add new progress item.
        //
        // Parameters:
        //   message:
        internal void _CreateProgressItem(string message)
        {
            CreateProgressItem(message);
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
        internal Guid _GetProcessId(string name, bool isDialog)
        {
            return GetProcessId(name, isDialog);
        }


        //
        // Summary:
        //     Checks to see if the Team is already associated to the requested security role.
        //
        // Parameters:
        //   guTeamId:
        //
        //   guRoleId:
        internal bool _IsRoleAssociatedWithTeam(Guid guTeamId, Guid guRoleId)
        {
            return IsRoleAssoicatedWithTeam(guTeamId, guRoleId);
        }


        //
        // Summary:
        //     Is a Workflow Active.
        //
        // Parameters:
        //   wfId:
        internal bool _IsWorkflowActive(Guid wfId)
        {
            return IsWorkflowActive(wfId);
        }

        //
        // Summary:
        //     Raised a failed event to the caller.
        //
        // Parameters:
        //   message:
        //
        //   Ex:
        internal void _RaiseFailEvent(string message, Exception Ex)
        {
            RaiseFailEvent(message, Ex);
        }

        //
        // Summary:
        //     Raise a Status updated event.
        //
        // Parameters:
        //   message:
        //
        //   status:
        internal void _RaiseUpdateEvent(string message, ProgressPanelItemStatus status)
        {
            RaiseUpdateEvent(message, status);
        }


        #endregion

        #region EnvironmentPrepMethods
        protected void SetPreferredSolution(Guid solutionId)
        {
            PackageLog.Log($"Attempting SetPreferredSolution request with solutionId {solutionId}");
            try
            {
                var msg = new OrganizationRequest("SetPreferredSolution");
                msg.Parameters.Add("SolutionId", solutionId);
            }
            catch (Exception ex)
            {
                PackageLog.Log($"Non-Fatal Exception attempting SetPreferredSolution request with {solutionId}", TraceEventType.Warning);
                PackageLog.Log(ex.ToString(), TraceEventType.Warning);
            }
        }
        protected void SetPreferredSolution(string solutionUniqueName)
        {
            PackageLog.Log($"Attempting to set preferred solution to {solutionUniqueName}");
            try
            {
                var qry = new QueryByAttribute("solution")
                {
                    ColumnSet = new ColumnSet("solutionid")
                };
                qry.AddAttributeValue("uniquename", solutionUniqueName);
                var result = CrmSvc.RetrieveMultiple(qry);
                if (result != null & result.Entities.Count > 0)
                {
                    SetPreferredSolution(result[0].Id);
                }
                else
                {
                    PackageLog.Log($"Unable to identify solution with unique name {solutionUniqueName}", TraceEventType.Warning);
                }
            }
            catch (Exception ex)
            {
                PackageLog.Log($"Non-Fatal Exception attempting to set preferred solution to {solutionUniqueName}", TraceEventType.Warning);
                PackageLog.Log(ex.ToString(), TraceEventType.Warning);
            }
        }
        #endregion

    }
}

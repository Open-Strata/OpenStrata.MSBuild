//using Microsoft.Uii.Common.Entities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Tooling.Dmt.DataMigCommon.DataModel.Schema;
using Microsoft.Xrm.Tooling.Dmt.DataMigCommon.Utility;
using Microsoft.Xrm.Tooling.Dmt.ImportProcessor.DataInteraction;
//using Microsoft.Xrm.Tooling.Dmt.ImportProcessor.UserMapper;
using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageCore.ImportCode;
using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageCore.Models;
using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenStrata.Deployment.Sdk.Common.ConfigData
{
    [Export(typeof(IImportPackageStrataFeatureExtension))]
    public class ConfigDataExtension : PackageStrataExtensionBase, IImportPackageStrataFeatureExtension
    {

        //private readonly CoreObjects _coreData;

        private Dictionary<StatusEventUpdate, ProgressDataItem> progressEventLinks;
        private ImportCrmDataHandler _parser;
        private ProgressDataItemCore CurrentProgress;
        private bool proceedwithImport;
        private Stopwatch deploymentTimer = new Stopwatch();
        private Stopwatch userCodeTimer = new Stopwatch();
        private string workingImportFolder = string.Empty;

        private List<string> ImportedSolutionsList = new List<string>();

        private IPackageSolutionMapper packageSolutionMapper;

        public event EventHandler<ProdgressDataItemEventArgs> AddNewProgressItem;
        public event EventHandler<ProdgressDataItemEventArgs> UpdateProgressItem;
        public event EventHandler<ImportProgressStatus> ImportComplete;
        public event EventHandler<ImportProgressStatus> ImportStatusUpdate;

        protected override bool AppliesToThisSolution(string solutionName)
        {
            return true;
        }

        protected override void PreSolutionImport(string solutionName, bool solutionOverwriteUnmanagedCustomizations, bool solutionPublishWorkflowsAndActivatePlugins, out bool overwriteUnmanagedCustomizations, out bool publishWorkflowsAndActivatePlugins)
        {
            base.PreSolutionImport(solutionName, solutionOverwriteUnmanagedCustomizations, solutionPublishWorkflowsAndActivatePlugins, out overwriteUnmanagedCustomizations, out publishWorkflowsAndActivatePlugins);

            ImportedSolutionsList.Add(solutionName);

        }

        protected override bool AfterPrimaryImport()
        {

            PackageLog.Log($"OpenStrata : ConfigData : Checking for OpenStrata config data for {ImportedSolutionsList.Count} solutions");

            foreach (string solution in ImportedSolutionsList)
            {

                var cdFiles = GetApplicableConfigData(solution);

                PackageLog.Log($"OpenStrata : ConfigData : Identified {cdFiles.Count} config data for solution {solution}");

                foreach (string cdFile in cdFiles)
                {
                    
                    var cdfPath = Path.Combine(this.AbsoluteImportPackageDataFolderPath, "ConfigData", cdFile);

                    if (!ExecuteDeepDataImportActions(cdfPath))
                    {
                        //TODO:  do some other stuff here...
                        return false;
                    }
                }
            }
            return true;
        }


        private List<string> GetApplicableConfigData(string solution)
        {

            var configDataFiles = new List<string>();

            var items = ImportStrataManifest.Root.Descendants("DataverseSolutionFile")
                                 .Where(dsf => dsf.Attribute("UniqueName").Value == solution)
                                 ?.FirstOrDefault()
                                 ?.Ancestors("StratiManifest")
                                 ?.FirstOrDefault()
                                 ?.Element("ConfigDataPackages")
                                 ?.Elements("ConfigDataPackage")
                                 ?.OrderBy(cdp => int.Parse(cdp.Attribute("LocalImportSequence").Value));

            if (items != null)
            {
                foreach (XElement configDataElement in items)
                {
                    configDataFiles.Add(configDataElement.Attribute("FileName").Value);
                }
            }

            return configDataFiles;

        }

        #region ConfigData Import Methods

        private bool ExecuteDeepDataImportActions(string configDataPath)
        {
           // this.logger.Log(nameof(ExecuteDeepDataImportActions), TraceEventType.Start);

           // if (!string.IsNullOrWhiteSpace(this._coreData.ImportConfigInfo.CrmMigDataImportFile))
            {
                //this.CreateProgressItem(LocalResourcesCore.CREATEPROGRESSITEM_RUNNINGCOMPLEXDATAIMPORT_MSG, PDStage.DataImport);
                
                
               // string path1 = Path.Combine(this._coreData.SelectedPluginPath, this._coreData.InstallerCustomizedExentions.selectedPlugin.GetImportPackageFolderName);
                
                
                string str1 = configDataPath;
                string str2 = string.Empty;


               //  MappingConfiguration mappingConfiguration = (MappingConfiguration)null;

                TimeSpan elapsed;
                //if (this._coreData.ImportConfigInfo.CMTFilesForImport != null && this._coreData.ImportConfigInfo.CMTFilesForImport.Count > 0)
                //{
                //    CrmServiceClient crmSvc = this.crmSvc;
                //    List<CrmServiceClient.CrmSearchFilter> searchParameters = new List<CrmServiceClient.CrmSearchFilter>();
                //    List<string> fieldList = new List<string>();
                //    fieldList.Add("languagecode");
                //    Guid batchId = new Guid();
                //    Dictionary<string, Dictionary<string, object>> dataBySearchParams = crmSvc.GetEntityDataBySearchParams("organization", searchParameters, CrmServiceClient.LogicalSearchOperator.None, fieldList, batchId);
                //    if (dataBySearchParams != null && dataBySearchParams.FirstOrDefault<KeyValuePair<string, Dictionary<string, object>>>().Value != null)
                //    {
                //        int LCIDOfOrganization = this.crmSvc.GetDataByKeyFromResultsSet<int>(dataBySearchParams.FirstOrDefault<KeyValuePair<string, Dictionary<string, object>>>().Value, "languagecode");
                //        if (LCIDOfOrganization != 0)
                //        {
                //            IEnumerable<ConfigMigrationImportFile> source1 = this._coreData.ImportConfigInfo.CMTFilesForImport.Where<ConfigMigrationImportFile>((Func<ConfigMigrationImportFile, bool>)(w => w.LCID == LCIDOfOrganization));
                //            if (this.iEx is IImportExtensions2)
                //            {
                //                try
                //                {
                //                    if (this.iEx != null && this._coreData.allowCustomCode)
                //                    {
                //                        this.userCodeTimer.Restart();
                //                        List<int> list = this._coreData.ImportConfigInfo.CMTFilesForImport.Select<ConfigMigrationImportFile, int>((Func<ConfigMigrationImportFile, int>)(s => s.LCID)).Distinct<int>().ToList<int>();
                //                        this.logger.Log("User Code Execution : OP=OverrideConfigurationDataFileLanguage : Status=Execute");
                //                        int selectedLCID = ((IImportExtensions2)this.iEx).OverrideConfigurationDataFileLanguage(LCIDOfOrganization, list);
                //                        if (selectedLCID != 0)
                //                        {
                //                            IEnumerable<ConfigMigrationImportFile> source2 = this._coreData.ImportConfigInfo.CMTFilesForImport.Where<ConfigMigrationImportFile>((Func<ConfigMigrationImportFile, bool>)(w => w.LCID == selectedLCID));
                //                            if (source2.Count<ConfigMigrationImportFile>() > 0)
                //                                source1 = source2;
                //                        }
                //                        this.logger?.ResetLastError();
                //                        this.userCodeTimer.Stop();
                //                        Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase.TraceLogger logger = this.logger;
                //                        elapsed = this.userCodeTimer.Elapsed;
                //                        string message = string.Format("User Code Execution : OP=OverrideConfigurationDataFileLanguage : Status=Complete : Duration={0}", (object)elapsed.ToString());
                //                        logger.Log(message);
                //                    }
                //                    else
                //                        this.logger.Log("User Code Execution : OP=OverrideConfigurationDataFileLanguage : Status=Disallowed custom code execution", TraceEventType.Warning);
                //                }
                //                catch (Exception ex)
                //                {
                //                    if (this.userCodeTimer.IsRunning)
                //                        this.userCodeTimer.Stop();
                //                    this.logger.Log(string.Format("User Code Execution : OP=OverrideConfigurationDataFileLanguage : Status=Failed to execute OverrideConfigurationDataFileLanguage. Message: {0}", (object)ex.Message), TraceEventType.Error, ex);
                //                    this.proceedwithImport = false;
                //                    if (this.CurrentProgress != null)
                //                        this.RaiseFailEvent(this.CurrentProgress.ItemText, ex);
                //                    return false;
                //                }
                //            }
                //            if (source1.Count<ConfigMigrationImportFile>() > 0)
                //            {
                //                if (source1.Count<ConfigMigrationImportFile>() > 1)
                //                    this.logger.Log(string.Format((IFormatProvider)CultureInfo.CurrentUICulture, "More then one Configuration Migration data file found for LCID {0}. Using first one found", (object)LCIDOfOrganization), TraceEventType.Warning);
                //                string path = Path.Combine(path1, source1.FirstOrDefault<ConfigMigrationImportFile>().FileName);
                //                if (File.Exists(path))
                //                {
                //                    str1 = path;
                //                    if (!string.IsNullOrEmpty(source1.FirstOrDefault<ConfigMigrationImportFile>().UserMapFileName))
                //                    {
                //                        string str3 = Path.Combine(path1, source1.FirstOrDefault<ConfigMigrationImportFile>().UserMapFileName);
                //                        if (File.Exists(str3))
                //                        {
                //                            str2 = source1.FirstOrDefault<ConfigMigrationImportFile>().UserMapFileName;
                //                            try
                //                            {
                //                                mappingConfiguration = Helper.Deserialize<MappingConfiguration>(str3);
                //                            }
                //                            catch (Exception ex)
                //                            {
                //                                this.logger.Log(string.Format((IFormatProvider)CultureInfo.CurrentUICulture, "Failed to read user map at {0}. Message: {1}", (object)str3, (object)ex.Message), TraceEventType.Error, ex);
                //                            }
                //                        }
                //                        else
                //                            this.logger.Log(string.Format((IFormatProvider)CultureInfo.CurrentUICulture, "UserMap Specified but not found at {0}. Skipping.", (object)str3), TraceEventType.Warning);
                //                    }
                //                    this.logger.Log(string.Format((IFormatProvider)CultureInfo.CurrentUICulture, "Selected LCID:{0} - Data Import File:{1}{2}", (object)LCIDOfOrganization, (object)str1, string.IsNullOrEmpty(str2) ? (object)"" : (object)string.Format((IFormatProvider)CultureInfo.CurrentUICulture, " - User Map to Use:{0}", (object)str2)), TraceEventType.Information);
                //                }
                //                else
                //                    this.logger.Log(string.Format((IFormatProvider)CultureInfo.CurrentUICulture, "Language Specific Data Import requested by File cannot be found at {0}. Failing over to default", (object)path), TraceEventType.Warning);
                //            }
                //        }
                //    }
                //}
                if (!File.Exists(str1))
                {
                    this.PackageLog.Log(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "ExecuteDeepDataImportActions : The complex data import failed because a data file is missing. Could not find Import File : {0}", str1), TraceEventType.Error, (Exception)null);
                    this.RaiseFailEvent(string.Format((IFormatProvider)CultureInfo.CurrentUICulture, "The complex data import failed because a data file is missing."));
                    this.PackageLog.Log(nameof(ExecuteDeepDataImportActions), TraceEventType.Stop);
                    return false;
                }
                if (this.progressEventLinks == null)
                    this.progressEventLinks = new Dictionary<StatusEventUpdate, ProgressDataItem>();
                this.progressEventLinks.Clear();
                if (this._parser != null)
                    this._parser = (ImportCrmDataHandler)null;
                this._parser = new ImportCrmDataHandler();
                this._parser.Logger.LogRetentionDuration = this.PackageLog.LogRetentionDuration;
                this._parser.Logger.EnabledInMemoryLogCapture = this.PackageLog.EnabledInMemoryLogCapture;
                this._parser.AddNewProgressItem += new EventHandler<ProgressItemEventArgs>(this._parser_AddNewProgressItem);
                this._parser.UpdateProgressItem += new EventHandler<ProgressItemEventArgs>(this._parser_UpdateProgressItem);
                this._parser.CrmConnection = this.CrmSvc;
                this._parser.ImportConnections =  new Dictionary<int, CrmServiceClient>();
                this._parser.ControlDispatcher = this.RootControlDispatcher;
                bool? enableBatchMode = this.EnableBatchMode;
                if (enableBatchMode.HasValue)
                {
                    ImportCrmDataHandler parser = this._parser;
                    enableBatchMode = this.EnableBatchMode;
                    int num = enableBatchMode.Value ? 1 : 0;
                    parser.EnabledBatchMode = num != 0;
                }
                int? nullable = this.PrefetchRecordLimitCount;
                if (nullable.HasValue)
                {
                    ImportCrmDataHandler parser = this._parser;
                    nullable = this.PrefetchRecordLimitCount;
                    int num = nullable.Value;
                    parser.PrefetchRecordLimitSize = num;
                }
                nullable = this.RequestedBatchSize;
                if (nullable.HasValue)
                {
                    ImportCrmDataHandler parser = this._parser;
                    nullable = this.RequestedBatchSize;
                    int num = nullable.Value;
                    parser.RequestedBatchSize = num;
                }
                
                // It is IImportExtensions2
              //  if (this.iEx is IImportExtensions2)
               // {
                    try
                    {
                        //if (this.iEx != null && this._coreData.allowCustomCode)
                       // {
                            this.userCodeTimer.Restart();
                            this.PackageLog.Log("User Code Execution : OP=OverrideDataImportSafetyChecks : Status=Execute");
                            this._parser.OverrideDataImportSafetyChecks = this.OverrideDataImportSafetyChecks;
                            this.PackageLog?.ResetLastError();
                            this.userCodeTimer.Stop();
                            Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase.TraceLogger logger = this.PackageLog;
                            elapsed = this.userCodeTimer.Elapsed;
                            string message = string.Format("User Code Execution : OP=OverrideDataImportSafetyChecks : Status=Complete : Duration={0}", (object)elapsed.ToString());
                            logger.Log(message);
                       // }
                      //  else
                       //     this.logger.Log("User Code Execution : OP=OverrideDataImportSafetyChecks : Status=Disallowed custom code execution", TraceEventType.Warning);
                    }
                    catch (Exception ex)
                    {
                        if (this.userCodeTimer.IsRunning)
                            this.userCodeTimer.Stop();
                        this.PackageLog.Log(string.Format("User Code Execution : OP=OverrideDataImportSafetyChecks : Status=Failed to execute OverrideDataImportSafetyChecks. {0}", string.IsNullOrEmpty(ex.Message) ? (object)"" : (object)(" Message: " + ex.Message)), TraceEventType.Error, ex);
                        this.proceedwithImport = false;
                        if (this.CurrentProgress != null)
                            this.RaiseFailEvent(this.CurrentProgress.ItemText, ex);
                        return false;
                    }
               // }
               // this._parser.UserMap = mappingConfiguration ?? new MappingConfiguration();
                this.workingImportFolder = string.Empty;
                Exception exception;
                if (ImportCrmDataHandler.CrackZipFileAndCheckContents(str1, this.workingImportFolder, out this.workingImportFolder, out exception))
                {
                    if (!this._parser.ValidateSchemaFile(this.workingImportFolder))
                    {
                        ProdgressDataItemEventArgs dataItemEventArgs = new ProdgressDataItemEventArgs();
                        ProgressDataItemCore progressDataItemCore = new ProgressDataItemCore();
                        progressDataItemCore.ItemStatus = ProgressPanelItemStatus.Failed;
                        progressDataItemCore.ItemText = "The complex data import can't continue. The file schema is invalid.";
                        dataItemEventArgs.progressItem = (ProgressDataItem)progressDataItemCore;
                        ProdgressDataItemEventArgs e = dataItemEventArgs;
                        EventHandler<ProdgressDataItemEventArgs> addNewProgressItem = this.AddNewProgressItem;
                        if (addNewProgressItem != null)
                            addNewProgressItem((object)this, e);
                        this._parser.ForceTerminateImport();
                        this.PackageLog.Log("ExecuteDeepDataImportActions : The complex data import failed because the schema is invalid.", TraceEventType.Error, (Exception)null);
                        this.RaiseFailEvent(string.Format((IFormatProvider)CultureInfo.CurrentUICulture, "The complex data import failed because the schema is invalid."));
                        return false;
                    }
                   // if (this.iEx is IImportExtensions4)
                    //{
                        try
                        {
                           // if (this.iEx != null && this._coreData.allowCustomCode)
                           // {
                                this.userCodeTimer.Restart();
                                this.PackageLog.Log("User Code Execution : OP=OverrideDateMode : Status=Execute");
                                this._parser.OverrideDateMode = ConvertImportExtensionDateModeToSchemaDateMode(this.OverrideDateMode);
                                this.PackageLog?.ResetLastError();
                                this.userCodeTimer.Stop();
                                Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase.TraceLogger logger = this.PackageLog;
                                elapsed = this.userCodeTimer.Elapsed;
                                string message = string.Format("User Code Execution : OP=OverrideDateMode : Status=Complete : Duration={0}", (object)elapsed.ToString());
                                logger.Log(message);
                           // }
                          //  else
                            //    this.PackageLog.Log("User Code Execution : OP=OverrideDateMode : Status=Disallowed custom code execution", TraceEventType.Warning);
                        }
                        catch (Exception ex)
                        {
                            if (this.userCodeTimer.IsRunning)
                                this.userCodeTimer.Stop();
                            this.PackageLog.Log(string.Format("User Code Execution : OP=OverrideDateMode : Status=Failed to read OverrideDateMode Custom Setting. Message:{0}", (object)ex.Message), TraceEventType.Error, ex);
                            this.proceedwithImport = false;
                            if (this.CurrentProgress != null)
                                this.RaiseFailEvent(this.CurrentProgress.ItemText, ex);
                            return false;
                        }
                        try
                        {
                           // if (this.iEx != null && this._coreData.allowCustomCode)
                           // {
                                this.userCodeTimer.Restart();
                                this.PackageLog.Log("User Code Execution : OP=OverrideDataTimestamp : Status=Execute");
                                this._parser.OverrideDataTimestamp = this.OverrideDataTimestamp;
                                this.PackageLog?.ResetLastError();
                                this.userCodeTimer.Stop();
                                Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase.TraceLogger logger = this.PackageLog;
                                elapsed = this.userCodeTimer.Elapsed;
                                string message = string.Format("User Code Execution : OP=OverrideDataTimestamp : Status=Complete : Duration={0}", (object)elapsed.ToString());
                                logger.Log(message);
                           // }
                           // else
                            //    this.PackageLog.Log("User Code Execution : OP=OverrideDataTimestamp : Status=Disallowed custom code execution", TraceEventType.Warning);
                        }
                        catch (Exception ex)
                        {
                            if (this.userCodeTimer.IsRunning)
                                this.userCodeTimer.Stop();
                            this.PackageLog.Log(string.Format("User Code Execution : OP=OverrideDataTimestamp : Status=Failed to read OverrideDataTimestamp Custom Setting. Message:{0}", (object)ex.Message), TraceEventType.Error, ex);
                            this.proceedwithImport = false;
                            if (this.CurrentProgress != null)
                                this.RaiseFailEvent(this.CurrentProgress.ItemText, ex);
                            return false;
                        }
                   // }
                    int num = this._parser.ImportDataToCrm(this.workingImportFolder, false) ? 1 : 0;
                    bool result = false;
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("SuppressImportLog")))
                        bool.TryParse(ConfigurationManager.AppSettings.Get("SuppressImportLog"), out result);
                    if (!result)
                        this._parser.ImportLog.WriteOutLogToFile("ComplexImportDetailLog.log", new Microsoft.Xrm.Tooling.Dmt.DataMigCommon.Utility.TraceLogger(string.Empty));
                    if (num != 0)
                    {
                        this.RaiseUpdateEvent(string.Format((IFormatProvider)CultureInfo.CurrentUICulture, "The complex data import is complete."), ProgressPanelItemStatus.Complete);
                    }
                    else
                    {
                        this.PackageLog.Log("The complex data import is failed", TraceEventType.Error, (Exception)null);
                        this.RaiseFailEvent(string.Format((IFormatProvider)CultureInfo.CurrentUICulture, "The complex data import is complete."));
                    }
                    this.PackageLog.Log(nameof(ExecuteDeepDataImportActions), TraceEventType.Stop);
                    this._parser.ForceTerminateImport();
                    return num != 0;
                }
                this.PackageLog.Log(string.Format("Failed to uncompress zip file {0} to {1}. {2}", (object)str1, (object)this.workingImportFolder, exception == null ? (object)"" : (object)("Message: " + exception.Message)), TraceEventType.Error, exception);
                ProdgressDataItemEventArgs dataItemEventArgs1 = new ProdgressDataItemEventArgs();
                ProgressDataItemCore progressDataItemCore1 = new ProgressDataItemCore();
                progressDataItemCore1.ItemStatus = ProgressPanelItemStatus.Failed;
                progressDataItemCore1.ItemText = "The complex data import failed because the file can't be found, or the compressed (.zip) file is invalid.";
                dataItemEventArgs1.progressItem = (ProgressDataItem)progressDataItemCore1;
                ProdgressDataItemEventArgs e1 = dataItemEventArgs1;
                EventHandler<ProdgressDataItemEventArgs> addNewProgressItem1 = this.AddNewProgressItem;
                if (addNewProgressItem1 != null)
                    addNewProgressItem1((object)this, e1);
                this.RaiseFailEvent(string.Format((IFormatProvider)CultureInfo.CurrentUICulture, "RAISEUPDATEVENT_COMPLEXDATEIMPORTFAILED_INVALIDZIPFILE_MSG"));
                this.PackageLog.Log(nameof(ExecuteDeepDataImportActions), TraceEventType.Stop);
                this._parser.ForceTerminateImport();
                return false;
            }
            this.PackageLog.Log(nameof(ExecuteDeepDataImportActions), TraceEventType.Stop);
            return true;
        }

        private void _parser_AddNewProgressItem(object sender, ProgressItemEventArgs e)
        {
            this.PackageLog.Log("AddNewProgressItem", TraceEventType.Start);
            bool flag = true;
            ProgressDataItem progressDataItem = (ProgressDataItem)new ProgressDataItemCore();
            progressDataItem.ItemText = e.progressItem.ItemText;
            switch (e.progressItem.ItemStatus)
            {
                case ProgressItemStatus.Working:
                    progressDataItem.ItemStatus = ProgressPanelItemStatus.Working;
                    flag = false;
                    break;
                case ProgressItemStatus.Complete:
                    progressDataItem.ItemStatus = ProgressPanelItemStatus.Complete;
                    break;
                case ProgressItemStatus.Failed:
                    progressDataItem.ItemStatus = ProgressPanelItemStatus.Failed;
                    break;
                case ProgressItemStatus.Warning:
                    progressDataItem.ItemStatus = ProgressPanelItemStatus.Warning;
                    break;
                default:
                    progressDataItem.ItemStatus = ProgressPanelItemStatus.Unknown;
                    break;
            }
            if (!flag)
                this.progressEventLinks.Add(e.progressItem, progressDataItem);
            EventHandler<ProdgressDataItemEventArgs> addNewProgressItem = this.AddNewProgressItem;
            if (addNewProgressItem != null)
                addNewProgressItem((object)this, new ProdgressDataItemEventArgs()
                {
                    progressItem = progressDataItem
                });
            this.PackageLog.Log("AddNewProgressItem", TraceEventType.Stop);
        }


        private void _parser_UpdateProgressItem(object sender, ProgressItemEventArgs e)
        {
            this.PackageLog.Log("UpdateProgressItem", TraceEventType.Start);
            ProgressDataItem progressDataItem = (ProgressDataItem)null;
            if (this.progressEventLinks.ContainsKey(e.progressItem))
                progressDataItem = this.progressEventLinks[e.progressItem];
            bool flag = false;
            if (progressDataItem != null)
            {
                progressDataItem.ItemText = e.progressItem.ItemText;
                switch (e.progressItem.ItemStatus)
                {
                    case ProgressItemStatus.Working:
                        progressDataItem.ItemStatus = ProgressPanelItemStatus.Working;
                        break;
                    case ProgressItemStatus.Complete:
                        progressDataItem.ItemStatus = ProgressPanelItemStatus.Complete;
                        flag = true;
                        break;
                    case ProgressItemStatus.Failed:
                        progressDataItem.ItemStatus = ProgressPanelItemStatus.Failed;
                        flag = true;
                        break;
                    case ProgressItemStatus.Warning:
                        progressDataItem.ItemStatus = ProgressPanelItemStatus.Warning;
                        break;
                    default:
                        progressDataItem.ItemStatus = ProgressPanelItemStatus.Unknown;
                        break;
                }
            }
            if (progressDataItem != null && this.UpdateProgressItem != null)
                this.UpdateProgressItem((object)this, new ProdgressDataItemEventArgs()
                {
                    progressItem = progressDataItem
                });
            if (flag)
                this.progressEventLinks.Remove(e.progressItem);
            this.PackageLog.Log("UpdateProgressItem", TraceEventType.Stop);
        }


        protected void RaiseFailEvent(
          string message,
          Exception ex = null,
          ErrorDetail errorDetails = null,
          PDStage stage = PDStage.Unknown)
        {
            this.PackageLog.Log(nameof(RaiseFailEvent), TraceEventType.Start);
            this.cleanupFolder(this.workingImportFolder, true);
            message = string.IsNullOrEmpty(message) ? "No Error Message Provided" : message;
            if (ex == null)
                ex = new Exception(message);
            if (this.CurrentProgress == null)
                this.CreateProgressItem(string.Format((IFormatProvider)CultureInfo.CurrentUICulture, message, (object)ex.Message), stage);
            this.CurrentProgress.ItemStatus = ProgressPanelItemStatus.Failed;
            this.CurrentProgress.ItemText = string.Format((IFormatProvider)CultureInfo.CurrentUICulture, message, (object)ex.Message);
            if (stage != PDStage.Unknown)
                this.CurrentProgress.DeploymentStage = stage;
            if (errorDetails == null)
                errorDetails = this.ExceptionToErrorDetail(this.PackageLog.LastException);
            this.CurrentProgress.ErrorDetails = errorDetails;
            this.LogErrorDetails((IErrorDetail)this.CurrentProgress.ErrorDetails);
            this.PackageLog.Log("RaiseFailEvent - update progress with fail event");
            EventHandler<ProdgressDataItemEventArgs> updateProgressItem = this.UpdateProgressItem;
            if (updateProgressItem != null)
                updateProgressItem((object)this, new ProdgressDataItemEventArgs()
                {
                    progressItem = (ProgressDataItem)this.CurrentProgress
                });
            //try
            //{
            //    this.ValidateAndQueueRibbonGenerationJob(false);
            //}
            //catch (Exception ex1)
            //{
            //    this.PackageLog.Log(string.Format("ValidateAndQueueRibbonGenerationJob failed with exception: {0}", (object)ex1));
            //}
            this.RaiseImportComplete(false, message, stage);
            this.PackageLog.Log(nameof(RaiseFailEvent), TraceEventType.Stop);
        }

        private void cleanupFolder(string folderPath, bool cleanSubFolders)
        {
            try
            {
                if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
                    return;
                this.PackageLog.Log(string.Format("Attempting to delete folder: {0} - cleanSubFolders: {1}", (object)folderPath, (object)cleanSubFolders), TraceEventType.Verbose);
                Directory.Delete(folderPath, cleanSubFolders);
            }
            catch (Exception ex)
            {
                this.PackageLog.Log(string.Format("Attempt to delete folder failed - {0}", (object)ex.Message), TraceEventType.Error, ex);
            }
        }

        protected void CreateProgressItem(string message, PDStage stage = PDStage.Unknown)
        {
            this.PackageLog.Log(nameof(CreateProgressItem), TraceEventType.Start);
            ProgressDataItemCore progressDataItemCore = new ProgressDataItemCore();
            progressDataItemCore.ItemStatus = ProgressPanelItemStatus.Working;
            progressDataItemCore.ItemText = message;
            progressDataItemCore.DeploymentStage = stage;
            this.CurrentProgress = progressDataItemCore;
            EventHandler<ProdgressDataItemEventArgs> addNewProgressItem = this.AddNewProgressItem;
            if (addNewProgressItem != null)
                addNewProgressItem((object)this, new ProdgressDataItemEventArgs()
                {
                    progressItem = (ProgressDataItem)this.CurrentProgress
                });
            this.PackageLog.Log(nameof(CreateProgressItem), TraceEventType.Stop);
        }

        private void RaiseImportComplete(bool isComplete, string message = null, PDStage stage = PDStage.Unknown)
        {
            this.PackageLog.Log(nameof(RaiseImportComplete), TraceEventType.Start);
            this.cleanupFolder(this.workingImportFolder, true);
            EventHandler<ImportProgressStatus> importComplete = this.ImportComplete;
            if (importComplete != null)
                importComplete((object)this, new ImportProgressStatus()
                {
                    isCompleted = isComplete,
                    DeploymentStage = stage,
                    StatusMessage = message
                });
            this.packageSolutionMapper?.CompletePackageImport(isComplete);
            if (this.deploymentTimer.IsRunning)
                this.deploymentTimer.Stop();
            this.PackageLog.Log(string.Format("****** PACKAGE DEPLOYMENT PROCESS COMPLETED. Result:{1}  Duration:{0} ******", (object)this.deploymentTimer.Elapsed.ToString(), isComplete ? (object)"SUCCESS" : (object)"FAILED"));
            this.PackageLog.Log(nameof(RaiseImportComplete), TraceEventType.Stop);
        }

        private void LogErrorDetails(IErrorDetail errorDetails)
        {
            if (errorDetails == null)
            {
                this.PackageLog.Log("errorDetails are not available", TraceEventType.Information);
            }
            else
            {
                this.PackageLog.Log(string.Format("errorDetails - ErrorCode  - {0}", (object)errorDetails.ErrorCode), TraceEventType.Information);
                this.PackageLog.Log(string.Format("errorDetails - ErrorName  - {0}", (object)errorDetails.ErrorName), TraceEventType.Information);
                this.PackageLog.Log(string.Format("errorDetails - Message  - {0}", string.IsNullOrEmpty(errorDetails.Message) ? (object)"" : (object)Regex.Replace(errorDetails.Message, "\\t|\\n|\\r", "...")), TraceEventType.Information);
                this.PackageLog.Log(string.Format("errorDetails - Type  - {0}", (object)errorDetails.Type), TraceEventType.Information);
                this.PackageLog.Log(string.Format("errorDetails - StatusCode  - {0}", (object)errorDetails.StatusCode), TraceEventType.Information);
                this.PackageLog.Log(string.Format("errorDetails - Source  - {0}", (object)errorDetails.Source), TraceEventType.Information);
                this.PackageLog.Log(string.Format("errorDetails - TraceText  - {0}", string.IsNullOrEmpty(errorDetails.TraceText) ? (object)"" : (object)Regex.Replace(errorDetails.TraceText, "\\t|\\n|\\r", "...")), TraceEventType.Information);
                if (errorDetails.InnerErrorDetail == null)
                    return;
                this.LogErrorDetails((IErrorDetail)errorDetails.InnerErrorDetail);
            }
        }

        private ErrorDetail ExceptionToErrorDetail(Exception ex)
        {
            ErrorDetail errorDetail = new ErrorDetail();
            if (ex == null)
                return errorDetail;
            this.PackageLog.Log(string.Format("package deployment failure exception type : {0}", (object)ex.GetType()), TraceEventType.Verbose);
            if (ex is FaultException<OrganizationServiceFault>)
            {
                this.PackageLog.Log("converting CRM exception to error details", TraceEventType.Verbose);
                FaultException<OrganizationServiceFault> faultException = (FaultException<OrganizationServiceFault>)ex;
                errorDetail.ErrorCode = new int?(faultException.Detail.ErrorCode);
                errorDetail.ErrorName = faultException.Detail.ErrorDetails == null || faultException.Detail.ErrorDetails.Count <= 0 || !faultException.Detail.ErrorDetails.ContainsKey("ApiExceptionMesageName") ? "NotProvided" : faultException.Detail.ErrorDetails["ApiExceptionMesageName"].ToString();
                errorDetail.Message = faultException.Detail.Message;
                errorDetail.Type = faultException.Detail.ErrorDetails == null || faultException.Detail.ErrorDetails.Count <= 0 || !faultException.Detail.ErrorDetails.ContainsKey("ApiExceptionCategory") ? "NotProvided" : faultException.Detail.ErrorDetails["ApiExceptionCategory"].ToString();
                errorDetail.StatusCode = faultException.Detail.ErrorDetails == null || faultException.Detail.ErrorDetails.Count <= 0 || !faultException.Detail.ErrorDetails.ContainsKey("ApiExceptionHttpStatusCode") ? 500 : (int)faultException.Detail.ErrorDetails["ApiExceptionHttpStatusCode"];
                errorDetail.InnerErrorDetail = (ErrorDetail)null;
                errorDetail.TraceText = string.IsNullOrEmpty(faultException.StackTrace) ? "" : faultException.StackTrace;
                errorDetail.Source = "PackageDeployer";
            }
            //else if (ex.GetType() == typeof(PackageDeployerCoreException))
            //{
            //    this.PackageLog.Log("converting PackageDeployerCoreException to error details", TraceEventType.Verbose);
            //    PackageDeployerCoreException deployerCoreException = (PackageDeployerCoreException)ex;
            //    errorDetail.ErrorCode = deployerCoreException.ErrorCode;
            //    errorDetail.ErrorName = string.IsNullOrEmpty(deployerCoreException.ErrorName) ? "NotProvided" : deployerCoreException.ErrorName;
            //    errorDetail.Message = string.IsNullOrEmpty(deployerCoreException.Message) ? "NotProvided" : deployerCoreException.Message;
            //    errorDetail.Type = deployerCoreException.Type;
            //    errorDetail.StatusCode = deployerCoreException.ErrorStatusCode;
            //    errorDetail.InnerErrorDetail = deployerCoreException.InnerException == null ? (ErrorDetail)null : this.ExceptionToErrorDetail(deployerCoreException.InnerException);
            //    errorDetail.TraceText = string.IsNullOrEmpty(ex.StackTrace) ? "" : ex.StackTrace;
            //    errorDetail.Source = "PackageDeployer";
            //}
            else if (ex.GetType() == typeof(PackageDeployerException))
            {
                this.PackageLog.Log("converting PackageDeployerException to error details", TraceEventType.Verbose);
                PackageDeployerException deployerException = (PackageDeployerException)ex;
                errorDetail.ErrorCode = deployerException.ErrorCode;
                errorDetail.ErrorName = string.IsNullOrEmpty(deployerException.ErrorName) ? "NotProvided" : deployerException.ErrorName;
                errorDetail.Message = string.IsNullOrEmpty(deployerException.Message) ? "NotProvided" : deployerException.Message;
                errorDetail.Type = deployerException.ErrorType.ToString();
                errorDetail.StatusCode = deployerException.ErrorStatusCode;
                errorDetail.InnerErrorDetail = deployerException.InnerException == null ? (ErrorDetail)null : this.ExceptionToErrorDetail(deployerException.InnerException);
                errorDetail.TraceText = string.IsNullOrEmpty(ex.StackTrace) ? "" : ex.StackTrace;
                errorDetail.Source = "PackageDeployer";
            }
            else
            {
                this.PackageLog.Log("converting exception to error details", TraceEventType.Verbose);
                errorDetail.ErrorCode = new int?(ex.HResult);
                errorDetail.ErrorName = "NotProvided";
                errorDetail.Message = ex.Message;
                errorDetail.InnerErrorDetail = ex.InnerException == null ? (ErrorDetail)null : this.ExceptionToErrorDetail(ex.InnerException);
                errorDetail.Type = "Application";
                errorDetail.StatusCode = 500;
                errorDetail.TraceText = string.IsNullOrEmpty(ex.StackTrace) ? "" : ex.StackTrace;
                errorDetail.Source = "PackageDeployer";
            }
            if (errorDetail.Type == "SystemFailure")
                errorDetail.Type = "Platform";
            else if (errorDetail.Type == "NonPlatformFailure")
                errorDetail.Type = "Client";
            else if (errorDetail.Type == "Timeout")
                errorDetail.Type = "Platform";
            return errorDetail;
        }

        protected void RaiseUpdateEvent(string message, ProgressPanelItemStatus status, PDStage stage = PDStage.Unknown)
        {
            this.PackageLog.Log(nameof(RaiseUpdateEvent), TraceEventType.Start);
            if (this.CurrentProgress != null)
            {
                this.CurrentProgress.ItemStatus = status;
                this.CurrentProgress.ItemText = message;
                if (stage != PDStage.Unknown)
                    this.CurrentProgress.DeploymentStage = stage;
                if (status == ProgressPanelItemStatus.Failed || status == ProgressPanelItemStatus.Complete)
                    this.cleanupFolder(this.workingImportFolder, true);
                EventHandler<ProdgressDataItemEventArgs> updateProgressItem = this.UpdateProgressItem;
                if (updateProgressItem != null)
                    updateProgressItem((object)this, new ProdgressDataItemEventArgs()
                    {
                        progressItem = (ProgressDataItem)this.CurrentProgress
                    });
            }
            this.PackageLog.Log(nameof(RaiseUpdateEvent), TraceEventType.Stop);
        }

        private static DateMode? ConvertImportExtensionDateModeToSchemaDateMode(
                DataMigrationDateMode? dateMode)
        {
            if (!dateMode.HasValue)
                return new DateMode?();
            DataMigrationDateMode? nullable = dateMode;
            DataMigrationDateMode migrationDateMode1 = DataMigrationDateMode.Absolute;
            if (nullable.GetValueOrDefault() == migrationDateMode1 & nullable.HasValue)
                return new DateMode?(DateMode.absolute);
            nullable = dateMode;
            DataMigrationDateMode migrationDateMode2 = DataMigrationDateMode.Relative;
            if (nullable.GetValueOrDefault() == migrationDateMode2 & nullable.HasValue)
                return new DateMode?(DateMode.relative);
            throw new ArgumentOutOfRangeException(nameof(dateMode), (object)dateMode, string.Format("Unable to map '{0}' to a schema DateMode", (object)dateMode));
        }

        #endregion

    }
}

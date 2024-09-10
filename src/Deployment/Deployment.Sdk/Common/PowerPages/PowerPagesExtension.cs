using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase;
using OpenStrata.Deployment.Sdk;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System;
using System.ComponentModel.Composition;
//using Microsoft.Xrm.Tooling.Dmt.DataMigCommon.Utility;
using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageCore.ImportCode;

namespace OpenStrata.Deployment.Sdk.Common.PowerPages
{
    [Export(typeof(IImportPackageStrataFeatureExtension))]
    public class PowerPagesExtension : PackageStrataExtensionBase, IImportPackageStrataFeatureExtension
    {

        //private readonly CoreObjects _coreData;

       // private Dictionary<StatusEventUpdate, ProgressDataItem> progressEventLinks;

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
    }
}
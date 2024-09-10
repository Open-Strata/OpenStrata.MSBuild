using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStrata.Deployment.Sdk
{
    public class ProgressDataItemManager
    {
        private ImportPackageStrataBase _importPackage;
        private ProgressDataItem _item { get; set; }

        private string _checkMessage;
        private object _lock = new object();
        private bool _itemCaptured = false;

        public ProgressDataItemManager(ImportPackageStrataBase importPackage, string message)
        {
            _importPackage = importPackage;
            _checkMessage = message;
            //Adding AddNewProgressItem event handler for the purpose
            //of capturing the ProgressDataItem object of the very next new
            //progress item.
            _importPackage.AddNewProgressItem += ProgressItemAdded;
            _importPackage._CreateProgressItem(message);
        }

        void ProgressItemAdded(object sender, ProdgressDataItemEventArgs e)
        {

            if (!_itemCaptured)
            {
            //using lock to unsure safe async execution.
            lock (_lock) { 


                //Checking to ensure the correct progressItem is captured.
                if (_checkMessage == e.progressItem.ItemText)
                {
                    //Capturing a reference to the progress item.
                    _item = e.progressItem;
                    _itemCaptured = true;
                    // imediately removing this method as a handler.
                    _importPackage.AddNewProgressItem -= ProgressItemAdded;
                }
            }
            }
        }

        public void UpdateWorkingMessage(string message)
        {
            _item.ItemText = message;
        }

        public void Complete(string message, bool withWarning = false)
        {
            _item.ItemStatus = withWarning ? ProgressPanelItemStatus.Warning : ProgressPanelItemStatus.Complete;
            _item.ItemText = message;
        }

        public void Failed(string message)
        {
            _item.ItemStatus = ProgressPanelItemStatus.Failed;
            _item.ItemText = message;
        }

    }
}

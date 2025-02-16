using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenStrata.Solution.Xml;
using System.Xml.Schema;

namespace OpenStrata.MSBuild.Solution.Tasks
{
    public class SetCanvasAppVersionToUTCNow : BaseTask
    {

        [Required]
        public ITaskItem[] CanvasAppMetaXmlFiles { get; set; }

        public override bool ExecuteTask()
        {
            foreach (var item in CanvasAppMetaXmlFiles)
            {
                try
                {

                    if (File.Exists(item.ItemSpec))
                    {
                        LogMessage($"SetCanvasAppVersionToUTCNow: Processing {item.ItemSpec}");

                        var Xdoc = CanvasAppMetaXDocument.Load(item.ItemSpec);

                        Xdoc.AppVersionDateTimeStamp = DateTime.UtcNow;

                        Xdoc.Save(item.ItemSpec);

                    }
                    else
                    {
                        LogMessage($"SetCanvasAppVersionToUTCNow: Skippin {item.ItemSpec} : File does not exist.");
                    }
                }
                catch (Exception ex) 
                {
                    this.Log.LogWarning($"SetCanvasAppVersionToUTCNow : Exception Processing {item.ItemSpec} : {ex.GetType()} : {ex.Message}");
                }
            }

            return true;
        }
    }
}

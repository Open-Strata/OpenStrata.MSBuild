using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using DocumentTemplates.Shared.Xml;

using OpenStrata.Xml;

using System.Xml.XPath;
using System.Text.RegularExpressions;
using DocumentTemplates.Shared;
using System.Linq;

namespace OpenStrata.MSBuild.DocumentTemplates.Tasks
{
    public class AnalyzeDocumentTemplates : BaseTask
    {

        [Required]
        public ITaskItem[] UnpackDocs { get; set; }

        public override bool ExecuteTask()
        {
            LogMessage($"Running AnalyzeDocumentTemplates Task; {UnpackDocs.Length} items identified.");

            foreach (ITaskItem item in UnpackDocs)
            {
                LogMessage($"Starting analysis of {item.ItemSpec}");

                AnalyzeTemplate(item);

                LogMessage($"Finished analysis of {item.ItemSpec}");



            }

            return true;
        }

        private void AnalyzeTemplate(ITaskItem item)
        {

            var extension = Path.GetExtension(item.ItemSpec).ToLower();

            if (extension == ".xlsx") AnalyzeXlsxTemplate(item);
            if (extension == ".docx") AnalyzeDocxTemplate(item);


        }

        private void AnalyzeXlsxTemplate(ITaskItem item)
        {

            LogMessage($"Starting analysis of Excel Document");

            var templateManifest = new DocumentTemplateManifestXDocument();

            var documentPath = item.ItemSpec;
            var manifestPath = documentPath + ".xml";
            var unpackPath = item.GetMetadata("UnpackPath");

            var customXmlFolder = Path.Combine(unpackPath, "xl", "worksheets");

            LogMessage($"enumerating files in {customXmlFolder}");

            var itemfiles = Directory.EnumerateFiles(customXmlFolder, "sheet*.xml");

            List<string> filesToInclude = new List<string>();

            //Regex docTempRX = new Regex(RegExHelper.DocumentTemplateRegExpression);
            //Regex schemaRefRX = new Regex(RegExHelper.SchemaRefRegExpression);

            //Regex entityUrn = new Regex(RegExHelper.entityuriExpression);

            LogMessage($"{itemfiles.Count()} found in {customXmlFolder}");

            foreach (var itemfile in itemfiles)
            {
                //    string itemxml = File.ReadAllText(itemfile);

                //    //var urnMatch = entityUrn.Match(itemxml);
                //    var dtmatches = docTempRX.Match(itemxml);

                LogMessage($"Attempting match in {itemfile}");

                //    //if (urnMatch.Success)
                //    //{
                //    //    templateManifest.EntityLogicalName = urnMatch.Groups["logicalName"].Value;
                //    //    templateManifest.EntityObjectTypeCode = int.Parse(urnMatch.Groups["objecttypecode"].Value);
                //    //    filesToInclude.Add(Path.GetFileName(itemfile));
                //    //}
                //    //else
                //    //{
                //    //   LogMessage($"No match found in {itemfile}");
                //    //}


                //    if (dtmatches.Success)
                //    {
                //        templateManifest.Schema = dtmatches.Groups["schema"].Value;
                if (String.IsNullOrEmpty(templateManifest.EntityLogicalName)) templateManifest.EntityLogicalName = "Temp";
                //        templateManifest.EntityObjectTypeCode = int.Parse(dtmatches.Groups["objecttypecode"].Value);
                //        templateManifest.ItemXml = Path.GetFileName(itemfile);
                //    }
                //    else
                //    {
                //        LogMessage($"2nd match attempt inin {itemfile}");

                //        var srmatches = schemaRefRX.Match(itemxml);
                //        if (srmatches.Success)
                //        {
                //            templateManifest.ItemPropsXml = Path.GetFileName(itemfile);
                //        }
                //        else
                //        {
                //            LogMessage($"Zero matches found in {itemfile}");
                //        }
                //    }
            }

            ////templateManifest.ItemXml = String.Join(";", filesToInclude);

            templateManifest.Save(manifestPath);
        }

        private void AnalyzeDocxTemplate(ITaskItem item)
        {
            var templateManifest = new DocumentTemplateManifestXDocument();

            var documentPath = item.ItemSpec;
            var manifestPath = documentPath + ".xml";
            var unpackPath = item.GetMetadata("UnpackPath");
            var customXmlFolder = Path.Combine(unpackPath, "customXml");

            LogMessage($"enumerating files in {customXmlFolder}");

            var itemfiles = Directory.EnumerateFiles(customXmlFolder, "item*.xml");

            List<string> filesToInclude = new List<string>();

            Regex docTempRX = new Regex(RegExHelper.DocumentTemplateRegExpression);
            Regex schemaRefRX = new Regex(RegExHelper.SchemaRefRegExpression);

            Regex entityUrn = new Regex(RegExHelper.entityuriExpression);

            LogMessage($"{itemfiles.Count()} found in {customXmlFolder}");

            foreach (var itemfile in itemfiles)
            {
                string itemxml = File.ReadAllText(itemfile);

                //var urnMatch = entityUrn.Match(itemxml);
                var dtmatches = docTempRX.Match(itemxml);

                LogMessage($"Attempting match in {itemfile}");

                //if (urnMatch.Success)
                //{
                //    templateManifest.EntityLogicalName = urnMatch.Groups["logicalName"].Value;
                //    templateManifest.EntityObjectTypeCode = int.Parse(urnMatch.Groups["objecttypecode"].Value);
                //    filesToInclude.Add(Path.GetFileName(itemfile));
                //}
                //else
                //{
                //   LogMessage($"No match found in {itemfile}");
                //}


                if (dtmatches.Success)
                {
                    templateManifest.Schema = dtmatches.Groups["schema"].Value;
                    templateManifest.EntityLogicalName = dtmatches.Groups["logicalName"].Value;
                    templateManifest.EntityObjectTypeCode = int.Parse(dtmatches.Groups["objecttypecode"].Value);
                    templateManifest.ItemXml = Path.GetFileName(itemfile);
                }
                else
                {
                    LogMessage($"2nd match attempt inin {itemfile}");

                    var srmatches = schemaRefRX.Match(itemxml);
                    if (srmatches.Success)
                    {
                        templateManifest.ItemPropsXml = Path.GetFileName(itemfile);
                    }
                    else
                    {
                        LogMessage($"Zero matches found in {itemfile}");
                    }
                }
            }

            //templateManifest.ItemXml = String.Join(";", filesToInclude);

            templateManifest.Save(manifestPath);
        }


    }



}

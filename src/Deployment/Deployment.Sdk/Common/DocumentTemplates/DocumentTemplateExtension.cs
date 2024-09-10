using OpenStrata.Deployment.Sdk;
using System.ComponentModel.Composition;

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using DocumentTemplates.Shared.Xml;
using Microsoft.Xrm.Sdk.Metadata;
using System.IO.Packaging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using System.Linq;

namespace OpenStrata.Deployment.Sdk.Common.DocumentTemplates
{
    [Export(typeof(IImportPackageStrataFeatureExtension))]
    public class DocumentTemplateExtension : PackageStrataExtensionBase, IImportPackageStrataFeatureExtension
    {

        private string templatesFolder = "documenttemplates";

        protected override bool AppliesToThisSolution(string solutionName)
        {
            return true;
        }



        protected override bool AfterPrimaryImport()
        {

            PackageLog.Log($"OpenStrata : DocumentTemplates : Checking for OpenStrata Document Templates");

            var templates = GetApplicableTemplates();

            var entityMetadata = new Dictionary<string, EntityMetadata>();

            foreach (var strati in templates.Keys)
            {
                var templateList = templates[strati];

                PackageLog.Log($"OpenStrata : DocumentTemplates : Found {templateList.Count} templates in {strati}");

                foreach (DocumentTemplate template in templateList)
                {
                    PackageLog.Log($"OpenStrata : DocumentTemplates : Process {template.FileName} from  {strati}");

                    ProcessTemplate(template, entityMetadata);

                }
            }



            return true;
        }

        private Dictionary<string, List<DocumentTemplate>> GetApplicableTemplates()
        {
            PackageLog.Log($"OpenStrata : DocumentTemplates : Attempting to identify document templates to import");

            var templates = new Dictionary<string, List<DocumentTemplate>>();

            foreach (XElement se in ImportStrataManifest.Root.Descendants("StratiManifest"))
            {

                var strati = se.Attribute("UniqueName").Value;

                PackageLog.Log($"OpenStrata : DocumentTemplates : Looking for document templates in {strati}");

                if (!templates.ContainsKey(strati))
                {
                    templates.Add(strati, new List<DocumentTemplate>());
                }

                foreach (XElement configDataElement in se.Descendants("DocumentTemplate"))
                {

                    var fileName = configDataElement.Attribute("Name").Value;

                    if (!String.IsNullOrEmpty(fileName) && DocumentTemplate.TryInstantiate(fileName, out DocumentTemplate template))
                    {
                        PackageLog.Log($"OpenStrata : DocumentTemplates : Identified {fileName} template data for solution {strati}");

                        templates[strati].Add(template);
                    }
                }

            }

            return templates;

        }

        private bool ProcessTemplate(DocumentTemplate template, Dictionary<string, EntityMetadata> entityMetadata)
        {

            try
            {
                if (!template.IsValid)
                {
                    PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : {template.InvalidMessage}");

                }
                else if (template.FileType == DocumentTemplateType.word)
                {
                    return ProcessWordTemplate(template, entityMetadata);
                }
                else if (template.FileType == DocumentTemplateType.excel)
                {
                    return ProcessExcelTemplate(template, entityMetadata);
                }
                return false;
            }
            catch (Exception ex)
            {
                PackageLog.Log($"OpenStrata : DocumentTemplates : Exception occurred processing {template.FileName} template.  The template will not been imported.");
                PackageLog.Log(ex.Message);
                return false;
            }
            finally
            {

            }
        }


        private bool ProcessWordTemplate(DocumentTemplate template, Dictionary<string, EntityMetadata> entityMetadata)
        {

            try
            {
                if (!template.IsValid)
                {
                    PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : {template.InvalidMessage}");

                }
                else
                {
                    var templateInfo = new FileInfo(Path.Combine(this.AbsoluteImportPackageDataFolderPath, templatesFolder, template.FileName));
                    if (templateInfo.Exists)
                    {

                        PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Found template file.");

                        var manifestInfo = new FileInfo(Path.Combine(this.AbsoluteImportPackageDataFolderPath, templatesFolder, template.Manfiest));
                        if (manifestInfo.Exists)
                        {
                            PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Found template manifest file.");

                            var manifest = DocumentTemplateManifestXDocument.Load(manifestInfo.FullName);

                            if (!entityMetadata.ContainsKey(manifest.EntityLogicalName))
                            {
                                entityMetadata.Add(manifest.EntityLogicalName, CrmSvc.GetEntityMetadata(manifest.EntityLogicalName));
                            }

                            var entityInfo = entityMetadata[manifest.EntityLogicalName];

                            if (manifest.EntityObjectTypeCode != entityInfo.ObjectTypeCode)
                            {
                                //Update of document xml required.
                                PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Updating entity object code from {manifest.EntityObjectTypeCode} to {entityInfo.ObjectTypeCode}");


                                using (var docPackage = Package.Open(templateInfo.FullName, FileMode.Open, FileAccess.ReadWrite))
                                {

                                    var newSchema = manifest.Schema.Replace(manifest.EntityObjectTypeCode.ToString(), entityInfo.ObjectTypeCode.ToString());

                                    // Process ItemXml
                                    if (!string.IsNullOrEmpty(manifest.ItemXml))
                                    {
                                        var itemUri = PackUriHelper.CreatePartUri(new Uri($"customXml\\{manifest.ItemXml}", UriKind.Relative));

                                        if (docPackage.PartExists(itemUri))
                                            docPackage.GetPart(itemUri).ReplaceText(manifest.Schema, newSchema, PackageLog);

                                        PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Processed {itemUri} part");


                                    }

                                    // Process ItemPropsXml
                                    if (!string.IsNullOrEmpty(manifest.ItemPropsXml))
                                    {
                                        var itemPropsUri = PackUriHelper.CreatePartUri(new Uri($"customXml\\{manifest.ItemPropsXml}", UriKind.Relative));
                                        if (docPackage.PartExists(itemPropsUri))
                                            docPackage.GetPart(itemPropsUri).ReplaceText(manifest.Schema, newSchema, PackageLog);

                                        PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Processed {itemPropsUri} part");

                                    }

                                }




                            }
                            else
                            {
                                PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Entity Object Code does not need to be updated.");
                            }


                            PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Finished updating document.  Preparing to import.");

                            string fileName = Path.GetFileNameWithoutExtension(template.FileName);

                            var qry = new QueryByAttribute("documenttemplate");

                            qry.Attributes.Add("name");
                            qry.Values.Add(fileName);

                            PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Attempting to locate pre-exsisting template to replace.");


                            var response = CrmSvc.RetrieveMultiple(qry);

                            if (response != null)
                            {

                                PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Found {response.Entities.Count} pre-existing template records");

                            }
                            else
                            {
                                PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : No pre-existing template records");

                            }

                            //var id = existingDoc != null ? existingDoc.Id : Guid.NewGuid();


                            //Update the Document Template
                            // Entity docTemplate = new Entity("documenttemplate", id);
                            Entity docTemplate = new Entity("documenttemplate");

                            docTemplate["name"] = fileName;
                            docTemplate["content"] = Convert.ToBase64String(File.ReadAllBytes(templateInfo.FullName));
                            docTemplate["documenttype"] = new OptionSetValue((int)template.FileType);
                            //docTemplate["associatedentitytypecode"] = entityInfo.ObjectTypeCode;
                            // docTemplate["languagecode"] = 1033;

                            PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Attempting Import");

                            var docTemplateId = CrmSvc.Create(docTemplate);

                            //var req = new UpsertRequest()
                            // {
                            //     Target = docTemplate,
                            // };

                            //var resp = CrmSvc.Execute(req);


                            PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Sucessfully Imported document template");

                            return true;

                        }
                        else
                        {
                            PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Manifest not found.  Cannot process");
                        }
                    }
                    else
                    {
                        PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Not found in the package.");
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                PackageLog.Log($"OpenStrata : DocumentTemplates : Exception occurred processing {template.FileName} template.  The template will not been imported.");
                PackageLog.Log(ex.Message);
                return false;
            }
            finally
            {

            }
        }

        private bool ProcessExcelTemplate(DocumentTemplate template, Dictionary<string, EntityMetadata> entityMetadata)
        {

            try
            {

                var templateInfo = new FileInfo(Path.Combine(this.AbsoluteImportPackageDataFolderPath, templatesFolder, template.FileName));
                if (templateInfo.Exists)
                {

                    PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Found template file.");

                    var manifestInfo = new FileInfo(Path.Combine(this.AbsoluteImportPackageDataFolderPath, templatesFolder, template.Manfiest));
                    if (manifestInfo.Exists)
                    {
                        PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Found template manifest file.");

                        var manifest = DocumentTemplateManifestXDocument.Load(manifestInfo.FullName);

                        string fileName = Path.GetFileNameWithoutExtension(template.FileName);

                        var qry = new QueryByAttribute("documenttemplate");

                        qry.Attributes.Add("name");
                        qry.Values.Add(fileName);

                        PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Attempting to locate pre-exsisting template to replace.");

                        var response = CrmSvc.RetrieveMultiple(qry);

                        if (response != null)
                        {

                            PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Found {response.Entities.Count} pre-existing template records");

                        }
                        else
                        {
                            PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : No pre-existing template records");

                        }

                        //var id = existingDoc != null ? existingDoc.Id : Guid.NewGuid();


                        //Update the Document Template
                        // Entity docTemplate = new Entity("documenttemplate", id);
                        Entity docTemplate = new Entity("documenttemplate");

                        docTemplate["name"] = fileName;
                        docTemplate["content"] = Convert.ToBase64String(File.ReadAllBytes(templateInfo.FullName));
                        docTemplate["documenttype"] = new OptionSetValue((int)template.FileType);
                        //docTemplate["associatedentitytypecode"] = entityInfo.ObjectTypeCode;
                        // docTemplate["languagecode"] = 1033;

                        PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Attempting Import");

                        var docTemplateId = CrmSvc.Create(docTemplate);

                        //var req = new UpsertRequest()
                        // {
                        //     Target = docTemplate,
                        // };

                        //var resp = CrmSvc.Execute(req);


                        PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Sucessfully Imported document template");

                        return true;

                    }
                    else
                    {
                        PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Manifest not found.  Cannot process");
                    }
                }
                else
                {
                    PackageLog.Log($"OpenStrata : DocumentTemplates : {template.FileName} : Not found in the package.");
                }

                return false;
            }
            catch (Exception ex)
            {
                PackageLog.Log($"OpenStrata : DocumentTemplates : Exception occurred processing {template.FileName} template.  The template will not been imported.");
                PackageLog.Log(ex.Message);
                return false;
            }
            finally
            {

            }
        }

        public enum DocumentTemplateType
        {
            word = 2,
            excel = 1,
        }


        private class DocumentTemplate
        {

            public static bool TryInstantiate(string fileName, out DocumentTemplate result)
            {
                var extenstion = Path.GetExtension(fileName).ToLower();

                result = new DocumentTemplate(fileName);

                if (result.IsValid) return true;

                result = null;
                return false;

            }

            public DocumentTemplateType FileType { get; private set; }

            public string FileName { get; private set; }

            public string Manfiest => FileName + ".xml";

            public string Extension { get; private set; }

            public bool IsValid { get; private set; } = true;

            public string InvalidMessage { get; private set; } = string.Empty;

            public DocumentTemplate(string fileName)
            {
                FileName = fileName;

                Extension = Path.GetExtension(fileName).ToLower();

                if (Extension == ".docx")
                {
                    FileType = DocumentTemplateType.word;
                }
                else if (Extension == ".xlsx")
                {
                    FileType = DocumentTemplateType.excel;
                }
                else
                {
                    IsValid = false;
                    InvalidMessage = "Invalid file type for Document Template Import.  Must be a .docx or .xlsx file.";
                }
            }


        }

    }




}

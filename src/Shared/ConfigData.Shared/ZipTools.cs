using OpenStrata.ConfigData.Xml;
using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace OpenStrata.ConfigData
{
    public static class ZipTools
    {

        public delegate void LogMessage(string msg);



        public static bool TryPackConfigData(string outpath, string configDataDir, LogMessage logger, out string message, out Exception ex)
        {
            message = null;
            ex = null;

            try
            {

                logger($"OpenStrata : ZipTools.TryPackConfigData : Attempting to pack config data from config data root {configDataDir}");

                DirectoryInfo dir = new DirectoryInfo(configDataDir);


                if (dir.Exists)
                {

                    FileInfo dataXml = new FileInfo(Path.Combine(dir.FullName, "data.xml"));
                    if (!dataXml.Exists)
                    {
                        message = $"OpenStrata : ZipTools.TryPackConfigData : Specified config data directory does not contain a data.xml file: {configDataDir}";
                        return false;
                    }

                    logger($"OpenStrata : ZipTools.TryPackConfigData : Located data.xml file");

                    FileInfo schemaXml = new FileInfo(Path.Combine(dir.FullName, "data_schema.xml"));
                    if (!schemaXml.Exists)
                    {
                        message = $"OpenStrata : ZipTools.TryPackConfigData : Specified config data directory does not contain a data_schema.xml file: {configDataDir}";
                        return false;
                    }

                    logger($"OpenStrata : ZipTools.TryPackConfigData : Located data_schema.xml file");

                    FileInfo contentTypeXml = new FileInfo(Path.Combine(dir.FullName, "[Content_Types].xml"));

                    if (!contentTypeXml.Exists) CreateConfigDataContentTypes(contentTypeXml, logger);


                    FileInfo orderXml = new FileInfo(Path.Combine(dir.FullName, "entityImportOrder.xml"));

                    if (!TryUpdateEntityImportOrder(schemaXml, orderXml, logger, out message))
                    {
                        return false;
                    }

                    using (FileStream packageZip = File.Open(outpath, FileMode.Create))
                    {
                        using (ZipArchive archive = new ZipArchive(packageZip, ZipArchiveMode.Create))
                        {

                            archive.CreateEntryFromFile(contentTypeXml.FullName, "[Content_Types].xml");

                            archive.CreateEntryFromFile(dataXml.FullName, dataXml.Name);

                            archive.CreateEntryFromFile(schemaXml.FullName, schemaXml.Name);

                        }

                    }
                    return true;
                }
                else
                {
                    message = $"OpenStrata : ZipTools.TryPackConfigData : Specified config data root directory does no exist: {configDataDir}";
                    return false;
                }

            }
            catch (Exception e)
            {

                ex = e;

                return false;
            }


        }

        private static void CreateConfigDataContentTypes(FileInfo contentTypeFile, LogMessage logger)
        {

            logger($"OpenStrata : ZipTools.TryPackConfigData : Creating ConfigData ContentTypes : {contentTypeFile.FullName}");

            StringBuilder xmlSB = new StringBuilder();
            xmlSB.AppendLine($"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xmlSB.AppendLine($"<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\">");
            xmlSB.AppendLine($"     <Default Extension=\"xml\" ContentType=\"application/octet-stream\" />");
            xmlSB.AppendLine($"</Types>");

            File.WriteAllText(contentTypeFile.FullName, xmlSB.ToString());

        }

        private static bool TryUpdateEntityImportOrder(FileInfo schemaXmlFile, FileInfo orderXmlFile, LogMessage logger, out string message)
        {

            message = null;

            List<string> currentOrder = new List<string>();
            List<string> newOrder = new List<string>();
            List<string> schemaEntities = new List<string>();

            // Load specified EntityImportOrder
            logger($"OpenStrata : ZipTools.TryPackConfigData : Updating Entity Import Order : Starting Processing");

            if (orderXmlFile.Exists)
            {
                logger($"OpenStrata : ZipTools.TryPackConfigData : Updating Entity Import Order : Extracting current order from {orderXmlFile.Name}");

                EntityImportOrderXDocument curOrderDoc = EntityImportOrderXDocument.Load(orderXmlFile.FullName);

                foreach (XElement orderEntity in curOrderDoc.ImportOrder.Elements("entityName"))
                {
                    if (orderEntity.Value != null)
                    {
                        currentOrder.Add(orderEntity.Value);
                    }
                }
            }

            if (schemaXmlFile.Exists)
            {
                ConfigDataSchemaXDocument schemaDoc = ConfigDataSchemaXDocument.Load(schemaXmlFile.FullName);

                logger($"OpenStrata : ZipTools.TryPackConfigData : Updating Entity Import Order : Extracting entities to order from {schemaXmlFile.Name}");

                foreach (XAttribute entityName in schemaDoc.Root.Elements("entity").Attributes("name"))
                {
                    schemaEntities.Add(entityName.Value);
                }

                if (schemaEntities.Count == 0)
                {
                    message = ($"OpenStrata : ZipTools.TryPackConfigData : No entities found in schema {schemaXmlFile.Name}");
                    return false;
                }

                logger($"OpenStrata : ZipTools.TryPackConfigData : Updating Entity Import Order : Comparing Schema with current import order.");

                foreach (string curOrderE in currentOrder)
                {
                    if (schemaEntities.Contains(curOrderE))
                    {
                        newOrder.Add(curOrderE);
                        _ = schemaEntities.Remove(curOrderE);
                    }
                }

                //Checking for changes.  If no change, then don't update files.
                if (currentOrder.ToArray().SequenceEqual(newOrder.ToArray()) && schemaEntities.Count == 0)
                {
                    logger($"OpenStrata : ZipTools.TryPackConfigData : Updating Entity Import Order : No Change.  No further processing is required.");
                    return true;
                }

                logger($"OpenStrata : ZipTools.TryPackConfigData : Updating Entity Import Order : Updates to the import order have been found.");

                foreach (string schemaE in schemaEntities)
                {
                    newOrder.Add(schemaE);
                }

                logger($"OpenStrata : ZipTools.TryPackConfigData : Updating Entity Import Order : Updating {schemaXmlFile.Name} and {orderXmlFile.Name} with the new import order.");

                logger($"remove: new EntityImportOrderXDocument()");

                EntityImportOrderXDocument newOrderDoc = new EntityImportOrderXDocument();

                logger($"remove: GetOrCreateElement(entityImportOrder)");

                XElement schemaDocIO = schemaDoc.Root.GetOrCreateElement("entityImportOrder");

                logger($"remove: before removenodes");

                schemaDocIO.RemoveNodes();

                logger($"remove: before foreach");

                foreach (string entityname in newOrder)
                {
                    newOrderDoc.ImportOrder.Add(new XElement("entityName", new XText(entityname)));
                    schemaDocIO.Add(new XElement("entityName", new XText(entityname)));
                }


                logger($"remove: before newOrderDoc save");

                newOrderDoc.Save(orderXmlFile.FullName);

                logger($"remove: before schemaDoc save");
                schemaDoc.Save(schemaXmlFile.FullName);

                logger($"remove: files saved");

                return true;
            }
            else
            {
                message = ($"OpenStrata : ZipTools.TryPackConfigData : Cannot find schema file :  {schemaXmlFile.FullName}");
                return false;
            }
        }

    }
}

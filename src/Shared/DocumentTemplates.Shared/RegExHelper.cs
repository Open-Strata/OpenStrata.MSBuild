using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplates.Shared
{
    internal class RegExHelper
    {

        public static string entityuriExpression = @"urn:microsoft-crm/document-template/(?<logicalName>[a-z,_]+)/(?<objecttypecode>[0-9]+)/";

        // public static string entityuriExpression = "urn:microsoft-crm/document-template/(?<logicalName>[a-z,_]+)/(?<objecttypecode>[0-9]+)/";


        public static string DocumentTemplateRegExpression = @"<DocumentTemplate xmlns=""(?<schema>urn:microsoft-crm/document-template/(?<logicalName>[a-z,_]+)/(?<objecttypecode>[0-9]+)/)"">";

        public static string SchemaRefRegExpression = @"<ds:schemaRef ds:uri=""urn:microsoft-crm/document-template/(?<logicalName>[a-z,_]+)/(?<objecttypecode>[0-9]+)/""/>";


    }
}

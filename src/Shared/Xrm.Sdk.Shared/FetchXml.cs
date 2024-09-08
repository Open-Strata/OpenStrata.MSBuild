using System;
using System.Collections.Generic;
using System.Text;

namespace OpenStrata.Xrm.Sdk
{
    public static class FetchXml
    {

        public static readonly string FetchDocumentTemplateXml = @"
<fetch version=""1.0"" output-format=""xml-platform"" mapping=""logical"" distinct=""false"">
    <entity name=""documenttemplate"">
    <attribute name=""documenttemplateid"" />	  
    <attribute name=""documenttype"" />
    <attribute name=""name"" />
    <attribute name=""status"" />
    <attribute name=""description"" />
    <attribute name=""languagecode"" />
    <attribute name=""associatedentitytypecode"" />
	<attribute name=""createdon"" />
    <order attribute=""createdon"" descending=""true"" />
    <filter type=""and"">
        <condition attribute=""name"" operator=""eq"" value=""{0}"" />
        <condition attribute=""status"" operator=""eq"" value=""0"" />
        <condition attribute=""documenttype"" operator=""eq"" value=""{2}"" />
        <condition attribute=""associatedentitytypecode"" operator=""eq"" value=""{1}"" />
    </filter>
    </entity>
</fetch>
";



    }
}

using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Publisher.Xml
{
    public class PublisherXDocument : OpenStrataXDocument<PublisherXDocument>
    {
        public override string GetDefaultXmlnsOnCreate => "";

        public override string RootNodeName => "Publisher";

        protected override bool UseXsiXmlns => true;

        private XElement _uniqueName;
        public XElement UniqueName => Root.LazyLoad("UniqueName", _uniqueName, out _uniqueName);

        private XElement _localizednames;
        public XElement LocalizedNames => Root.LazyLoad("LocalizedNames", _localizednames, out _localizednames);

        private XElement _descriptions;
        public XElement Descriptions => Root.LazyLoad("Descriptions", _descriptions, out _descriptions);

        private XElement _emailaddress;
        public XElement EmailAddress => Root.LazyLoad("EMailAddress", _emailaddress, out _emailaddress);

        private XElement _supportingwebsiteurl;
        public XElement SupportingWebsiteUrl => Root.LazyLoad("SupportingWebsiteUrl", _supportingwebsiteurl, out _supportingwebsiteurl);

        private XElement _customizationprefix;
        public XElement CustomizationPrefix => Root.LazyLoad("CustomizationPrefix", _customizationprefix, out _customizationprefix);

        private XElement _customizationoptionvalueprefix;
        public XElement CustomizationOptionValuePrefix => Root.LazyLoad("CustomizationOptionValuePrefix", _customizationoptionvalueprefix, out _customizationoptionvalueprefix);

        private XElement _addresses;
        public XElement Addresses => Root.LazyLoad("Addresses", _addresses, out _addresses)
                                     .ReTypeChildrenToAddress();



        public class LocalizedName : XElement
        {
            public LocalizedName(XElement other) 
                : base(other)
            {
            }
            public LocalizedName(XElement parent, string description, string languagecode, string defaultnamespace = "") 
                : base(XName.Get("LocalizedName", defaultnamespace))
            {
                _ = this.AttachTo(parent);
                Description.Value = description;
                LanguageCode.Value = languagecode;
            }

            private XAttribute _description;
            public XAttribute Description => this.LazyLoad("description", _description, out _description);

            private XAttribute _languagecode;
            public XAttribute LanguageCode => this.LazyLoad("languagecode", _languagecode, out _languagecode);

        }

        public class Description : XElement
        {
            public Description(XElement other)
                : base(other)
            {
            }
            public Description(XElement parent, string description, string languagecode, string defaultnamespace = "")
                : base(XName.Get("Description", defaultnamespace))
            {
                _ = this.AttachTo(parent);
                this.description.Value = description;
                LanguageCode.Value = languagecode;

            }

            private XAttribute _description;
            public XAttribute description => this.LazyLoad("description", _description, out _description);

            private XAttribute _languagecode;
            public XAttribute LanguageCode => this.LazyLoad("languagecode", _languagecode, out _languagecode);

        }


        public class Address : XElement
        {
            public Address(XElement other)
                : base(other)
            {
            }
            public Address(XElement parent, int addressNumber, string defaultnamespace = "")
                : base(XName.Get("Address", defaultnamespace))
            {
                _ = this.AttachTo(parent);
            }

            private XElement _addressnumber;
            public XElement AddressNumber => this.LazyLoad("AddressNumber", _addressnumber, out _addressnumber);

            private XElement _addresstypecode;
            public XElement AddressTypeCode => this.LazyLoad("AddressTypeCode", _addresstypecode, out _addresstypecode);

            private XElement _city;
            public XElement City => this.LazyLoad("City", _city, out _city);

            private XElement _county;
            public XElement County => this.LazyLoad("County", _county, out _county);

            private XElement _country;
            public XElement Country => this.LazyLoad("Country", _country, out _country);

            private XElement _fax;
            public XElement Fax => this.LazyLoad("Fax", _fax, out _fax);

            private XElement _freighttermscode;
            public XElement FreightTermsCode => this.LazyLoad("FreightTermsCode", _freighttermscode, out _freighttermscode);

            private XElement _importsequencenumber;
            public XElement ImportSequenceNumber => this.LazyLoad("ImportSequenceNumber", _importsequencenumber, out _importsequencenumber);

            private XElement _latitude;
            public XElement Latitude => this.LazyLoad("Latitude", _latitude, out _latitude);

            private XElement _line1;
            public XElement Line1 => this.LazyLoad("Line1", _line1, out _line1);

            private XElement _line2;
            public XElement Line2 => this.LazyLoad("Line2", _line2, out _line2);

            private XElement _line3;
            public XElement Line3 => this.LazyLoad("Line3", _line3, out _line3);

            private XElement _longitude;
            public XElement Longitude => this.LazyLoad("Longitude", _longitude, out _longitude);

            private XElement _name;
            public XElement AddressName => this.LazyLoad("Name", _name, out _name);

            private XElement _postalcode;
            public XElement PostalCode => this.LazyLoad("PostalCode", _postalcode, out _postalcode);

            private XElement _postofficebox;
            public XElement PostOfficeBox => this.LazyLoad("PostOfficeBox", _postofficebox, out _postofficebox);

            private XElement _primarycontactname;
            public XElement PrimaryContactName => this.LazyLoad("PrimaryContactName", _primarycontactname, out _primarycontactname);

            private XElement _shippingmethodcode;
            public XElement ShippingMethodCode => this.LazyLoad("ShippingMethodCode", _shippingmethodcode, out _shippingmethodcode);

            private XElement _stateorprovince;
            public XElement StateOrProvince => this.LazyLoad("StateOrProvince", _stateorprovince, out _stateorprovince);

            private XElement _telephone1;
            public XElement Telephone1 => this.LazyLoad("Telephone1", _telephone1, out _telephone1);

            private XElement _telephone2;
            public XElement Telephone2 => this.LazyLoad("Telephone2", _telephone2, out _telephone2);

            private XElement _telephone3;
            public XElement Telephone3 => this.LazyLoad("Telephone3", _telephone3, out _telephone3);

            private XElement _timezoneruleversionnumber;
            public XElement TimeZoneRuleVersionNumber => this.LazyLoad("TimeZoneRuleVersionNumber", _timezoneruleversionnumber, out _timezoneruleversionnumber);

            private XElement _upszone;
            public XElement UPSZone => this.LazyLoad("UPSZone", _upszone, out _upszone);

            private XElement _utcoffset;
            public XElement UTCOffset => this.LazyLoad("UTCOffset", _utcoffset, out _utcoffset);

            private XElement _utcconversiontimezonecode;
            public XElement UTCConversionTimeZoneCode => this.LazyLoad("UTCConversionTimeZoneCode", _utcconversiontimezonecode, out _utcconversiontimezonecode);



        }



    }
}

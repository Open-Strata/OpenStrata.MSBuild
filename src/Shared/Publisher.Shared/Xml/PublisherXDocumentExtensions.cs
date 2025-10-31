using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Publisher.Xml
{
    public static class PublisherXDocumentExtensions
    {

        public static XElement ReTypeChildrenToAddress (this XElement parent)
        {

            foreach (XElement addrElement in parent.Elements("Address").InDocumentOrder())
            {
                new PublisherXDocument.Address(addrElement).AttachTo(parent);
                addrElement.Remove();
            }

            return parent;
        }







    }
}

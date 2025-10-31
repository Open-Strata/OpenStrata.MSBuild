using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strati.Manifest.Xml
{
    public class PowerPagesXElement : CustomXElement
    {
        public PowerPagesXElement(XElement copyFrom) : base(copyFrom)
        {
        }

        public PowerPagesXElement() : base(XName.Get("PowerPagesSite", ""))
        {
        }

        private XAttribute _contentZipFile;
        public XAttribute ContentZipFile => this.LazyLoad("ContentZipFile", _contentZipFile, out _contentZipFile);

        private XAttribute _siteName;
        public XAttribute SiteName => this.LazyLoad("SiteName", _siteName, out _siteName);

        private XAttribute _deploymentYml;
        public XAttribute DeploymentYml => this.LazyLoad("DeploymentYml", _deploymentYml, out _deploymentYml);

    }
}

using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strati.Manifest.Xml
{
    public class DeploymentExtensionXElement : CustomXElement
    {
        public DeploymentExtensionXElement(XElement copyFrom) : base(copyFrom)
        {
        }

        public DeploymentExtensionXElement() : base(XName.Get("DeploymentExtension", ""))
        {
        }

        private XAttribute _runtimeAssembly;
        public XAttribute RunTimeAssembly => this.LazyLoad("RunTimeAssembly", _runtimeAssembly, out _runtimeAssembly);

        private XAttribute _localImportSequence;
        public XAttribute LocalImportSequence => this.LazyLoad("LocalImportSequence", _localImportSequence, out _localImportSequence);

    }
}

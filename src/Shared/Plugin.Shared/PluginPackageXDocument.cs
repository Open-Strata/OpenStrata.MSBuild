using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Plugin.Shared.Xml
{
    public class PluginPackageXDocument : OpenStrataXDocument<PluginPackageXDocument>
    {
        public override string GetDefaultXmlnsOnCreate => "";

        public override string RootNodeName => "pluginpackage";

        private XAttribute _uniqueName;
        public XAttribute uniquename => Root.LazyLoad("uniquename", _uniqueName, out _uniqueName);

        private XElement _pluginpackageid;
        public XElement pluginpackageid => Root.LazyLoad("pluginpackageid", _pluginpackageid, out _pluginpackageid);

        private XElement _exportkeyversion;
        public XElement exportkeyversion => Root.LazyLoad("exportkeyversion", _exportkeyversion, out _exportkeyversion);

        private XElement _iscustomizable;
        public XElement iscustomizable => Root.LazyLoad("iscustomizable", _iscustomizable, out _iscustomizable);

        private XElement _name;
        public XElement name => Root.LazyLoad("name", _name, out _name);

        private XElement _package;
        public XElement package => Root.LazyLoad("package", _package, out _package);

        private XElement _mimetype;
        public XElement package_mimetype => package.LazyLoad("mimetype", _mimetype, out _mimetype);

        private XElement _statecode;
        public XElement statecode => Root.LazyLoad("statecode", _statecode, out _statecode);

        private XElement _statuscode;
        public XElement statuscode => Root.LazyLoad("statuscode", _statuscode, out _statuscode);

        private XElement _version;
        public XElement version => Root.LazyLoad("version", _version, out _version);

    }
}

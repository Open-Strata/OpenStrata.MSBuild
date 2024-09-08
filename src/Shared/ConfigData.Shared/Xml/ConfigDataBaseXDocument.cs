using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenStrata.ConfigData.Xml
{
    public abstract class ConfigDataBaseXDocument<x> : OpenStrataXDocument<x>
        where x : ConfigDataBaseXDocument<x>, new ()
    {

        public override string GetDefaultXmlnsOnCreate => "";

        public override string RootNodeName => "entities";

    }

}

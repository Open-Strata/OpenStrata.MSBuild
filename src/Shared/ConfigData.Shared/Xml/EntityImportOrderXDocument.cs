using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.ConfigData.Xml
{
    public class EntityImportOrderXDocument : ConfigDataBaseXDocument<EntityImportOrderXDocument>
    {
        //public override string GetDefaultXmlnsOnCreate => "";

        //protected override bool UseXsiXmlns => true;
        //protected override bool UseXsdXmlns => true;

        //public override string RootNodeName => "entities";

        public static string EntityImportOrder = "entityImportOrder";
        public static string EntityName = "entityName";

        private XElement _importOrder;
        public XElement ImportOrder => Root.LazyLoad(EntityImportOrder, _importOrder, out _importOrder);

    }
}

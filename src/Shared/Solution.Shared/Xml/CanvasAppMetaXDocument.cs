using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Solution.Xml
{
    public class CanvasAppMetaXDocument : OpenStrataXDocument<CanvasAppMetaXDocument>
    {
        public override string GetDefaultXmlnsOnCreate => "";

        public override string RootNodeName => "CanvasApp";

        public DateTime? AppVersionDateTimeStamp
        {
            get
            {
                if ( DateTime.TryParse(AppVersionElement.Value, out DateTime result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value!= null && value.HasValue)
                {
                    AppVersionElement.Value = value.Value.ToUTCOffSetString();
                }
                else
                {
                    AppVersionElement.Value = string.Empty;
                }
            }
        }

        private XElement _appVersion;
        private XElement AppVersionElement => Root.LazyLoad("AppVersion", _appVersion, out _appVersion);


    }
}

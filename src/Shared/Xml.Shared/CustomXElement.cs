using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Xml
{
    public class CustomXElement : XElement
    {
        public CustomXElement(XElement copyFrom) : base(copyFrom)
        {
            this.ReplaceElement(copyFrom);
        }

        public CustomXElement(XName name) : base(name)
        {
        }
    }
}

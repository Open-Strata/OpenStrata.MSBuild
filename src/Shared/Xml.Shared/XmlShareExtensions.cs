using System;
using System.Collections.Generic;
using System.Text;

namespace OpenStrata.Xml
{
    public static class XmlShareExtensions
    {

        public static string ToUTCOffSetString(this DateTime datetime)
        {
            if (datetime == null) throw new ArgumentNullException();

            return (new DateTimeOffset(datetime)).ToString("u").Replace(' ', 'T');


        }
    }
}

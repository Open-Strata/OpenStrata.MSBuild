using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace OpenStrata.Xml
{

    public abstract class OpenStrataXDocument<x> : OpenStrataXDocument
        where x : OpenStrataXDocument<x>, new()
    {

        public new static x Load(string uri)
        {
            return Load(XDocument.Load(uri));
        }

        public new static x Load(string uri, LoadOptions options)
        {
            return Load(XDocument.Load(uri, options));
        }
        public new static x Load(XmlReader reader)
        {
            return Load(XDocument.Load(reader));
        }

        public new static x Load(XmlReader reader, LoadOptions options)
        {
            return Load(XDocument.Load(reader, options));
        }

        public new static x Load(Stream stream)
        {
            return Load(XDocument.Load(stream));
        }

        public new static x Load(TextReader textReader)
        {
            return Load(XDocument.Load(textReader));
        }

        public new static x Load(Stream stream, LoadOptions options)
        {
            return Load(XDocument.Load(stream, options));
        }

        public new static x Load(TextReader textReader, LoadOptions options)
        {
            return Load(XDocument.Load(textReader, options));
        }

        public static x Load(XDocument xdoc)
        {
            var newDoc = new x();
            newDoc.Add(xdoc.Root);
            return newDoc;
        }

    }
}

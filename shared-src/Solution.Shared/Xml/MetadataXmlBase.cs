using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenStrata.Solution.Xml
{
    public abstract class MetadataXmlBase<x> : OpenStrataXDocument<x>
        where x : MetadataXmlBase<x>, new()
    {
        public override string GetDefaultXmlnsOnCreate => "";

        public abstract string PathFromRoot { get;}


        public void SaveToRoot(string rootPath)
        {
            var savePath = Path.Combine(rootPath, PathFromRoot);
            Save(savePath);
        }
    }
}

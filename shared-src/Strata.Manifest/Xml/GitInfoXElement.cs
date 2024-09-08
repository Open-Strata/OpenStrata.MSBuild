using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OpenStrata.Strata.Manifest.Xml
{
    public class GitInfoXElement : CustomXElement
    {
        public GitInfoXElement(XElement copyFrom) : base(copyFrom)
        {
        }

        public GitInfoXElement() : base(XName.Get("GitInfo", ""))
        {
        }

        private XElement _repourl;
        public XElement RepoUrl => this.LazyLoad("RepoUrl", _repourl, out _repourl);

        private XElement _commit;
        public XElement Commit => this.LazyLoad("Commit", _commit, out _commit);

        private XElement _commitdate;
        public XElement CommitDate => this.LazyLoad("CommitDate", _commitdate, out _commitdate);

    }
}

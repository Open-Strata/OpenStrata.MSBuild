using Microsoft.Build.Framework;
using OpenStrata.MSBuild.Tasks;
using OpenStrata.Solution.Xml;
using OpenStrata.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace OpenStrata.MSBuild.Solution.Tasks
{
    public class FixSolutionXml : BaseTask
    {
        [Required]
        public string RootPath { get; set; }

        //[Required]
        //public string DataverseSolutionUniqueName { get; set; }

        public ITaskItem[] AttributeDependenciesToRemove { get; set; }
        public ITaskItem[] SolutionDependenciesToRemove { get; set; }
        public ITaskItem[] RequiredTypeDependenciesToRemove { get; set; }
        public ITaskItem[] DependentTypeDependenciesToRemove { get; set; }

        public ITaskItem[] SolutionDependencyMinVersion { get; set; }

        public bool Debugging { get; set; } = false;

        public override bool ExecuteTask()
        {

            var solXDoc = SolutionXDocument.LoadFromRootPath(RootPath);

            if (SolutionDependenciesToRemove != null)
            {

                foreach (ITaskItem item in SolutionDependenciesToRemove)
                {
                    RemoveDependencyNodeBySolutionName(solXDoc, item.ItemSpec);
                }

            }

            if (AttributeDependenciesToRemove != null)
            {

                foreach (ITaskItem item in AttributeDependenciesToRemove)
                {
                    RemoveDependencyNodeByAttribute(solXDoc, item.ItemSpec);
                }
            }

            if (RequiredTypeDependenciesToRemove != null)
            {

                foreach (ITaskItem item in RequiredTypeDependenciesToRemove)
                {
                    RemoveDependencyNodeByRequiredType(solXDoc, item.ItemSpec);
                }
            }

            if (DependentTypeDependenciesToRemove != null)
            {

                foreach (ITaskItem item in DependentTypeDependenciesToRemove)
                {
                    RemoveDependencyNodeByDependentType(solXDoc, item.ItemSpec);
                }
            }

            if (SolutionDependencyMinVersion != null)
            {

                foreach (ITaskItem item in SolutionDependencyMinVersion)
                {
                    var minVer = item.GetMetadata("MinVersion");
                    if (!String.IsNullOrEmpty(minVer))
                    {
                        SetSolutionDependencyMinimumVersion(solXDoc, item.ItemSpec, minVer);
                    }
                }
            }

            solXDoc.SaveToRoot(RootPath);

            return true;
        }

        internal void RemoveDependencyNodeByRequiredType(SolutionXDocument doc, string requiredType)
        {
            foreach (XElement dependency in doc.MissingDependencies.XPathSelectElements($"MissingDependency/Required[@type='{requiredType}']/.."))
            {

                if (Debugging)
                {
                    this.LogMessage(dependency.ToString());
                }
                dependency.Remove();
            }
        }

        internal void RemoveDependencyNodeByDependentType(SolutionXDocument doc, string dependentType)
        {
            foreach (XElement dependency in doc.MissingDependencies.XPathSelectElements($"MissingDependency/Dependent[@type='{dependentType}']/.."))
            {

                if (Debugging)
                {
                    this.LogMessage(dependency.ToString());
                }
                dependency.Remove();
            }
        }


        internal void RemoveDependencyNodeBySolutionName(SolutionXDocument doc, string Solution)
        {
            string solutionPart;

            foreach (XElement dependency in doc.MissingDependencies.XPathSelectElements($"MissingDependency/Required[starts-with(@solution,'{Solution}')]/.."))
            {

                solutionPart = dependency.Element("Required").Attribute("solution").Value.Split(' ')[0];

                if (solutionPart == Solution)
                {

                    if (Debugging)
                    {
                        this.LogMessage($"Solution Part: {dependency.ToString()}");
                        this.LogMessage(dependency.ToString());
                    }

                    dependency.Remove();
                }
            }
        }

        internal void RemoveDependencyNodeByAttribute(SolutionXDocument doc, string schemaName)
        {

            foreach (XElement dependency in doc.MissingDependencies.XPathSelectElements($"MissingDependency/Required[@schemaName='{schemaName}']/.."))
            {

                if (Debugging)
                {
                    this.LogMessage(dependency.ToString());
                }
                dependency.Remove();
            }
        }

        internal void SetSolutionDependencyMinimumVersion(SolutionXDocument doc, string Solution, string minVersion)
        {
            string solutionPart;

            foreach (XElement dependency in doc.MissingDependencies.XPathSelectElements($"MissingDependency/Required[starts-with(@solution,'{Solution}')]/.."))
            {

                solutionPart = dependency.Element("Required").Attribute("solution").Value.Split(' ')[0];

                if (solutionPart == Solution)
                {

                    if (Debugging)
                    {
                        this.LogMessage($"Solution Part: {dependency.ToString()}");
                        this.LogMessage(dependency.ToString());
                    }

                    dependency.Element("Required").Attribute("solution").Value = $"{solutionPart} ({minVersion})";
                }
            }
        }
    }
}

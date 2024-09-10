using OpenStrata.Strati.Manifest.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenStrata.Deployment.Sdk
{
    public abstract class PackageStratiBase : PackageStrataExtensionBase, IImportPackageStratiExtension
    {
        [Obsolete("SratiDataverseSolutionUniqueName is deprecated and will be disregarded.", true)]
        protected virtual string SratiDataverseSolutionUniqueName => null;

        private List<string> _applicablesolutions;
        protected List<string> ApplicableSolutions
        {
            get
            {
                if(_applicablesolutions == null)
                {
                    _applicablesolutions = GetApplicableSolutions();

                }
                return _applicablesolutions;
            }
        }

        private string _assemblyFileName;
        protected string AssemblyFileName
        {
            get
            {
                if (_assemblyFileName == null)
                {
                    _assemblyFileName = Path.GetFileName(this.GetType().Assembly.Location);
                    PackageLog.Log($"OpenStrata : Assembly File Name for {this.GetType().FullName} is {_assemblyFileName}");
                } 
                return _assemblyFileName;
            }
        }

        private StratiManifestXElement _stratimanifest;
        protected StratiManifestXElement StratiManifest
        {
            get
            {
                if (_stratimanifest == null)
                {



                    _stratimanifest = (StratiManifestXElement)ImportStrataManifest.ImportStrata.Descendants("DeploymentExtension")
                             .Where(de => de.Attribute("RunTimeAssembly").Value.ToLower() == AssemblyFileName.ToLower())
                             ?.FirstOrDefault()
                             ?.Ancestors("StratiManifest")
                             ?.FirstOrDefault() ?? new StratiManifestXElement();

                    PackageLog.Log($"{_stratimanifest.ToString()}");

                }
                return _stratimanifest;
            }
        }
        protected virtual List<string> GetApplicableSolutions()
        {
            var solutionsList = new List<string>();

            PackageLog.Log($"OpenStrata : Getting Applicable Solutions for {this.GetType().FullName}");

            foreach (XElement solutionFile in StratiManifest.DataverseSolutions.Elements("DataverseSolutionFile"))
            {
                string sf = solutionFile.Attribute("UniqueName")?.Value;
                if (sf != null)
                {
                    PackageLog.Log($"OpenStrata : Solution {sf} is applicable to  {this.GetType().FullName}");
                    solutionsList.Add(sf);
                }
            }

            return solutionsList;
        }
        protected override bool AppliesToThisSolution(string solutionName)
        {
            //todo:  Write code to investigate the importstrata manifest to determine if the solution applies.
            return ApplicableSolutions.Contains(solutionName);
            //return (solutionName.Trim().ToLower() == SratiDataverseSolutionUniqueName.Trim().ToLower());
        }
    }
}

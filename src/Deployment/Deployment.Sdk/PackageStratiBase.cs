using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using OpenStrata.Strati.Manifest.Xml;
using OpenStrata.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        protected virtual Guid[] PluginStepsToDisable => new Guid[]{};


        protected override bool AfterPrimaryImport()
        {
            DisablePluginSteps();

            return base.AfterPrimaryImport();

        }

        protected virtual void DisablePluginSteps()
        {

            if (this.PluginStepsToDisable.Length > 0)
            {

                var pluginStepReq = new QueryExpression("sdkmessageprocessingstep")
                {
                    ColumnSet = new ColumnSet(new string[] { "sdkmessageprocessingstepid" })
                };

                pluginStepReq.Criteria.FilterOperator = LogicalOperator.Or;

                // Ensuring at least one value is checked....
                pluginStepReq.Criteria.AddCondition("sdkmessageprocessingstepid", ConditionOperator.Equal, Guid.Empty);

                foreach (var pluginStepId in this.PluginStepsToDisable)
                {
                    pluginStepReq.Criteria.AddCondition("sdkmessageprocessingstepid", ConditionOperator.Equal, pluginStepId);
                }

                var existingplugins = this.CrmSvc.RetrieveMultiple(pluginStepReq)
                                        .Entities.Select(ps => ps.Id);

                foreach (var pluginStepId in existingplugins)
                {

                        // Extra Safety check...

                        if (this.PluginStepsToDisable.Contains(pluginStepId))
                        {
                             try
                             {
                                this.PackageLog.Log($"Attempting to deactivate plugin {pluginStepId}");

                                var pluginStep = new Entity("sdkmessageprocessingstep", pluginStepId);
                                pluginStep.Attributes.Add("statecode", new OptionSetValue(1));

                                this.CrmSvc.Update(pluginStep);

                                this.PackageLog.Log($"Successfully deactivated plugin {pluginStepId}");

                            }
                            catch (Exception ex)
                            {
                                PackageLog.Log($"WARNING:  Unable to deactivate plugin {pluginStepId}");
                                PackageLog.Log(ex.Message);
                                PackageLog.Log("Continuing with the deployment.");

                             }




                        }

 
                }
            }

        }


        private Dictionary<Guid, EntityReference> _TeamBusinessUnit = new Dictionary<Guid, EntityReference>();
        protected bool TryTeamBusinessUnit(Guid teamId, out EntityReference businessUnit, EntityReference defaultValue = null)
        {
            if (!_TeamBusinessUnit.ContainsKey(teamId))
            {
                var teamBURequest = new QueryExpression("team")
                {
                    ColumnSet = new ColumnSet(new string[] { "businessunitid" })
                };
                teamBURequest.Criteria.AddCondition("teamid", ConditionOperator.Equal, teamId);

                var teamBuResult = this.CrmSvc.RetrieveMultiple(teamBURequest);

                if (teamBuResult != null && teamBuResult.Entities.Count > 0)
                {

                    businessUnit = (EntityReference)teamBuResult.Entities[0]["businessunitid"];

                    this.PackageLog.Log($"TryTeamBusinessUnit: teamid = {teamId}; businessUnit = {businessUnit.Id}");


                    _TeamBusinessUnit.Add(teamId, businessUnit);
                }
                else
                {

                    this.PackageLog.Log($"TryTeamBusinessUnit: teamid {teamId} does not exist.");

                    _TeamBusinessUnit.Add(teamId, defaultValue);
                }
            }

            businessUnit = _TeamBusinessUnit[teamId];

            this.PackageLog.Log($"TryTeamBusinessUnit: Try returned {businessUnit != defaultValue}");

            return businessUnit != defaultValue;


        }

        private Dictionary<Guid, EntityReference> _BusinessUnitDefaultTeam = new Dictionary<Guid, EntityReference>();
        protected bool TryBusinessUnitDefaultTeam(Guid businessUnitId, out EntityReference team, EntityReference defaultValue = null)
        {

            this.PackageLog.Log($"TryBusinessUnitDefaultTeam: businessUnitId = {businessUnitId}");

            if (!_BusinessUnitDefaultTeam.ContainsKey(businessUnitId))
            {
                var teamBURequest = new QueryExpression("team")
                {
                    ColumnSet = new ColumnSet(new string[] { "teamid", "businessunitid" })
                };
                teamBURequest.Criteria.AddCondition("businessunitid", ConditionOperator.Equal, businessUnitId);
                teamBURequest.Criteria.AddCondition("isdefault", ConditionOperator.Equal, true);
                teamBURequest.Criteria.AddCondition("membershiptype", ConditionOperator.Equal, 0);

                var teamBuResult = this.CrmSvc.RetrieveMultiple(teamBURequest);

                if (teamBuResult != null && teamBuResult.Entities.Count > 0)
                {
                    team = teamBuResult.Entities[0].ToEntityReference();

                    this.PackageLog.Log($"TryBusinessUnitDefaultTeam: businessUnit ({businessUnitId}) default team is {team.Id}");

                    _BusinessUnitDefaultTeam.Add(businessUnitId, team);
                }
                else
                {

                    this.PackageLog.Log($"TryBusinessUnitDefaultTeam: unable to locate default team for businessUnit ({businessUnitId}).  Business unit may not exist.");

                    _BusinessUnitDefaultTeam.Add(businessUnitId, defaultValue);
                }
            }

            team = _BusinessUnitDefaultTeam[businessUnitId];

            return team != defaultValue;
        }

        private Dictionary<string, EntityReference> _RoleGuidByName = new Dictionary<string, EntityReference>();
        protected bool TryRootRoleByName(string roleName, out EntityReference team, EntityReference defaultValue = null)
        {
            if (!_RoleGuidByName.ContainsKey(roleName))
            {
                var rootRoleIdRequest = new QueryExpression("role")
                {
                    ColumnSet = new ColumnSet(new string[] { "parentrootroleid" })
                };
                rootRoleIdRequest.Criteria.AddCondition("name", ConditionOperator.Equal, roleName);

                var rootRoleResult = this.CrmSvc.RetrieveMultiple(rootRoleIdRequest);

                if (rootRoleResult != null && rootRoleResult.Entities.Count > 0)
                {
                    team = (EntityReference)rootRoleResult.Entities[0]["parentrootroleid"];
                    _RoleGuidByName.Add(roleName, team);
                }
                else
                {
                    _RoleGuidByName.Add(roleName, defaultValue);
                }
            }

            team = _RoleGuidByName[roleName];

            return team != defaultValue;
        }

        protected void AssignRoleToBusinessUnitDefaultTeam(string roleName, Guid businessUnitId)
        {
            if (TryRootRoleByName(roleName, out EntityReference roleRef))
            {
                AssignRoleToBusinessUnitDefaultTeam(roleRef.Id, businessUnitId);
            }
        }

        protected void AssignRoleToBusinessUnitDefaultTeam(Guid rootRoleId, Guid businessUnitId)
        {

            this.PackageLog.Log($"AssignRoleToBusinessUnitDefaultTeam: businessUnitId = {businessUnitId}; rootroleid = {rootRoleId}");

            if (TryBusinessUnitDefaultTeam(businessUnitId, out EntityReference team))
            {
                AssignRoleToTeam(team.Id, rootRoleId);
            }
        }

        protected void AssignRoleToTeam(Guid teamId, string roleName)
        {
            if (TryRootRoleByName(roleName, out EntityReference roleRef))
            {
                AssignRoleToTeam(teamId, roleRef.Id);
            }
        }

        protected void AssignRoleToTeam(Guid teamId, Guid rootRoleId)
        {

            this.PackageLog.Log($"AssignRoleToTeam: teamid = {teamId}; rootroleid = {rootRoleId}");


            if (TryTeamBusinessUnit(teamId, out EntityReference businessUnit))
            {

                var fetchXml = String.Format(@"
<fetch top=""1"">
  <entity name=""role"">
    <attribute name=""roleid"" />
    <filter>
      <condition attribute=""parentrootroleid"" operator=""eq"" value=""{0}"" />
      <condition attribute=""businessunitid"" operator=""eq"" value=""{1}"" />
      <link-entity name=""teamroles"" from=""roleid"" to=""roleid"" link-type=""not any"" intersect=""true"">
        <filter>
          <condition attribute=""teamid"" operator=""eq"" value=""{2}"" />
        </filter>
      </link-entity>
    </filter>
  </entity>
</fetch>", rootRoleId, businessUnit.Id, teamId);


                this.PackageLog.Log($"AssignRoleToTeam: FetchXml");
                this.PackageLog.Log(fetchXml);

                var TeamRoleRequest = new FetchExpression(fetchXml);

                var TeamRoleResponse = this.CrmSvc.RetrieveMultiple(TeamRoleRequest);

                if (TeamRoleResponse != null && TeamRoleResponse.Entities.Count > 0)
                {
                    //Role not already assigned....

                    AssociateRequest request = new AssociateRequest()
                    {
                        Target = new EntityReference("team", teamId),
                        Relationship = new Relationship("teamroles_association"),
                        RelatedEntities = new EntityReferenceCollection(TeamRoleResponse.Entities.Select(e => e.ToEntityReference()).ToList()),
                    };

                    try
                    {
                        this.PackageLog.Log($"AssignRoleToTeam: Attempting teamroles_association AssociateRequest");
                        var result = (AssociateResponse)this.CrmSvc.Execute(request);

                        this.PackageLog.Log($"AssignRoleToTeam: AssociateRequest Successful");

                    }
                    catch (Exception ex)
                    {
                        this.PackageLog.Log($"AssignRoleToTeam: AssociateRequest Failed : {ex.GetType()} : {ex.Message}", TraceEventType.Warning);
                    }

                }
            }
            else
            {
                this.PackageLog.Log($"AssignRoleToTeam: team {teamId} businessunit.  Team may not exist in environment.");
            }
        }





    }
}

using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Messages;
using System.Security.Principal;
using Microsoft.Xrm.Sdk.Metadata;

namespace OpenStrata.Xrm.Sdk
{

    public static class UtilityExtensions
    {

        public static void PublishWorkflow (this IOrganizationService orgSvc, Workflow workflow)
        {
            var publishXml = String.Format(@"
<importexportxml>
<Workflows>
<Workflow>{0}</Workflow>
</Workflows>
</importexportxml>", workflow.Id.ToString("B"));

            try
            {
                orgSvc.Execute(new PublishXmlRequest()
                {
                    ParameterXml = publishXml,
                });
            }
            catch (Exception ex)
            {
                
            }
        }

        public static bool TryRetrieveFirstOrNull<E>(this IOrganizationService orgSvc, QueryBase query, out E entity, E defaultValue = null )
            where E : Entity
        {
            var result = orgSvc.RetrieveMultiple(query);
            entity = result.Entities.Count > 0 ? (E)result.Entities[0] : defaultValue; 
            return result.Entities.Count > 0;
        }

        public static bool TryRetrieveFirstOrNull<E>(this IOrganizationService orgSvc, string entityLogicalName, ColumnSet columnSet, string whereField, object whereValue, out E entity, E defaultValue = null)
            where E : Entity
        {
            QueryExpression findExp = new QueryExpression
            {
                EntityName = entityLogicalName,
                ColumnSet = columnSet,
                Criteria = new FilterExpression()
            };

            findExp.Criteria.AddCondition(whereField, ConditionOperator.Equal,
               whereValue);

            return orgSvc.TryRetrieveFirstOrNull(findExp, out entity, defaultValue);
        }

        public static bool TryRetrieveFirstOrNull<E>(this IOrganizationService orgSvc, string entityLogicalName, string[] fields, string whereField, object whereValue, out E entity, E defaultValue = null)
            where E : Entity
        {
            return orgSvc.TryRetrieveFirstOrNull(entityLogicalName,new ColumnSet(fields), whereField,whereValue, out entity, defaultValue);
        }

        public static E Create<E> (this E entity, IOrganizationService orgSvc)
            where E : Entity
        {
            entity.Id = orgSvc.Create(entity);
            return entity;
        }

        public static Publisher EnsurePublisher(this IOrganizationService orgSvc, string uniqueName, string customizationPrefix, string friendlyName = null, string description = null, string emailAddress = null, string supportingWebsiteUrl = null)
        {

            if (orgSvc.TryRetrieveFirstOrNull(Publisher.EntityLogicalName, new string[]{ "publisherid", "customizationprefix" }, "uniquename", uniqueName, out Publisher existingpublisher))
            {
                return existingpublisher;
            }
            else
            {
                Publisher publisher = new Publisher
                {
                    UniqueName = uniqueName,
                    CustomizationPrefix = customizationPrefix,
                    FriendlyName = friendlyName ?? uniqueName,
                    Description = description ?? friendlyName ?? uniqueName,
                    EMailAddress = emailAddress,
                    SupportingWebsiteUrl = supportingWebsiteUrl,

                };

                publisher.PublisherId = orgSvc.Create(publisher);

                return publisher;
            }

        }
        
        
        public static bool TryRetrieveSolution( this IOrganizationService orgSvc,string uniqueName, out Solution solution)
        {
            solution = null;

            return orgSvc.TryRetrieveFirstOrNull(Solution.EntityLogicalName, new ColumnSet(true), "uniquename", uniqueName, out solution);
        }
        
        public static Solution EnsureUnmanagedSolution(this IOrganizationService orgSvc, string uniqueName, string publisherUniqueName, string publisherCustomizationPrefix, string friendlyName = null, string description = null, string version = "1.0.0")
        {
            return orgSvc.EnsureUnmanagedSolution(uniqueName, orgSvc.EnsurePublisher(uniqueName, publisherCustomizationPrefix), friendlyName, description, version);
        }


        public static Solution EnsureUnmanagedSolution(this IOrganizationService orgSvc, string uniqueName, Publisher publisher, string friendlyName = null, string description = null, string version = "1.0.0")
        {
            if (orgSvc.TryRetrieveFirstOrNull(Solution.EntityLogicalName, new ColumnSet(), "uniquename", uniqueName, out Solution existingSolution))
            {
                return existingSolution;
            }
            else
            {
                return new Solution
                {
                    UniqueName = uniqueName,
                    FriendlyName = friendlyName ?? uniqueName,
                    PublisherId = publisher.ToEntityReference(),
                    Description = description ?? friendlyName ?? uniqueName,
                    Version = version,
                }.Create(orgSvc);
            }
        }


        public static void EnsureExistingSolutionEntity(this Solution solution, IOrganizationService orgSvc, string entityLogicalName)
        {
            if (orgSvc.TryRetrieveEntityMetadata(entityLogicalName,out EntityMetadata metadata))
            {
                // orgSvc.EnsureExistingComponent(componenttype.Entity, metadata.MetadataId, solution.UniqueName);
                orgSvc.EnsureExistingSolutionComponent(componenttype.Entity, metadata.MetadataId.GetValueOrDefault(), solution.UniqueName);
            }
        }

        public static bool TryRetrieveEntityMetadata (this IOrganizationService orgSvc , string entityLogicalName, out EntityMetadata entityMetadata)
        {
            RetrieveEntityRequest request = new RetrieveEntityRequest()
            {
                LogicalName = entityLogicalName
            };

            entityMetadata = ((RetrieveEntityResponse) orgSvc.Execute(request)).EntityMetadata;

            return (entityMetadata != null);
        }

        public static void EnsureExistingComponent (this Solution solution, IOrganizationService orgSvc, componenttype type, Guid componentId)
        {
            orgSvc.EnsureExistingSolutionComponent(type, componentId, solution.UniqueName);
        }

        public static void EnsureExistingComponent(this Solution solution, IOrganizationService orgSvc, int type, Guid componentId)
        {
            orgSvc.EnsureExistingSolutionComponent(type, componentId, solution.UniqueName);
        }

        public static void EnsureExistingSolutionComponent (this IOrganizationService orgSvc, componenttype type, Guid componentId, string solutionUniqueName)
        {
            AddSolutionComponentRequest addReq = new AddSolutionComponentRequest()
            {
                ComponentType = (int)type,
                ComponentId = componentId,
                SolutionUniqueName = solutionUniqueName
            };
            orgSvc.Execute(addReq);
        }

        public static void EnsureExistingSolutionComponent(this IOrganizationService orgSvc, int type, Guid componentId, string solutionUniqueName)
        {
            AddSolutionComponentRequest addReq = new AddSolutionComponentRequest()
            {
                ComponentType = type,
                ComponentId = componentId,
                SolutionUniqueName = solutionUniqueName
            };
            orgSvc.Execute(addReq);
        }

    }
}

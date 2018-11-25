using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;//used for LINQ query expression operations performed


namespace Plugin_for_retrieve_of_data_in_contact
{
    public class Retrieve : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService((Guid )context.UserId);//gets the userid under who's context the plugin was executing, remember this can vary than the current user due to impersonation in plugins

            try
            {
                if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
                {
                    if (context.Depth <= 1)
                    {
                        Entity entity = (Entity)context.InputParameters["Target"];
                        WhoAmIRequest whoreq = new WhoAmIRequest();
                        WhoAmIResponse whores = new WhoAmIResponse();
                        Guid userid = whores.UserId;//gets the current user who was running the operation in ms crm

                        QueryExpression query = new QueryExpression();
                        ConditionExpression condition = new ConditionExpression("new_user", ConditionOperator.Equal, context.UserId);
                        query.Criteria.AddCondition(condition);
                    }
                }
            }

            catch (Exception e)
            {
                throw new InvalidPluginExecutionException("User details couldnt be retrieved" + e.Message);
            }
        }
    }
}

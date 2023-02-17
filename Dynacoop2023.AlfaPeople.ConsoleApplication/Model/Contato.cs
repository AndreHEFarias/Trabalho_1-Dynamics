using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Deployment;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynacoop2023.AlfaPeople.ConsoleApplication.Model
{
    public class Contato
    {
        public CrmServiceClient ServiceClient { get; set; }

        public string LogicalnameContact { get; set; }

        public Contato(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.LogicalnameContact = "contact";
        }

        public Guid CreateContact(string contactName, string contactTel, string contactCPF, Guid accountId)
        {
            Entity contato = new Entity(this.LogicalnameContact);
            contato["firstname"] = contactName;
            contato["dcp_cpf"] = contactCPF;
            contato["telephone1"] = contactTel;
            contato["accountid"] = new EntityReference("account", accountId);
            contato["parentcustomerid"] = new EntityReference("account", accountId);

            Guid contactId = this.ServiceClient.Create(contato);
            return contactId;
        }

        public Entity GetContactByCPF(string cpf)
        {
            QueryExpression queryAccount = new QueryExpression(this.LogicalnameContact);
            queryAccount.ColumnSet.AddColumns("dcp_cpf");
            queryAccount.Criteria.AddCondition("dcp_cpf", ConditionOperator.Equal, cpf);
            return RetrieveOneContact(queryAccount);
        }
        private Entity RetrieveOneContact(QueryExpression queryAccount)
        {
            EntityCollection accounts = this.ServiceClient.RetrieveMultiple(queryAccount);

            if (accounts.Entities.Count() > 0)
                return accounts.Entities.FirstOrDefault();
            else
                return null;

        }


    }
}

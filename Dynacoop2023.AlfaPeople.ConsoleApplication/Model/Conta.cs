using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Linq;

namespace Dynacoop2023.AlfaPeople.ConsoleApplication.Model
{
    public class Conta
    {
        public CrmServiceClient ServiceClient { get; set; }

        public string LogicalnameAccount { get; set; }

        public Conta(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.LogicalnameAccount = "account";
        }

        public Guid CreateAccount(string name, decimal accountValue, int numberBranches, Guid primarycontactid, int accountType, string accountCNPJ)
        {
            Entity conta = new Entity(this.LogicalnameAccount);  
            conta["name"] = name;
            conta["dcp_numero_de_filiais"] = numberBranches;
            conta["dcp_valor"] = accountValue;
            conta["dcp_tipo_da_conta"] = new OptionSetValue(accountType);
            conta["dcp_cnpj"] = accountCNPJ;
            conta["primarycontactid"] = new EntityReference("contact", primarycontactid);

            Guid accountId = this.ServiceClient.Create(conta);
            return accountId;
        }

        public Entity GetAccountByCNPJ(string cnpj)
        {
            QueryExpression queryAccount = new QueryExpression(this.LogicalnameAccount);
            queryAccount.ColumnSet.AddColumns("dcp_cnpj");
            queryAccount.Criteria.AddCondition("dcp_cnpj", ConditionOperator.Equal, cnpj);
            return RetrieveOneAccount(queryAccount);
        }

        private Entity RetrieveOneAccount(QueryExpression queryAccount)
        {
            EntityCollection accounts = this.ServiceClient.RetrieveMultiple(queryAccount);

            if (accounts.Entities.Count() > 0)
                return accounts.Entities.FirstOrDefault();
            else
                return null;
        }

    }
}

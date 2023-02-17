using Dynacoop2023.AlfaPeople.ConsoleApplication.Model;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Rest;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynacoop2023.AlfaPeople.ConsoleApplication.Controller
{
    public class ContaController
    {
        public CrmServiceClient ServiceClient { get; set; }
        public Conta Conta { get; set; }

        public ContaController(CrmServiceClient crmServiceCliente)
        {
            ServiceClient = crmServiceCliente;
            this.Conta = new Conta(ServiceClient);
        }

        public Guid CreateAccount(string name, decimal accountValue, int numberBranches, Guid primarycontactid, int accountType, string accountCNPJ)
        {
            return Conta.CreateAccount(name, accountValue, numberBranches,primarycontactid,accountType, accountCNPJ);
        }


        public Entity GetAccountByCNPJ(string cnpj)
        {
            return Conta.GetAccountByCNPJ(cnpj);
        }

    }
}

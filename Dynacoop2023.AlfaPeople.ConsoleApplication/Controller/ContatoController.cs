using Dynacoop2023.AlfaPeople.ConsoleApplication.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace Dynacoop2023.AlfaPeople.ConsoleApplication.Controller
{
    public class ContatoController
    {
        public CrmServiceClient ServiceClient { get; set; }
        public Contato Contato { get; set; }

        public ContatoController(CrmServiceClient crmServiceCliente)
        {
            ServiceClient = crmServiceCliente;
            this.Contato = new Contato(ServiceClient);
        }
        public Guid CreateContact(string contactName, string contactTel, string contactCPF, Guid accountId)
        {
            return Contato.CreateContact(contactName, contactTel, contactCPF, accountId);
        }

        public Entity GetContactByCPF(string cpf)
        {
            return Contato.GetContactByCPF(cpf);
        }
    }
}

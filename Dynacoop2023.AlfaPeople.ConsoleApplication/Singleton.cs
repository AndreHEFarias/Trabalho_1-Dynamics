using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynacoop2023.AlfaPeople.ConsoleApplication
{
    public class Singleton
    {
        public static CrmServiceClient GetService()
        {
            string url = "org5229b951";
            string clientId = "1f9078e1-4ca6-418e-b5cc-3c088b82beb0";
            string clientSecret = "Q8c8Q~cf2SvqWoHdaxmQRX040pLBV4eBAsOpSadx";

            CrmServiceClient serviceClient = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{url}.crm2.dynamics.com/;AppId={clientId};ClientSecret={clientSecret};");

            if (!serviceClient.CurrentAccessToken.Equals(null))
                Console.WriteLine("Conexão Realizada com Sucesso");
            else
                Console.WriteLine("Erro na conexão");

            return serviceClient;
        }
    }
}

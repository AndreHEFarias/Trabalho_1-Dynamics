using Dynacoop2023.AlfaPeople.ConsoleApplication.Controller;
using Dynacoop2023.AlfaPeople.ConsoleApplication.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Dynacoop2023.AlfaPeople.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient serviceClient = Singleton.GetService();

            ContaController contaController = new ContaController(serviceClient);

            ContatoController contatoController = new ContatoController(serviceClient);

            Guid accountId = Account(contaController);

            Contact(contatoController, accountId);

            Console.ReadKey();
        }

        public static Guid Account(ContaController contaController)
        {


            Console.WriteLine("Por favor informe o nome da Conta:");
            var name = Console.ReadLine();

            Console.WriteLine("Por favor informe o Valor da Conta:");
            var accountValue = Console.ReadLine();

            Console.WriteLine("Por favor informe o Numero de filiais da Conta:");
            var numberBranches = Console.ReadLine();

            Console.WriteLine("Por favor informe o Contato Primario da Conta:");
            var primarycontactid = Console.ReadLine();

            Console.WriteLine("Por favor informe o Tipo da Conta:");
            var accountType = Console.ReadLine();

            Console.WriteLine("Por favor informe o CNPJ da Conta:");
            var accountCNPJ = Console.ReadLine();

            Entity accounts = contaController.GetAccountByCNPJ(accountCNPJ);
            if (accounts != null)
            {
                Console.WriteLine("Conta ja existe");
                Restart();
                Guid guid = Guid.NewGuid();
                return guid;

            }
            else
            {
                Guid accountId = contaController.CreateAccount(name, decimal.Parse(accountValue), int.Parse(numberBranches), Guid.Parse(primarycontactid), int.Parse(accountType), accountCNPJ);
                Console.WriteLine($"https://org5229b951.crm2.dynamics.com/main.aspx?appid=74c97688-24ae-ed11-9885-002248365eb3&forceUCI=1&pagetype=entityrecord&etn=account&id={accountId}");
                Console.WriteLine("");
                return accountId;
            }

        }

        public static void Contact(ContatoController contatoController, Guid accountId)
        {
            Console.WriteLine("Você deseja criar um contato para essa conta? (S/N)");
            var s_n = Console.ReadLine();
            if (s_n.ToString().ToUpper() == "S")
            {
                Console.WriteLine("Por favor informe o Nome do Contato:");
                var contactName = Console.ReadLine();

                Console.WriteLine("Por favor informe o telefone do Contato:");
                var contactTel = Console.ReadLine();

                Console.WriteLine("Por favor informe o CPF do Contato:");
                var contactCPF = Console.ReadLine();

                Entity contacts = contatoController.GetContactByCPF(contactCPF);
                if (contacts != null)
                {
                    Console.WriteLine("Contato ja existe");
                    Restart();
                }
                else
                {
                    Guid contactId = contatoController.CreateContact(contactName, contactTel, contactCPF, accountId);
                    Console.WriteLine($"https://org5229b951.crm2.dynamics.com/main.aspx?appid=74c97688-24ae-ed11-9885-002248365eb3&forceUCI=1&pagetype=entityrecord&etn=contact&id={contactId}");
                    Console.WriteLine("");
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }

        public static void Restart()
        {
            CrmServiceClient serviceClient = Singleton.GetService();

            ContaController contaController = new ContaController(serviceClient);

            ContatoController contatoController = new ContatoController(serviceClient);

            Guid accountId = Account(contaController);

            Contact(contatoController, accountId);

            Console.ReadKey();

            Environment.Exit(0);

        }
    }
}

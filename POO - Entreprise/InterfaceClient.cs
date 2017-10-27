using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO___Entreprise
{
    class InterfaceClient
    {
        public Entreprise Entreprise;
        private String Username;

        public InterfaceClient(Entreprise entreprise)
        {
            this.Entreprise = entreprise;
        }

        public void Start()
        {
            Console.WriteLine(this.StringStart());
            this.LogIn();
        }
        private void Run()
        {
            this.Menu();
            this.End();
        }

        private void LogIn()
        {
            Console.WriteLine("Firstname :");
            String username = Console.ReadLine();
            if (username == "admin")
            {
                this.AdminTool();
            }
            Console.WriteLine("Lastname :");
            username += Console.ReadLine();
            if (this.Entreprise.GetManagers().ContainsKey(username) | this.Entreprise.GetDirectors().ContainsKey(username))
            {
                this.Username = username;
                Console.WriteLine("You are logged in.");
                this.Run();
            }
            else
            {
                Console.WriteLine("You are not allowed to use this app (you're not a director or a manager).");
                this.LogIn();
            }
        }

        private void End()
        {
            Console.WriteLine("Print any key to quit.");
            Console.ReadLine();
        }

        private String StringStart()
        {
            String result = Environment.NewLine;
            result += Environment.NewLine;
            result += "=======================================================";
            result += Environment.NewLine;
            result += Environment.NewLine;
            result += "Welcome to " + this.Entreprise.GetName() + " Entreprise";
            result += Environment.NewLine;
            result += Environment.NewLine;
            return result;
        }

        private void AdminTool()
        {
            foreach (Manager manager in this.Entreprise.GetManagers().Values)
            {
                try
                {
                    manager.GenerateReport();
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: Couldn't generate Manager Report" + e.ToString());
                }
            }

            foreach (Director director in this.Entreprise.GetDirectors().Values)
            {
                try
                {
                    director.GenerateReport(this.Entreprise, this.Entreprise.GetDate());
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: Couldn't generate Manager Report" + e.ToString());
                }
            }

        }

        private void Menu()
        {
            List<String> listchoice = new List<String>
            {
                "Print a ManagerReport",
                "Print a FinacialDirectorReport",
                "Print a HumanRessourceReport"
            };

            String result = "What would you like to do ?" + Environment.NewLine;
            for (int i = 0; i < listchoice.Count; i++)
            {
                result += String.Format("{0}. {1}", i + 1, listchoice[i]);
                result += Environment.NewLine;
            }
            result += Environment.NewLine;
            result += String.Format("Type number in the console between 1 and {0}", listchoice.Count);
            Console.WriteLine(result);
            String task = Console.ReadLine();
            if (task == "1")
            {
                try
                {
                    this.Entreprise.GetManagers()[this.Username].GenerateReport();
                }
                catch
                {
                    Console.WriteLine("ERROR manager");
                }
            }
        }
    }
}

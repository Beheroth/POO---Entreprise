using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POO___Entreprise
{
    class Generator
    {
        private Entreprise Entreprise;

        public Generator()
        {

        }

        // Generate all the info to initialise the entreprise
        public Entreprise GenerateAll(String entreprise, DateTime date)
        {
            this.Entreprise = new Entreprise(entreprise, date);
            Console.Write("[GEN] Entreprise Loaded" + Environment.NewLine);
            this.GenerateEmploye("EmployeFile.txt");
            Console.Write("[GEN] Employees Loaded" + Environment.NewLine);
            Console.Write("Consultants : " + this.Entreprise.GetConsultants().Count + Environment.NewLine);
            Console.Write("Managers : " + this.Entreprise.GetManagers().Count + Environment.NewLine);
            Console.Write("Directors : " + this.Entreprise.GetDirectors().Count + Environment.NewLine);
            this.LinkConsultantandManager("LinkFile.txt");
            Console.Write("[GEN] Consultants linked to Manager" + Environment.NewLine);
            this.GenerateClient("ClientFile.txt");
            Console.Write("[GEN] Client Loaded" + Environment.NewLine);
            Console.Write("Clients : " + this.Entreprise.GetClients().Count + Environment.NewLine);
            this.GenerateMission("MissionFile.txt");
            Console.Write("[GEN] Mission Loaded" + Environment.NewLine);
            return this.Entreprise;
        }

        // Method to generate the instances of the entreprise

        //generate all the employees
        private void GenerateEmploye(String filename)
        {
            File employefile = new File(filename);
            // Extract each line of the file 
            foreach (string c in employefile.Load)
            {
                // Use the lines who match the pattern of the regex
                Regex rg = new Regex(@"^(?<job>[a-zA-Z]+)/(?<firstname>[a-zA-Z]+)/(?<lastname>[a-zA-Z]+)/(?<personalaccount>[0-9]+)$");
                Match m = rg.Match(c);
                if (m.Success)
                {
                    // Generate the consultants
                    if (m.Groups["job"].Value == "consultant")
                    {
                        int pa = Int32.Parse(m.Groups["personalaccount"].Value);
                        Consultant consultant = new Consultant(m.Groups["firstname"].Value, m.Groups["lastname"].Value, pa);
                        this.Entreprise.AddConsultant(consultant);
                    }

                    // Generate the directors
                    if (m.Groups["job"].Value == "director")
                    {
                        int pa = Int32.Parse(m.Groups["personalaccount"].Value);
                        Director director = new Director(m.Groups["firstname"].Value, m.Groups["lastname"].Value, pa);
                        this.Entreprise.AddDirector(director);
                    }
                    if (m.Groups["job"].Value == "financialdirector")
                    {
                        int pa = Int32.Parse(m.Groups["personalaccount"].Value);
                        FinancialDirector director = new FinancialDirector(m.Groups["firstname"].Value, m.Groups["lastname"].Value, pa);
                        this.Entreprise.AddDirector(director);
                    }
                    if (m.Groups["job"].Value == "humanresourcedirector")
                    {
                        int pa = Int32.Parse(m.Groups["personalaccount"].Value);
                        HumanResourcesDirector director = new HumanResourcesDirector(m.Groups["firstname"].Value, m.Groups["lastname"].Value, pa);
                        this.Entreprise.AddDirector(director);
                    }

                    // Generate the managers
                    if (m.Groups["job"].Value == "manager")
                    {
                        int pa = Int32.Parse(m.Groups["personalaccount"].Value);
                        Manager manager = new Manager(m.Groups["firstname"].Value, m.Groups["lastname"].Value, pa);
                        this.Entreprise.AddManager(manager);
                    }
                }
            }

        }

        private void GenerateMission(String filename)
        {
            Dictionary<String, List<Mission>> consultantagenda = new Dictionary<String, List<Mission>>();
            File missionfile = new File(filename);
            // Extract each line of the file
            foreach (string c in missionfile.Load)
            {
                // Use the lines that match the pattern of the regex
                Regex rg = new Regex(@"^(?<consultant>[a-zA-Z]+)/(?<datein>[0-9]{4}\-[0-9]{2}\-[0-9]{2})/(?<dateout>[0-9]{4}\-[0-9]{2}\-[0-9]{2})/(?<client>\w+)");
                Match m = rg.Match(c);
                if (m.Success)
                {
                    // take the consultant and the client in the lists of the entreprise
                    Consultant consultant = this.Entreprise.GetConsultants()[m.Groups["consultant"].Value];
                    Client client = this.Entreprise.GetClients()[m.Groups["client"].Value];

                    // Generate time in
                    string[] datein = m.Groups["datein"].Value.Split('-'); // format date year-month-day
                    DateTime In = new DateTime();
                    In.AddYears(Int32.Parse(datein[0]));
                    In.AddMonths(Int32.Parse(datein[1]));
                    In.AddDays(Int32.Parse(datein[2]));

                    // Generate time out
                    string[] dateout = m.Groups["datein"].Value.Split('-'); // format date year-month-day
                    DateTime Out = new DateTime();
                    Out.AddYears(Int32.Parse(dateout[0]));
                    Out.AddMonths(Int32.Parse(dateout[1]));
                    Out.AddDays(Int32.Parse(dateout[2]));

                    // Generate Mission
                    Mission mission = new Mission(In, Out, client);

                    if (consultantagenda.ContainsKey(m.Groups["consultant"].Value)) // if key in dictionary 
                    {
                        consultantagenda[m.Groups["consultant"].Value].Add(mission);
                    }
                    else
                    {
                        List<Mission> listmission = new List<Mission>();
                        listmission.Add(mission);
                        consultantagenda[m.Groups["consultant"].Value] = listmission;
                    }

                    // Put Mission in the database 
                    foreach (String consultantname in this.Entreprise.GetConsultants().Keys)
                    {
                        foreach (string consu in consultantagenda.Keys)
                        {
                            this.Entreprise.GetConsultants()[consultantname].SetMissionHistory(consultantagenda[consu]);
                        }
                    }
                }
            }
        }

        private void LinkConsultantandManager(String filename)
        {
            File linkfile = new File(filename);
            // Extract each line of the file
            foreach (string l in linkfile.Load)
            {
                Regex rg = new Regex(@"^(?<manager>[a-zA-Z]+)/(?<consultantslist>[a-zA-Z\-]+)$");
                Match m = rg.Match(l);
                if (m.Success)
                {
                    Console.WriteLine("[TEST] Link");
                    String managername = m.Groups["manager"].Value;
                    try
                    {
                        // find the Consultant and putt it in his Manager
                        Manager manager = this.Entreprise.GetManagers()[managername];
                        string[] consultants = m.Groups["consultantslist"].Value.Split('-');
                        foreach (String consultantname in consultants)
                        {
                            Consultant consultant = this.Entreprise.GetConsultants()[consultantname];
                            manager.AddConsultant(consultant);
                        }
                    }
                    catch
                    {
                        String msgERROR = "The manager :" + managername + "in the file LinkFile.txt is not find in the Entreprise";
                        Console.WriteLine(msgERROR);
                    }
                }
            }
        }

        private void GenerateClient(String filename)
        {
            File clientfile = new File(filename);
            // Extract each line of the file
            foreach (string c in clientfile.Load)
            {
                Regex rg = new Regex(@"^(?<job>[a-zA-Z]+)/(?<name>\w+)$");
                Match m = rg.Match(c);
                if (m.Success)
                {
                    if (m.Groups["job"].Value == "client")
                    {
                        // Generate the client
                        Client client = new Client(m.Groups["name"].Value);
                        this.Entreprise.AddClient(client);
                    }
                }
            }
        }
    }
}
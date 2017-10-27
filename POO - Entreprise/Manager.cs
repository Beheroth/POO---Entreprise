using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO___Entreprise
{
    class Manager : Person
    {
        private Dictionary<String, Consultant> Consultants;
        private Dictionary<String, List<Mission>> Consultantagenda;   //historique des missions

        public Manager(String firstname, String lastname, int personnalaccount) : base(firstname, lastname, personnalaccount)
        {
            this.Consultants = new Dictionary<String, Consultant>();
        }

        public void GenerateReport()
        {
            Console.WriteLine("[MAN] Generating Report");
            String name = "ManagerReport" + " - " + this.ToString() + ".txt";
            File ManagerReport = new File(name);
            Console.WriteLine("[MAN] File created");
            String txt = " ";
            foreach (Consultant consultant in Consultants.Values)
            {
                Console.WriteLine("[MAN] Looping through Consultants" + consultant.GetMission().GetClient());
                txt += String.Format("{0}   :   {1}" + Environment.NewLine,
                    consultant.ToString(),
                    consultant.GetMission().GetClient().ToString());
            }
            Console.WriteLine("[MAN] txt generated");
            ManagerReport.SaveFile(txt);
            Console.WriteLine(String.Format("[MAN] File saved as {0}", name));
        }

        //Getter - Setter

        public Dictionary<String, Consultant> GetConsultants()
        {
            return this.Consultants;
        }

        public Dictionary<String, List<Mission>> GetConsultantagenda()
        {
            return this.Consultantagenda;
        }

        // Method

        public void AddConsultant(Consultant consultant)
        {
            //Assert that consultant is not already contained in Consultants Dictionary
            //BEWARE: Currently Shallow copy of consultant object=> can create problems!!!
            this.Consultants.Add(consultant.GetFirstname() + consultant.GetLastname(), consultant);
        }

        public void RemoveConsultant(String id)
        {
            //assert that id exist before removing
            if (this.Consultants.ContainsKey(id))
            {
                this.Consultants.Remove(id);
            }

        }

        public void LoadConsultants(Dictionary<String, Consultant> consultants)
        {
            this.Consultants = consultants;
        }

        public void AddConsultantmissions(List<Mission> consultantmissions, String consultantname)
        {
            if (this.Consultantagenda.ContainsKey(consultantname))
            {
                foreach (Mission mission in consultantmissions)
                {
                    this.Consultantagenda[consultantname].Add(mission);
                }
            }
            this.Consultantagenda[consultantname] = consultantmissions;
        }
        public void LoadConsultantagenda(Dictionary<String, List<Mission>> consultantagenda)
        {
            this.Consultantagenda = consultantagenda;
        }

        public int NumberConsultant()
        {
            //See how many Consultant are under the manager
            return this.Consultants.Count;
        }

    }
}


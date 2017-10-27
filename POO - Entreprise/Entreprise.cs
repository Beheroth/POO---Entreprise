using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO___Entreprise
{
    class Entreprise : IClient
    {
        private string Name;
        private Dictionary<string, Director> Directors;
        private Dictionary<string, Manager> Managers;
        private Dictionary<string, Consultant> Consultants;
        private Dictionary<string, Client> Clients;
        private DateTime Date;

        public Entreprise(string name, DateTime date)
        {
            this.Name = name;
            this.Directors = new Dictionary<string, Director>();
            this.Managers = new Dictionary<string, Manager>();
            this.Consultants = new Dictionary<string, Consultant>();
            this.Clients = new Dictionary<string, Client>();
            this.Date = date;
        }

        // getter-setter

        public DateTime GetDate()
        {
            return this.Date;
        }

        public Dictionary<string, Director> GetDirectors()
        {
            return this.Directors;
        }

        public Dictionary<string, Manager> GetManagers()
        {
            return this.Managers;
        }

        public Dictionary<string, Consultant> GetConsultants()
        {
            return this.Consultants;
        }

        public Dictionary<string, Client> GetClients()
        {
            return this.Clients;
        }

        public string GetName()
        {
            return this.Name;
        }


        // don't use Loadxxx to generate

        //BEWARE OF THE SHALLOW COPIES!!!

        public void LoadDirectors(Dictionary<String, Director> directors)
        {
            this.Directors = directors;
        }

        public void LoadManagers(Dictionary<String, Manager> managers)
        {
            this.Managers = managers;
        }

        public void LoadConsultants(Dictionary<String, Consultant> consultants)
        {
            this.Consultants = consultants;
        }

        public void LoadClients(Dictionary<String, Client> clients)
        {
            this.Clients = clients;
        }

        //Adding Attributes

        public void AddDirector(Director director)
        {
            this.Directors.Add(director.ToString(), director);
        }

        public void AddManager(Manager manager)
        {
            this.Managers.Add(manager.ToString(), manager);
        }

        public void AddConsultant(Consultant consultant)
        {
            this.Consultants.Add(consultant.ToString(), consultant);
        }

        public void AddClient(Client client)
        {
            this.Clients.Add(client.ToString(), client);
        }
    }
}



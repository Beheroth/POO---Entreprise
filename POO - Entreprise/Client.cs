using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO___Entreprise
{
    class Client : IClient
    {
        String Name;

        public Client(String Name)
        {
            this.Name = Name;
        }

        public override String ToString()
        {
            return this.Name;
        }
    }
}
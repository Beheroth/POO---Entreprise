using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO___Entreprise
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime date = new DateTime(2017, 10, 23);
            String name = "GE";

            Generator gen = new Generator();
            Entreprise entreprise = gen.GenerateAll(name, date);

            InterfaceClient show = new InterfaceClient(entreprise);
            show.Start();
        }
    }
}
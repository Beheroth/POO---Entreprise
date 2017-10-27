using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO___Entreprise
{
    abstract class Person
    {
        private readonly String Firstname;
        private readonly String Lastname;
        private int Personnalaccount;

        public Person(String firstname, String lastname, int personnalaccount)
        {
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Personnalaccount = personnalaccount;
        }

        public override string ToString()
        {
            return this.Firstname + this.Lastname;
        }


        //getter - setter

        public String GetFirstname()
        {
            return this.Firstname;
        }

        public String GetLastname()
        {
            return this.Lastname;
        }

        public int GetPersonnalaccount()
        {
            return this.Personnalaccount;
        }

        public void SetPersonnalaccount(int amount)
        {
            this.Personnalaccount = amount;
        }


        //methods

        public virtual void GetPaid(int salary)
        {
            this.SetPersonnalaccount(this.GetPersonnalaccount() + salary);
        }
    }
}
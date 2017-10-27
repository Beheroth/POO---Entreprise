using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO___Entreprise
{
    class FinancialDirector : Director
    {
        public FinancialDirector(
            String firstname,
            String lastname,
            int personnalaccount) :
            base(firstname, lastname, personnalaccount)
        {

        }

        public override void GenerateReport(Entreprise entreprise, DateTime date)
        {
            Console.WriteLine("[FD] Generating Report");
            File FDReport = new File("Financial Report - " + this.ToString() + date.ToString() + ".txt");
            string report = String.Format("- Relevé des salaires au sein de {0} - {1}\r \r", entreprise.GetName(), date.Year);
            report += this.GenerateDirectorsSalary(entreprise, date) + this.GenerateManagersSalary(entreprise, date) + this.GenerateConsultantsSalary(entreprise, date);
            Console.Write(report);
            FDReport.SaveFile(report);
        }

        private string GenerateDirectorsSalary(Entreprise entreprise, DateTime date)
        {
            string report = " - Directors: \r \r";
            foreach (Director director in entreprise.GetDirectors().Values)
            {
                report += String.Format("   - {0} {1} - {2} - {3} €\r",
                    director.GetFirstname(),
                    director.GetLastname(),
                    director.GetType(),
                    150000);
            }
            return report + "\r";
        }

        private string GenerateManagersSalary(Entreprise entreprise, DateTime date)
        {
            string report = " - Managers: \r \r";
            foreach (Manager manager in entreprise.GetManagers().Values)
            {
                report += String.Format("   - {0} {1} - {2} €\r",
                    manager.GetFirstname(),
                    manager.GetLastname(),
                    60000 + 500 * manager.NumberConsultant());
            }
            return report + "\r";
        }

        private string GenerateConsultantsSalary(Entreprise entreprise, DateTime date)
        {
            string report = " - Consultants: \r \r";
            foreach (Manager manager in entreprise.GetManagers().Values)
            {
                foreach (Consultant consultant in manager.GetConsultants().Values)
                {
                    int bonus = (60000 + 500 * manager.NumberConsultant()) / 100;
                    foreach (Mission mission in consultant.GetMissionsFromYear(date))
                    {
                        int bounty = 250;
                        Console.WriteLine("GENE SAL CON: " + mission.GetClient().GetType());
                        if (mission.GetClient() is Entreprise)
                        {
                            bounty = -10 * mission.GetDuration();
                        }
                        bonus += bounty;
                    }

                    report += String.Format("   - {0} {1} - {2} €\r",
                        consultant.GetFirstname(),
                        consultant.GetLastname(),
                        30000 + bonus);
                }
            }
            return report + "\r";
        }
    }
}


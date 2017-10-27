using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO___Entreprise
{
    class HumanResourcesDirector : Director
    {
        public HumanResourcesDirector(String firstname, String lastname, int personnalaccount) :
            base(firstname, lastname, personnalaccount)
        {

        }

        public override void GenerateReport(Entreprise entreprise, DateTime date)
        {
            String name = "HR Report" + " - " + this.ToString() + ".txt";
            File HRDReport = new File(name);
            String txt = "";
            Dictionary<IClient, List<List<String>>> data = new Dictionary<IClient, List<List<String>>>();
            foreach (Consultant consultant in entreprise.GetConsultants().Values)
            {
                foreach (Mission mission in consultant.GetMissionHistory())
                {
                    IClient client = mission.GetClient();
                    List<String> info = new List<String>();
                    info.Add(consultant.ToString());
                    info.Add(mission.GetStart().ToString());
                    info.Add(mission.GetEnd().ToString());
                    data[client].Add(info);
                }
            }
            foreach (Client client in data.Keys)
            {
                txt += client.ToString();
                txt += Environment.NewLine;
                txt += Environment.NewLine;

                foreach (List<String> mission in data[client])
                {
                    txt += mission.ToString();
                    txt += Environment.NewLine;
                }
            }
            HRDReport.SaveFile(txt);
        }
    }

}

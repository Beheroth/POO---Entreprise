using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO___Entreprise
{
    class Consultant : Person
    {
        private Mission Mission;
        private List<Mission> MissionHistory; //Sorted List of All the missions the consultant has done or is doing.

        public Consultant(String firstname, String lastname, int personnalaccount) : base(firstname, lastname, personnalaccount)
        {
            this.MissionHistory = new List<Mission>();
        }

        //getter-setter

        //BEWARE OF SHALLOW COPIES!

        public Mission GetMission()
        {
            return this.Mission;
        }

        public List<Mission> GetMissionHistory()
        {
            return this.MissionHistory;
        }

        public void SetMission(Mission mission)
        {
            this.Mission = mission;
        }

        public void SetMissionHistory(List<Mission> var)
        {
            this.MissionHistory = var;
            this.MissionHistory.OrderBy(x => x.GetStart());
        }


        //methods


        public List<Mission> GetMissionsFromYear(DateTime date)
        {
            List<Mission> ans = new List<Mission>();
            foreach (Mission mission in this.GetMissionHistory())
            {
                if (mission.GetEnd().Year == date.Year)
                {
                    ans.Add(mission);
                }
            }
            return ans;
        }

        /*
        public void CompleteMission(Entreprise entreprise)
        {
            this.MissionHistory.OrderBy(x => x.GetStart());
            DateTime date1 = new DateTime(2017, 01, 01);
            foreach(Mission mission in MissionHistory)
            {
                if (date1 <= mission.GetStart())
                {
                    Mission missionentreprise = new Mission(date1, mission.GetStart().AddDays(-1), entreprise);
                    this.MissionHistory.Add(missionentreprise);
                    this.MissionHistory.OrderBy(x => x.GetStart());
                    date1 = mission.GetEnd().AddDays(1);
                }
                else
                {
                    date1 = mission.GetEnd().AddDays(1);
                }
            }
        }
        */
    }
}

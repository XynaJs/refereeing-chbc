using System;
using System.Collections.Generic;
using System.Text;
using static RefereeingCHBC.Classes.Helpers.ClassEnums;

namespace RefereeingCHBC.Classes
{
    public class Report
    {
        //Entete
        public int Id { get; set; }
        public DateTime DateBegin { get; set; }
        public Championship Championship { get; set; }
        public Club ClubHome { get; set; }
        public int NbGoalsClubHome { get; set; }
        public Club ClubVisitor { get; set; }
        public int NbGoalsClubVisitor { get; set; }
        public Referee Referee1 { get; set; }
        public Referee Referee2 { get; set; }
        public int IdSupervisor { get; set; }

        //Corps du suivi
        //A
        public RateReport A1Rate { get; set; }
        public string A1Comment { get; set; }
        public RateReport A2Rate { get; set; }
        public string A2Comment { get; set; }
        public RateReport A3Rate { get; set; }
        public string A3Comment { get; set; }
        public RateReport A4Rate { get; set; }
        public string A4Comment { get; set; }
        //B
        public RateReport B1Rate { get; set; }
        public string B1Comment { get; set; }
        public RateReport B2Rate { get; set; }
        public string B2Comment { get; set; }
        public RateReport B3Rate { get; set; }
        public string B3Comment { get; set; }
        //C
        public RateReport C1Rate { get; set; }
        public string C1Comment { get; set; }
        public RateReport C2Rate { get; set; }
        public string C2Comment { get; set; }
        public RateReport C3Rate { get; set; }
        public string C3Comment { get; set; }
        public RateReport C4Rate { get; set; }
        public string C4Comment { get; set; }
        public RateReport C5Rate { get; set; }
        public string C5Comment { get; set; }

        //Pied
        public int TotalRateA { get; set; }
        public int TotalRateB { get; set; }
        public int TotalRateC { get; set; }
        public int TotalRate { get; set; }
        public string Comment { get; set; }
    }
}

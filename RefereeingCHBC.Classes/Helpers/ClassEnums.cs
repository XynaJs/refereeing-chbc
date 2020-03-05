using System;
using System.Collections.Generic;
using System.Text;

namespace RefereeingCHBC.Classes.Helpers
{
    public class ClassEnums
    {
        public enum RateReport
        {
            Corriger,Approfondir,Satisfaisant,Maitrise
        }

        /// <summary>
        /// Différentes chaînes de connections
        /// </summary>
        public static class ConnectionStrings
        {
            public static string ConnectionStringToto = "Driver={SQL Server Native Client 11.0};server=PC-CARUCHETDEV\\MSSQL2019;database=dbRefereeingCHBC;Uid=sa;Pwd=Thomasdu06510;";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RefereeingCHBC.Datas.Helpers
{
    public class Enums
    {
        public enum OdbcCommandType
        {
            Fill,
            ExecuteNonQuery
        }
        public enum RateReport
        {
            Corriger = 1,Approfondir = 2,Satisfaisant = 3,Maitrise = 4
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

using System;
using System.Collections.Generic;
using System.Text;

namespace RefereeingCHBC.Classes
{
    public class Referee
    {
        public int Id { get; set; }
        public string NameLast { get; set; }
        public string NameFirst { get; set; }
        public Championship Championship { get; set; }
        public Club Club { get; set; }
        public bool Enabled { get; set; }
    }
}

using RefereeingCHBC.Datas.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Text;

namespace RefereeingCHBC.Datas
{
    public class Supervisor
    {
        public int Id { get; set; } //Correspond à l'ID ApplicationUser
        public string NameLast { get; set; }
        public string NameFirst { get; set; }
        public string Email { get; set; }
        public Club Club { get; set; }
        public Guid UidUser { get; set; }
    }

    public class Supervisors
    {
        public static List<Supervisor> GetSupervisors()
        {
            //On créé une liste vide
            List<Supervisor> Items = new List<Supervisor>();
            //On créé une commande basé sur notre requpête puis sur la connexion
            OdbcCommand cmd = new OdbcCommand($"SELECT * FROM tbSupervisor", Security.OdbcConnection);

            Args args = Security.ExecuteOdbcCommand(cmd, Enums.OdbcCommandType.Fill);

            if (args.Result)
            {
                //On traite notre table de données
                //On passe tous les enregistrements
                foreach (DataRow item in args.DataTable.Rows) { Items.Add(GetSupervisor(item)); }
            }
            else
            {
                throw new Exception(args.Message);
            }

            //On renvoie notre liste
            return Items;
        }

        public static Supervisor GetSupervisorById(int Id)
        {
            //On créé une liste vide
            Supervisor Item = new Supervisor();
            //On créé une commande basé sur notre requpête puis sur la connexion
            OdbcCommand cmd = new OdbcCommand($"SELECT * FROM tbSupervisor WHERE Id = ?", Security.OdbcConnection);
            cmd.Parameters.Add("@Id", OdbcType.Int).Value = Id;
            Args args = Security.ExecuteOdbcCommand(cmd, Enums.OdbcCommandType.Fill);

            if (args.Result)
            {
                //On traite notre table de données
                //On passe tous les enregistrements
                foreach (DataRow item in args.DataTable.Rows) { Item = GetSupervisor(item); }
            }
            else
            {
                throw new Exception(args.Message);
            }

            //On renvoie notre liste
            return Item;
        }

        public static Supervisor GetSupervisor(DataRow dataRow)
        {
            Supervisor supervisor = new Supervisor();
            supervisor.Club = new Club();

            supervisor.Id = dataRow.IsNull("Id") ? 0 : (int)dataRow["Id"];
            supervisor.NameFirst = dataRow.IsNull("NameFirst") ? String.Empty : (string)dataRow["NameFirst"];
            supervisor.NameLast = dataRow.IsNull("NameLast") ? String.Empty : (string)dataRow["NameLast"];
            supervisor.Email = dataRow.IsNull("Email") ? String.Empty : (string)dataRow["Email"];
            supervisor.Club.Id = dataRow.IsNull("IdClub") ? 0 : (int)dataRow["IdClub"];
            supervisor.Club = Clubs.GetClubById(supervisor.Club.Id);
            supervisor.UidUser = dataRow.IsNull("UidUser") ? Guid.Empty : (Guid)dataRow["UidUser"];

            return supervisor;
        }
    }
}

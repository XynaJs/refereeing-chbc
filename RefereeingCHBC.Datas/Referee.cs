using RefereeingCHBC.Datas.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Odbc;
using System.Text;

namespace RefereeingCHBC.Datas
{
    public class Referee
    {
        [DisplayName("Id de l'Arbitre")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Nom de l'Arbitre")]
        public string NameLast { get; set; }
        [Required]
        [DisplayName("Prénom de l'Arbitre")]
        public string NameFirst { get; set; }
        [Required]
        [DisplayName("Championnat de l'Arbitre")]
        public Championship Championship { get; set; }
        [Required]
        [DisplayName("Club de l'Arbitre")]
        public Club Club { get; set; }
        [Required]
        [DisplayName("Activé ?")]
        public bool Enabled { get; set; }
    }

    public class Referees
    {
        public static List<Referee> GetReferees()
        {
            //On créé une liste vide
            List<Referee> Items = new List<Referee>();
            //On créé une commande basé sur notre requpête puis sur la connexion
            OdbcCommand cmd = new OdbcCommand($"SELECT * FROM tbReferee", Security.OdbcConnection);

            Args args = Security.ExecuteOdbcCommand(cmd, Enums.OdbcCommandType.Fill);

            if (args.Result)
            {
                //On traite notre table de données
                //On passe tous les enregistrements
                foreach (DataRow item in args.DataTable.Rows) { Items.Add(GetReferee(item)); }
            }
            else
            {
                throw new Exception(args.Message);
            }

            //On renvoie notre liste
            return Items;
        }

        public static Referee GetRefereeById(int? Id = 0)
        {
            //On créé une liste vide
            Referee Item = new Referee();
            //On créé une commande basé sur notre requpête puis sur la connexion
            OdbcCommand cmd = new OdbcCommand($"SELECT * FROM tbReferee WHERE Id = ?", Security.OdbcConnection);
            cmd.Parameters.Add("@Id", OdbcType.Int).Value = Id;
            Args args = Security.ExecuteOdbcCommand(cmd, Enums.OdbcCommandType.Fill);

            if (args.Result)
            {
                //On traite notre table de données
                //On passe tous les enregistrements
                foreach (DataRow item in args.DataTable.Rows) { Item = GetReferee(item); }
            }
            else
            {
                throw new Exception(args.Message);
            }

            //On renvoie notre liste
            return Item;
        }

        public static Referee GetReferee(DataRow dataRow)
        {
            Referee referee = new Referee();
            referee.Championship = new Championship();
            referee.Club = new Club();

            referee.Id = dataRow.IsNull("Id") ? 0 : (int)dataRow["Id"];
            referee.NameFirst = dataRow.IsNull("NameFirst") ? String.Empty : (string)dataRow["NameFirst"];
            referee.NameLast = dataRow.IsNull("NameLast") ? String.Empty : (string)dataRow["NameLast"];
            referee.Championship.Id = dataRow.IsNull("IdChampionship") ? 0 : (int)dataRow["IdChampionship"];
            referee.Championship = Championships.GetChampionshipById(referee.Championship.Id);
            referee.Club.Id = dataRow.IsNull("IdClub") ? 0 : (int)dataRow["IdClub"];
            referee.Club = Clubs.GetClubById(referee.Club.Id);
            referee.Enabled = dataRow.IsNull("Enabled") ? true : (bool)dataRow["Enabled"];


            return referee;
        }
    }
}

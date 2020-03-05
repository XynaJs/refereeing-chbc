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
    public class Club
    {
        [DisplayName("Id du Club")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Nom du Club")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Code Postal du Club")]
        public string ZipCode { get; set; }
        [Required]
        [DisplayName("Ville du Club")]
        public string City { get; set; }
    }

    public class Clubs
    {
        public static List<Club> GetClubs()
        {
            //On créé une liste vide
            List<Club> Items = new List<Club>();
            //On créé une commande basé sur notre requpête puis sur la connexion
            OdbcCommand cmd = new OdbcCommand($"SELECT * FROM tbClub", Security.OdbcConnection);

            Args args = Security.ExecuteOdbcCommand(cmd, Enums.OdbcCommandType.Fill);

            if (args.Result)
            {
                //On traite notre table de données
                //On passe tous les enregistrements
                foreach (DataRow item in args.DataTable.Rows) { Items.Add(GetClub(item)); }
            }
            else
            {
                throw new Exception(args.Message);
            }

            //On renvoie notre liste
            return Items;
        }

        public static Club GetClubById(int Id)
        {
            //On créé une liste vide
            Club Item = new Club();
            //On créé une commande basé sur notre requpête puis sur la connexion
            OdbcCommand cmd = new OdbcCommand($"SELECT * FROM tbClub WHERE Id = ?", Security.OdbcConnection);
            cmd.Parameters.Add("@Id", OdbcType.Int).Value = Id;
            Args args = Security.ExecuteOdbcCommand(cmd, Enums.OdbcCommandType.Fill);

            if (args.Result)
            {
                //On traite notre table de données
                //On passe tous les enregistrements
                foreach (DataRow item in args.DataTable.Rows) { Item = GetClub(item); }
            }
            else
            {
                throw new Exception(args.Message);
            }

            //On renvoie notre liste
            return Item;
        }

        public static Club GetClub(DataRow dataRow)
        {
            Club club = new Club();

            club.Id = dataRow.IsNull("Id") ? 0 : (int)dataRow["Id"];
            club.Name = dataRow.IsNull("Name") ? String.Empty : (string)dataRow["Name"];
            club.City = dataRow.IsNull("City") ? String.Empty : (string)dataRow["City"];
            club.ZipCode = dataRow.IsNull("ZipCode") ? String.Empty : (string)dataRow["ZipCode"];

            return club;
        }
    }
}

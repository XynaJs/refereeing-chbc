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
    public class Championship
    {
        [DisplayName("Id du Championnat")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Nom du Championnat")]
        public string Name { get; set; }
    }

    public class Championships
    {
        public static List<Championship> GetChampionships()
        {
            //On créé une liste vide
            List<Championship> Items = new List<Championship>();
            //On créé une commande basé sur notre requpête puis sur la connexion
            OdbcCommand cmd = new OdbcCommand($"SELECT * FROM tbChampionship", Security.OdbcConnection);

            Args args = Security.ExecuteOdbcCommand(cmd, Enums.OdbcCommandType.Fill);

            if (args.Result)
            {
                //On traite notre table de données
                //On passe tous les enregistrements
                foreach (DataRow item in args.DataTable.Rows) { Items.Add(GetChampionship(item)); }
            }
            else
            {
                throw new Exception(args.Message);
            }

            //On renvoie notre liste
            return Items;
        }

        public static Championship GetChampionshipById(int Id)
        {
            //On créé une liste vide
            Championship Item = new Championship();
            //On créé une commande basé sur notre requpête puis sur la connexion
            OdbcCommand cmd = new OdbcCommand($"SELECT * FROM tbChampionship WHERE Id = ?", Security.OdbcConnection);
            cmd.Parameters.Add("@Id", OdbcType.Int).Value = Id;
            Args args = Security.ExecuteOdbcCommand(cmd, Enums.OdbcCommandType.Fill);

            if (args.Result)
            {
                //On traite notre table de données
                //On passe tous les enregistrements
                foreach (DataRow item in args.DataTable.Rows) { Item = GetChampionship(item); }
            }
            else
            {
                throw new Exception(args.Message);
            }

            //On renvoie notre liste
            return Item;
        }

        public static Championship GetChampionship(DataRow dataRow)
        {
            Championship championship = new Championship();

            championship.Id = dataRow.IsNull("Id") ? 0 : (int)dataRow["Id"];
            championship.Name = dataRow.IsNull("Name") ? String.Empty : (string)dataRow["Name"];

            return championship;
        }
    }
}

using RefereeingCHBC.Datas.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Odbc;
using System.Text;

namespace RefereeingCHBC.Datas
{
    public class Game
    {
        [DisplayName("Id du Match")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Date du Début du Match")]
        public DateTime DateBegin { get; set; }
        [Required]
        [DisplayName("Championnat")]
        public Championship Championship { get; set; }
        [Required]
        [DisplayName("Club Recevant")]
        public Club ClubHome { get; set; }
        [Required]
        [DisplayName("Score du Club Recevant")]
        public int NbGoalsClubHome { get; set; }
        [Required]
        [DisplayName("Club Visiteur")]
        public Club ClubVisitor { get; set; }
        [Required]
        [DisplayName("Score du Club Visiteur")]
        public int NbGoalsClubVisitor { get; set; }
        [Required]
        [DisplayName("Arbitre 1")]
        public Referee Referee1 { get; set; }
        [Required]
        [DisplayName("Arbitre 2 (facultatif)")]
        public Referee Referee2 { get; set; }
        [Required]
        [DisplayName("Rapport/Suivi")]
        public Report? Report { get; set; }
    }

    public class Games
    {
        public static int InsertGame(Game game)
        {
            //On créé une commande avec notre petite requête et basée sur la connexion
            OdbcCommand cmd = new OdbcCommand($"INSERT INTO [tbGame] " +
                $"([DateBegin]" +
                $",[IdChampionship]" +
                $",[IdClubHome]" +
                $",[NbGoalsClubHome]" +
                $",[IdClubVisitor]" +
                $",[NbGoalsClubVisitor]" +
                $",[IdReferee1]" +
                $",[IdReferee2]" +
                $",[IdReport])" +
                $" VALUES " +
                $"(?,?,?,?,?,?,?,?,?)", Security.OdbcConnection);
            //On ajoute les paramètres
            cmd.Parameters.Add("@DateBegin", OdbcType.DateTime).Value = game.DateBegin;
            cmd.Parameters.Add("@IdChampionship", OdbcType.Int).Value = game.Championship.Id;
            cmd.Parameters.Add("@IdClubHome", OdbcType.Int).Value = game.ClubHome.Id;
            cmd.Parameters.Add("@NbGoalsClubHome", OdbcType.Int).Value = game.NbGoalsClubHome;
            cmd.Parameters.Add("@IdClubVisitor", OdbcType.Int).Value = game.ClubVisitor.Id;
            cmd.Parameters.Add("@NbGoalsClubVisitor", OdbcType.Int).Value = game.NbGoalsClubVisitor;
            cmd.Parameters.Add("@IdReferee1", OdbcType.Int).Value = game.Referee1.Id;
            cmd.Parameters.Add("@IdReferee2", OdbcType.Int).Value = game.Referee2.Id;
            cmd.Parameters.Add("@IdReport", OdbcType.Int).Value = game.Report.Id;

            Args args = Security.ExecuteOdbcCommand(cmd, Enums.OdbcCommandType.ExecuteNonQuery);

            if (args.Result)
            {
                return args.RowsAffected;
            }
            else
            {
                throw new Exception(args.Message);
            }
        }
    }
}

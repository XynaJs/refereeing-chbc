using RefereeingCHBC.Datas.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Odbc;
using System.Text;
using static RefereeingCHBC.Datas.Helpers.Enums;

namespace RefereeingCHBC.Datas
{
    public class Report
    {
        //Entete
        [DisplayName("Numéro du Rapport")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Date et Heure du Match")]
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
        [DisplayName("Accompagnateur")]
        public Supervisor Supervisor { get; set; }

        //Corps du suivi
        //A
        [Required]
        [DisplayName("A1 - Comprend le jeu, N'arrête pas forcément le jeu")]
        public RateReport A1Rate { get; set; }
        [Required]
        [DisplayName("A1 - Commentaire")]
        public string? A1Comment { get; set; } 
        [Required]
        [DisplayName("A2 - Appréciation des duels (Contacts) entre attaquants et défenseurs")]
        public RateReport A2Rate { get; set; }
        [Required]
        [DisplayName("A2 - Commentaire")]
        public string? A2Comment { get; set; }
        [Required]
        [DisplayName("A3 - Sanctions sportives, Jet Franc - 7M, Cohérence")]
        public RateReport A3Rate { get; set; }
        [Required]
        [DisplayName("A3 - Commentaire")]
        public string? A3Comment { get; set; }
        [Required]
        [DisplayName("A4 - Sanctions disciplinaires, Echelle des sanctions, Sanction Adaptée, Proportionnalité: Sportive / Disciplinaire")]
        public RateReport A4Rate { get; set; }
        [Required]
        [DisplayName("A4 - Commentaire")]
        public string? A4Comment { get; set; }
        //B
        [Required]
        [DisplayName("B1 - Placement et déplacement, Coopération, Arbitre domine l'autre")]
        public RateReport B1Rate { get; set; }
        [Required]
        [DisplayName("B1 - Commentaire")]
        public string? B1Comment { get; set; }
        [Required]
        [DisplayName("B2 - Geste et coup de sifflet, Communication")]
        public RateReport B2Rate { get; set; }
        [Required]
        [DisplayName("B2 - Commentaire")]
        public string? B2Comment { get; set; }
        [Required]
        [DisplayName("B3 - Personnalité : autorité, Responsable / décideur - Réactif")]
        public RateReport B3Rate { get; set; }
        [Required]
        [DisplayName("B3 - Commentaire")]
        public string? B3Comment { get; set; }
        //C
        [Required]
        [DisplayName("C1 - Marcher - dribble - pied, 3 secondes")]
        public RateReport C1Rate { get; set; }
        [Required]
        [DisplayName("C1 - Commentaire")]
        public string? C1Comment { get; set; }
        [Required]
        [DisplayName("C2 - Empiètement - Renvoi, Défense en zone, But après retombée en zone")]
        public RateReport C2Rate { get; set; }
        [Required]
        [DisplayName("C2 - Commentaire")]
        public string? C2Comment { get; set; }
        [Required]
        [DisplayName("C3 - Jeu passif")]
        public RateReport C3Rate { get; set; }
        [Required]
        [DisplayName("C3 - Commentaire")]
        public string? C3Comment { get; set; } 
        [Required]
        [DisplayName("C4 - Autres règles, Engagement - Ordre (lieu des Jets)")]
        public RateReport C4Rate { get; set; }
        [Required]
        [DisplayName("C4 - Commentaire")]
        public string? C4Comment { get; set; }
        [Required]
        [DisplayName("C5 - Impression générale et autres")]
        public RateReport C5Rate { get; set; }
        [Required]
        [DisplayName("C5 - Commentaire")]
        public string? C5Comment { get; set; }

        //Pied
        [Required]
        [DisplayName("Total Notes en A - Lecture du Jeu")]
        public int TotalRateA { get; set; }
        [Required]
        [DisplayName("Total Notes en B - Technique")]
        public int TotalRateB { get; set; }
        [Required]
        [DisplayName("Total Notes en C - Action")]
        public int TotalRateC { get; set; }
        [Required]
        [DisplayName("Note Globale /50")]
        public int TotalRate { get; set; }
        [Required]
        [DisplayName("Commentaire Général (facultatif)")]
        public string? Comment { get; set; }
    }

    public class Reports
    {
        public static List<Report> GetReports()
        {
            //On créé une liste vide
            List<Report> Items = new List<Report>();
            //On créé une commande basé sur notre requpête puis sur la connexion
            OdbcCommand cmd = new OdbcCommand($"SELECT * FROM tbReport", Security.OdbcConnection);

            Args args = Security.ExecuteOdbcCommand(cmd, Enums.OdbcCommandType.Fill);

            if (args.Result)
            {
                //On traite notre table de données
                //On passe tous les enregistrements
                foreach (DataRow item in args.DataTable.Rows) { Items.Add(GetReport(item)); }
            }
            else
            {
                throw new Exception(args.Message);
            }

            //On renvoie notre liste
            return Items;
        }

        public static int InsertReport(Report report)
        {
            //On créé une commande avec notre petite requête et basée sur la connexion
            OdbcCommand cmd = new OdbcCommand($"INSERT INTO tbReport (" +
                $"[DateBegin]," +
                $"[IdChampionship]," +
                $"[IdClubHome]," +
                $"[NbGoalsClubHome]," +
                $"[IdClubVisitor]," +
                $"[NbGoalsClubVisitor]" +
                $",[IdReferee1]" +
                $",[IdReferee2]" +
                $",[IdSupervisor]" +
                $",[A1Rate]" +
                $",[A1Comment]" +
                $",[A2Rate]" +
                $",[A2Comment]" +
                $",[A3Rate]" +
                $",[A3Comment]" +
                $",[A4Rate]" +
                $",[A4Comment]" +
                $",[B1Rate]" +
                $",[B1Comment]" +
                $",[B2Rate]" +
                $",[B2Comment]" +
                $",[B3Rate]" +
                $",[B3Comment]" +
                $",[C1Rate]" +
                $",[C1Comment]" +
                $",[C2Rate]" +
                $",[C2Comment]" +
                $",[C3Rate]" +
                $",[C3Comment]" +
                $",[C4Rate]" +
                $",[C4Comment]" +
                $",[C5Rate]" +
                $",[C5Comment]" +
                $",[TotalRateA]" +
                $",[TotalRateB]" +
                $",[TotalRateC]" +
                $",[TotalRate]" +
                $",[Comment])" +
                $" VALUES " +
                $"(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", Security.OdbcConnection);
            //On ajoute les paramètres
            //Entete
            cmd.Parameters.Add("@DateBegin", OdbcType.DateTime).Value = report.DateBegin;
            cmd.Parameters.Add("@IdChampionship", OdbcType.Int).Value = report.Championship.Id;
            cmd.Parameters.Add("@IdClubHome", OdbcType.Int).Value = report.ClubHome.Id;
            cmd.Parameters.Add("@NbGoalsClubHome", OdbcType.Int).Value = report.NbGoalsClubHome;
            cmd.Parameters.Add("@IdClubVisitor", OdbcType.Int).Value = report.ClubVisitor.Id;
            cmd.Parameters.Add("@NbGoalsClubVisitor", OdbcType.Int).Value = report.NbGoalsClubVisitor;
            cmd.Parameters.Add("@IdReferee1", OdbcType.Int).Value = report.Referee1.Id;
            cmd.Parameters.Add("@IdReferee2", OdbcType.Int).Value = report.Referee2.Id;
            cmd.Parameters.Add("@IdSupervisor", OdbcType.Int).Value = report.Supervisor.Id;
            //Corps
            cmd.Parameters.Add("@A1Rate", OdbcType.TinyInt).Value = report.A1Rate;
            cmd.Parameters.Add("@A1Comment", OdbcType.NVarChar).Value = report.A1Comment;
            cmd.Parameters.Add("@A2Rate", OdbcType.TinyInt).Value = report.A2Rate;
            cmd.Parameters.Add("@A2Comment", OdbcType.NVarChar).Value = report.A2Comment;
            cmd.Parameters.Add("@A3Rate", OdbcType.TinyInt).Value = report.A3Rate;
            cmd.Parameters.Add("@A3Comment", OdbcType.NVarChar).Value = report.A3Comment;
            cmd.Parameters.Add("@A4Rate", OdbcType.TinyInt).Value = report.A4Rate;
            cmd.Parameters.Add("@A4Comment", OdbcType.NVarChar).Value = report.A4Comment;
            cmd.Parameters.Add("@B1Rate", OdbcType.TinyInt).Value = report.B1Rate;
            cmd.Parameters.Add("@B1Comment", OdbcType.NVarChar).Value = report.B1Comment;
            cmd.Parameters.Add("@B2Rate", OdbcType.TinyInt).Value = report.B2Rate;
            cmd.Parameters.Add("@B2Comment", OdbcType.NVarChar).Value = report.B2Comment;
            cmd.Parameters.Add("@B3Rate", OdbcType.TinyInt).Value = report.B3Rate;
            cmd.Parameters.Add("@B3Comment", OdbcType.NVarChar).Value = report.B3Comment;
            cmd.Parameters.Add("@C1Rate", OdbcType.TinyInt).Value = report.C1Rate;
            cmd.Parameters.Add("@C1Comment", OdbcType.NVarChar).Value = report.C1Comment;
            cmd.Parameters.Add("@C2Rate", OdbcType.TinyInt).Value = report.C2Rate;
            cmd.Parameters.Add("@C2Comment", OdbcType.NVarChar).Value = report.C2Comment;
            cmd.Parameters.Add("@C3Rate", OdbcType.TinyInt).Value = report.C3Rate;
            cmd.Parameters.Add("@C3Comment", OdbcType.NVarChar).Value = report.C3Comment;
            cmd.Parameters.Add("@C4Rate", OdbcType.TinyInt).Value = report.C4Rate;
            cmd.Parameters.Add("@C4Comment", OdbcType.NVarChar).Value = report.C4Comment;
            cmd.Parameters.Add("@C5Rate", OdbcType.TinyInt).Value = report.C5Rate;
            cmd.Parameters.Add("@C5Comment", OdbcType.NVarChar).Value = report.C5Comment;
            //Pied
            cmd.Parameters.Add("@TotalRateA", OdbcType.TinyInt).Value = report.TotalRateA;
            cmd.Parameters.Add("@TotalRateB", OdbcType.TinyInt).Value = report.TotalRateB;
            cmd.Parameters.Add("@TotalRateC", OdbcType.TinyInt).Value = report.TotalRateC;
            cmd.Parameters.Add("@TotalRate", OdbcType.TinyInt).Value = report.TotalRate;
            cmd.Parameters.Add("@Comment", OdbcType.NVarChar).Value = report.Comment;

            Args args = Security.ExecuteOdbcCommand(cmd, Enums.OdbcCommandType.ExecuteNonQuery);

            if (args.Result)
            {
                //AJouter un Match avec
                Game game = new Game
                {
                    DateBegin = report.DateBegin,
                    Championship = report.Championship,
                    ClubHome = report.ClubHome,
                    NbGoalsClubHome = report.NbGoalsClubHome,
                    ClubVisitor = report.ClubVisitor,
                    NbGoalsClubVisitor = report.NbGoalsClubVisitor,
                    Referee1 = report.Referee1,
                    Referee2 = report.Referee2 ?? new Referee(),
                    Report = GetReportByDateTime(report.DateBegin) ?? new Report() //Avoir le rapport fraichement rentré
                };
                if(Games.InsertGame(game) > 0) {
                    return args.RowsAffected;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                throw new Exception(args.Message);
            }
        }

        public static Report GetReportByDateTime(DateTime dateTime)
        {
            //On créé une liste vide
            Report Item = new Report();
            //On créé une commande basé sur notre requpête puis sur la connexion
            OdbcCommand cmd = new OdbcCommand($"SELECT * FROM tbReport WHERE DateBegin = ?", Security.OdbcConnection);
            cmd.Parameters.Add("@DateBegin", OdbcType.DateTime).Value = dateTime;

            Args args = Security.ExecuteOdbcCommand(cmd, Enums.OdbcCommandType.Fill);

            if (args.Result)
            {
                //On traite notre table de données
                //On passe tous les enregistrements
                foreach (DataRow item in args.DataTable.Rows) { Item = GetReport(item); }
            }
            else
            {
                throw new Exception(args.Message);
            }

            //On renvoie notre liste
            return Item;
        }

        public static Report GetReport(DataRow dataRow)
        {
            Report report = new Report();
            report.Championship = new Championship();
            report.ClubHome = new Club();
            report.ClubVisitor = new Club();
            report.Referee1 = new Referee();
            report.Referee2 = new Referee();
            report.Supervisor = new Supervisor();

            report.Id = dataRow.IsNull("Id") ? 0 : (int)dataRow["Id"];
            report.DateBegin = dataRow.IsNull("DateBegin") ? DateTime.Now : (DateTime)dataRow["DateBegin"];
            report.Championship.Id = dataRow.IsNull("IdChampionship") ? 0 : (int)dataRow["IdChampionship"];
            report.Championship = Championships.GetChampionshipById(report.Championship.Id);
            report.ClubHome.Id = dataRow.IsNull("IdClubHome") ? 0 : (int)dataRow["IdClubHome"];
            report.ClubHome = Clubs.GetClubById(report.ClubHome.Id);
            report.NbGoalsClubHome = dataRow.IsNull("NbGoalsClubHome") ? 0 : (int)dataRow["NbGoalsClubHome"];
            report.ClubVisitor.Id = dataRow.IsNull("IdClubVisitor") ? 0 : (int)dataRow["IdClubVisitor"];
            report.ClubVisitor = Clubs.GetClubById(report.ClubVisitor.Id);
            report.NbGoalsClubVisitor = dataRow.IsNull("NbGoalsClubVisitor") ? 0 : (int)dataRow["NbGoalsClubVisitor"];
            report.Referee1.Id = dataRow.IsNull("IdReferee1") ? 0 : (int)dataRow["IdReferee1"];
            report.Referee1 = Referees.GetRefereeById(report.Referee1.Id);
            report.Referee2.Id = dataRow.IsNull("IdReferee2") ? 0 : (int)dataRow["IdReferee2"];
            report.Referee2 = Referees.GetRefereeById(report.Referee2.Id);
            report.Supervisor.Id = dataRow.IsNull("IdSupervisor") ? 0 : (int)dataRow["IdSupervisor"];
            report.Supervisor = Supervisors.GetSupervisorById(report.Supervisor.Id);

            //Corps
            report.A1Rate = dataRow.IsNull("A1Rate") ? 0 : (RateReport)(int)(byte)dataRow["A1Rate"];
            report.A1Comment = dataRow.IsNull("A1Comment") ? String.Empty : (string)dataRow["A1Comment"];
            report.A2Rate = dataRow.IsNull("A2Rate") ? 0 : (RateReport)(int)(byte)dataRow["A2Rate"];
            report.A2Comment = dataRow.IsNull("A2Comment") ? String.Empty : (string)dataRow["A2Comment"];
            report.A3Rate = dataRow.IsNull("A3Rate") ? 0 : (RateReport)(int)(byte)dataRow["A3Rate"];
            report.A3Comment = dataRow.IsNull("A3Comment") ? String.Empty : (string)dataRow["A3Comment"];
            report.A4Rate = dataRow.IsNull("A4Rate") ? 0 : (RateReport)(int)(byte)dataRow["A4Rate"];
            report.A4Comment = dataRow.IsNull("A4Comment") ? String.Empty : (string)dataRow["A4Comment"];
            report.B1Rate = dataRow.IsNull("B1Rate") ? 0 : (RateReport)(int)(byte)dataRow["B1Rate"];
            report.B1Comment = dataRow.IsNull("B1Comment") ? String.Empty : (string)dataRow["B1Comment"];
            report.B2Rate = dataRow.IsNull("B2Rate") ? 0 : (RateReport)(int)(byte)dataRow["B2Rate"];
            report.B2Comment = dataRow.IsNull("B2Comment") ? String.Empty : (string)dataRow["B2Comment"];
            report.B3Rate = dataRow.IsNull("B3Rate") ? 0 : (RateReport)(int)(byte)dataRow["B3Rate"];
            report.B3Comment = dataRow.IsNull("B3Comment") ? String.Empty : (string)dataRow["B3Comment"];
            report.C1Rate = dataRow.IsNull("C1Rate") ? 0 : (RateReport)(int)(byte)dataRow["C1Rate"];
            report.C1Comment = dataRow.IsNull("C1Comment") ? String.Empty : (string)dataRow["C1Comment"];
            report.C2Rate = dataRow.IsNull("C2Rate") ? 0 : (RateReport)(int)(byte)dataRow["C2Rate"];
            report.C2Comment = dataRow.IsNull("C2Comment") ? String.Empty : (string)dataRow["C2Comment"];
            report.C3Rate = dataRow.IsNull("C3Rate") ? 0 : (RateReport)(int)(byte)dataRow["C3Rate"];
            report.C3Comment = dataRow.IsNull("C3Comment") ? String.Empty : (string)dataRow["C3Comment"];
            report.C4Rate = dataRow.IsNull("C4Rate") ? 0 : (RateReport)(int)(byte)dataRow["C4Rate"];
            report.C4Comment = dataRow.IsNull("C4Comment") ? String.Empty : (string)dataRow["C4Comment"];
            report.C5Rate = dataRow.IsNull("C5Rate") ? 0 : (RateReport)(int)(byte)dataRow["C5Rate"];
            report.C5Comment = dataRow.IsNull("C5Comment") ? String.Empty : (string)dataRow["C5Comment"];

            //Pied
            report.TotalRateA = dataRow.IsNull("TotalRateA") ? 0 : (int)(byte)dataRow["TotalRateA"];
            report.TotalRateB = dataRow.IsNull("TotalRateB") ? 0 : (int)(byte)dataRow["TotalRateB"];
            report.TotalRateC = dataRow.IsNull("TotalRateC") ? 0 : (int)(byte)dataRow["TotalRateC"];
            report.TotalRate = dataRow.IsNull("TotalRate") ? 0 : (int)(byte)dataRow["TotalRate"];

            report.Comment = dataRow.IsNull("Comment") ? String.Empty : (string)dataRow["Comment"];

            return report;
        }
    }
}


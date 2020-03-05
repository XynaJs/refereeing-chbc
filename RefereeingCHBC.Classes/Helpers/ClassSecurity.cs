using System;
using System.Collections.Generic;
using System.Text;

namespace RefereeingCHBC.Classes.Helpers
{
    public class Security
    {
        /// <summary>
        /// Utilisateur connecté à l'application
        /// </summary>
        public static Models.User CurrentUser;

        /// <summary>
        /// Connection Odbc Utilisée par la BDD
        /// </summary>
        public static OdbcConnection OdbcConnection => new OdbcConnection(Configuration.ConnectionString);


        /// <summary>
        /// Méthode pour identifier un utilisateur renvoie 'utilisateur si la connection réussie , sinon un utilisateur vide;
        /// </summary>
        /// <param name="email">Email de l'utilisateur</param>
        /// <param name="password">Mot de passe de l'utilisateur</param>
        /// <returns>bool</returns>
        internal static User Login(string email, string password)
        {
            CurrentUser = Models.Users.GetDataByEmailAndPassword(email, password);
            return CurrentUser;
        }

        /// <summary>
        /// Méthode qui execute la commande passé en paramètres 
        /// </summary>
        /// <param name="command">OdbCommand</param>
        /// <returns>Renvoie l'objet Args (Contient le résultat de la requête ou une exception)</returns>
        public static Args ExecuteOdbcCommand(OdbcCommand command, Classes.Enums.OdbcCommandType odbcCommandType)
        {
            Args args = new Args();
            args.Result = true;
            using (OdbcConnection con = command.Connection)
            {
                //On créé un adapter pour remplir une table de données basé sur notre commande
                OdbcDataAdapter adapter = new OdbcDataAdapter(command);
                //On essaye d'ouvrir la connexion à la base
                try
                {
                    con.Open();
                    try
                    {
                        switch (odbcCommandType)
                        {
                            case Classes.Enums.OdbcCommandType.ExecuteNonQuery:
                                args.RowsAffected = command.ExecuteNonQuery();
                                break;
                            case Classes.Enums.OdbcCommandType.Fill:
                                adapter.Fill(args.DataTable);
                                break;
                            default:
                                throw new Exception();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Classes.Security.GetOdbcDataTable().Fill()", ex);
                        args.Result = false;
                        throw new Exception();
                    }
                    con.Close();
                }
                catch (OdbcException)
                {
                    args.Result = false;
                    args.Message = "Erreur de connexion à la base de données.";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Classes.Security.GetOdbcDataTable().Open()", ex);
                }
            }

            return args;
        }

    }




    /// <summary>
    /// Classe Argument: Résultat des commandes ODBC
    /// </summary>
    public class Args
    {

        private DataTable _dataTable { get; set; } = new DataTable();
        /// <summary>
        ///  Données si pas d'erreur
        /// </summary>
        public DataTable DataTable
        {
            get { return _dataTable; }
            set
            {
                RowsAffected = DataTable.Rows.Count;
            }
        }


        public int RowsAffected { get; set; }

        /// <summary>
        /// Résultat, Vrai = succès, False = erreur
        /// </summary>
        public bool Result { get; set; } = false;

        /// <summary>
        /// Message a afficher si une exception s'est produite
        /// </summary>
        public string Message { get; set; } = "Une erreur s'est produite.";
    }
}

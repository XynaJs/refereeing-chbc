using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Text;

namespace RefereeingCHBC.Datas.Helpers
{
    public class Security
    {
        /// <summary>
        /// Connection Odbc Utilisée par la BDD
        /// </summary>
        public static OdbcConnection OdbcConnection => new OdbcConnection(Configuration.ConnectionString);

        /// <summary>
        /// Méthode qui execute la commande passé en paramètres 
        /// </summary>
        /// <param name="command">OdbCommand</param>
        /// <returns>Renvoie l'objet Args (Contient le résultat de la requête ou une exception)</returns>
        public static Args ExecuteOdbcCommand(OdbcCommand command, Enums.OdbcCommandType odbcCommandType)
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
                            case Enums.OdbcCommandType.ExecuteNonQuery:
                                args.RowsAffected = command.ExecuteNonQuery();
                                break;
                            case Enums.OdbcCommandType.Fill:
                                adapter.Fill(args.DataTable);
                                break;
                            default:
                                throw new Exception();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Security.GetOdbcDataTable().Fill()", ex);
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
                    Console.WriteLine("Security.GetOdbcDataTable().Open()", ex);
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

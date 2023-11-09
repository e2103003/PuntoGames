using MongoDB.Driver;
using MySql.Data.MySqlClient;
using Punto.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Punto
{
    class DatabaseUse
    {
        public static MySqlConnection MySqlConnection;

        private IMongoDatabase _database;

        public string databaseTechno;


        List<Player> players;
        List<Cell> cells;
        List<Game> games;


        public DatabaseUse(string databaseTechno)
        {
            this.databaseTechno = databaseTechno;
            if (databaseTechno == "MySQL")
            {
                try
                {
                    MySQLUse mySQLUse = new MySQLUse();
                    players = mySQLUse.LoadPlayersFromDatabase();
                    cells = mySQLUse.LoadCellsFromDatabase();
                    games = mySQLUse.LoadGamesFromDatabase();
                }
                catch (Exception e){
                    MessageBox.Show($"Erreur connexion à la base de données MySQL : {e}");
                 
                }
                


            }
            else if(databaseTechno == "MongoDB")
            {
                try
                {
                    MongoClient client = new MongoClient("mongodb://localhost:27017");
                    this._database = client.GetDatabase("PuntoDatabase");
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Erreur connexion à la base de données MongoDB : {e}");
                }

            }
            else if(databaseTechno == "SQLite")
            {

            }
            else
            {
                throw new Exception("Techno de BDD introuvable : " + databaseTechno);
                
            }

        }




        

    }
}

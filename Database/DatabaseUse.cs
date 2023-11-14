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
    public class DatabaseUse
    {
        private MySQLUse mySQLUse;
        private MongoDBUse mongoDBUse;

        private IMongoDatabase _database;

        public string databaseTechno;


        private List<Player> players;
        private List<Cell> cells;
        private List<Game> games;


        public DatabaseUse(string databaseTechno)
        {
            this.databaseTechno = databaseTechno;
            if (databaseTechno == "MySQL")
            {
                try
                {
                    mySQLUse = new MySQLUse();
                    LoadDatas();
                }
                catch (Exception e){
                    MessageBox.Show($"Erreur connexion à la base de données MySQL : {e}");
                 
                }
            }
            else if(databaseTechno == "MongoDB")
            {
                try
                {
                    mongoDBUse = new MongoDBUse();
                    players = mongoDBUse.LoadPlayersFromDatabase();

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

        public void LoadDatas()
        {
            players = mySQLUse.LoadPlayersFromDatabase();
            //cells = mySQLUse.LoadCellsFromDatabase();
            games = mySQLUse.LoadGamesFromDatabase();
        }


        public List<Player> GetPlayers() { return players; }
        public List<Cell> GetCells() { return cells; }
        public List<Game> GetGames() { return games; }


        public void DeletePlayer(Player player)
        {
            players.Remove(player);
            if(databaseTechno == "MySQL" && mySQLUse != null)
            {
                this.mySQLUse.DeletePlayerFromDatabase(player.Id);
            }else if(databaseTechno == "MongoDB" && mongoDBUse != null)
            {

            }

        }

        public void ModifyPlayer(Player player)
        {
            if (databaseTechno == "MySQL" && mySQLUse != null)
            {
                this.mySQLUse.UpdatePlayerInDatabase(player);
            }
            else if (databaseTechno == "MongoDB" && mongoDBUse != null)
            {

            }
        }


        public void AddPlayer(Player player)
        {
            if (databaseTechno == "MySQL" && mySQLUse != null)
            {
                this.mySQLUse.AddPlayerToDatabase(player);
            }
            else if (databaseTechno == "MongoDB" && mongoDBUse != null)
            {

            }

        }




    }
}

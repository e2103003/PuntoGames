﻿using MongoDB.Driver;
using MySql.Data.MySqlClient;
using Punto.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Punto
{
    public class DatabaseUse
    {
        private MySQLUse mySQLUse;
        private IMongoDatabase _database;


        private MongoDBUse mongoDBUse;
        private SQLiteUse sqliteUse;

       

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
                    LoadDatas();
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Erreur connexion à la base de données MongoDB : {e}");
                }

            }
            else if(databaseTechno == "SQLite")
            {
                sqliteUse = new SQLiteUse();
                LoadDatas();
            }
            else
            {
                throw new Exception("Techno de BDD introuvable : " + databaseTechno);
                
            }

        }

        public void LoadDatas()
        {
            if (databaseTechno == "MySQL" && mySQLUse != null)
            {
                players = mySQLUse.LoadPlayersFromDatabase();
                //cells = mySQLUse.LoadCellsFromDatabase();
                games = mySQLUse.LoadGamesFromDatabase();
            }
            else if (databaseTechno == "MongoDB" && mongoDBUse != null)
            {
                players = mongoDBUse.LoadPlayersFromDatabase();
                //cells = mongoDBUse.LoadCellsFromDatabase();
                games = mongoDBUse.LoadGamesFromDatabase();
            }
            else if (databaseTechno == "SQLite" && sqliteUse != null)
            {
                
                players = sqliteUse.LoadPlayersFromDatabase();
                ////cells = sqliteUse.LoadCellsFromDatabase();
                //games = sqliteUse.LoadGamesFromDatabase();
            }
            else
            {
                MessageBox.Show("Erreur technologie de base de données introuvable ou null : " + databaseTechno);
            }

        }


        public List<Player> GetPlayers() { return players; }
        public List<Cell> GetCells() { return cells; }
        public List<Game> GetGames() { return games; }


        public void DeletePlayer(Player player)
        {
            players.Remove(player);
            if(databaseTechno == "MySQL" && mySQLUse != null)
            {
                mySQLUse.DeletePlayerFromDatabase(player.Id);
            }
            else if(databaseTechno == "MongoDB" && mongoDBUse != null)
            {
                mongoDBUse.DeletePlayerFromDatabase(player.Id);
            } 
            else if(databaseTechno == "SQLite" && sqliteUse != null)
            {
                sqliteUse.DeletePlayerFromDatabase(player.Id);
            }
            else
            {
                MessageBox.Show("Erreur technologie de base de données introuvable ou null : " + databaseTechno);
            }

        }

        public void ModifyPlayer(Player player)
        {
            if (databaseTechno == "MySQL" && mySQLUse != null)
            {
                mySQLUse.UpdatePlayerInDatabase(player);
            }
            else if (databaseTechno == "MongoDB" && mongoDBUse != null)
            {
                mongoDBUse.UpdatePlayerInDatabase(player);
            }
            else if (databaseTechno == "SQLite" && sqliteUse != null)
            {
                sqliteUse.UpdatePlayerInDatabase(player);
            }
            else
            {
                MessageBox.Show("Erreur technologie de base de données introuvable ou null : " + databaseTechno);
            }
        }


        public void AddPlayer(Player player)
        {
            if (databaseTechno == "MySQL" && mySQLUse != null)
            {
                mySQLUse.AddPlayerToDatabase(player);
            }
            else if (databaseTechno == "MongoDB" && mongoDBUse != null)
            {
                mongoDBUse.AddPlayerToDatabase(player);
            }
            else if (databaseTechno == "SQLite" && sqliteUse != null)
            {
                sqliteUse.AddPlayerToDatabase(player);
            }
            else
            {
                MessageBox.Show("Erreur technologie de base de données introuvable ou null : " + databaseTechno);
            }

        }




    }
}

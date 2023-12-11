using MongoDB.Driver;
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
        private MongoDBUse mongoDBUse;
        private SQLiteUse sqliteUse;
        private Neo4jUse neo4jUse;

       

        public string databaseTechno;


        private List<Player> players;


        public DatabaseUse(string databaseTechno)
        {
            this.databaseTechno = databaseTechno;
            if (databaseTechno == "MySQL")
            {
                try
                {
                    mySQLUse = new MySQLUse();
                    LoadDatasAsync();
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
                    LoadDatasAsync();
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Erreur connexion à la base de données MongoDB : {e}");
                }

            }
            else if(databaseTechno == "SQLite")
            {
                sqliteUse = new SQLiteUse();
                LoadDatasAsync();
            }
            else if(databaseTechno == "Neo4j")
            {
                neo4jUse = new Neo4jUse();
            }
            else
            {
                throw new Exception("Techno de BDD introuvable : " + databaseTechno);
                
            }

        }

        public async Task LoadDatasAsync()
        {
            if (databaseTechno == "MySQL" && mySQLUse != null)
            {
                players = mySQLUse.LoadPlayersFromDatabaseAsync();
            }
            else if (databaseTechno == "MongoDB" && mongoDBUse != null)
            {
                players = mongoDBUse.LoadPlayersFromDatabaseAsync();
            }
            else if (databaseTechno == "SQLite" && sqliteUse != null)
            {
                players = sqliteUse.LoadPlayersFromDatabaseAsync();
            }
            else if (databaseTechno == "Neo4j" && neo4jUse != null)
            {
                
                players = await neo4jUse.LoadPlayersFromDatabaseAsync();
            }
            else
            {
                MessageBox.Show("Erreur technologie de base de données introuvable ou null : " + databaseTechno);
            }

        }


        public List<Player> GetPlayers() { return players; }


        public async Task DeletePlayerAsync(Player player)
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
            else if (databaseTechno == "Neo4j" && neo4jUse != null)
            {
                await neo4jUse.DeletePlayerFromDatabaseAsync(player.Id);
            }
            else
            {
                MessageBox.Show("Erreur technologie de base de données introuvable ou null : " + databaseTechno);
            }

        }

        public async Task ModifyPlayerAsync(Player player)
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
            else if (databaseTechno == "Neo4j" && neo4jUse != null)
            {
                await neo4jUse.UpdatePlayerInDatabaseAsync(player);
            }
            else
            {
                MessageBox.Show("Erreur technologie de base de données introuvable ou null : " + databaseTechno);
            }
        }


        public async Task AddPlayerAsync(Player player)
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
            else if (databaseTechno == "Neo4j" && neo4jUse != null)
            {
                await neo4jUse.AddPlayerToDatabaseAsync(player);
            }


            else
            {
                MessageBox.Show("Erreur technologie de base de données introuvable ou null : " + databaseTechno);
            }

        }

        public void AddVictory(Player winner)
        {
            if(winner.Wins == null)
            {
                winner.Wins = 1;
            } else
            {
                winner.Wins++;
            }
            winner.LastWin = DateTime.Now.ToString();

            if (databaseTechno == "MySQL" && mySQLUse != null)
            {                
                mySQLUse.UpdatePlayerInDatabase(winner);
            }
            else if (databaseTechno == "MongoDB" && mongoDBUse != null)
            {
                mongoDBUse.UpdatePlayerInDatabase(winner);
            }
            else if (databaseTechno == "SQLite" && sqliteUse != null)
            {
                sqliteUse.UpdatePlayerInDatabase(winner);
            }
            else if (databaseTechno == "Neo4j" && neo4jUse != null)
            {
                neo4jUse.UpdatePlayerInDatabaseAsync(winner);
            }

            else
            {
                MessageBox.Show("Erreur technologie de base de données introuvable ou null : " + databaseTechno);
            }
            
        }


        // méthode qui permet de générer des données aléatoires dans la base de données en fonction du nombre de joueurs
        public void GenerateDatas(double value)
        {
            // liste des couleurs
            List<string> colors = new List<string>() { "Blue", "Orange", "Green", "Purple", "Yellow", "Red" };
            // génération de la liste de joueurs       
            List<Player> players = new List<Player>(); 
            for (int i = 0; i < value; i++)            
            {                                          
                Player player = new Player();          
                player.Name = "Joueur " + i;
                // on choisit une couleur au hasard dans la liste des couleurs
                player.Color = colors[new Random().Next(colors.Count)];
                player.Wins = new Random().Next(0, 100);
                // on génére u date aléatoire entre le 1er janvier 2020 et aujourd'hui
                DateTime start = new DateTime(2020, 1, 1);
                int range = (DateTime.Today - start).Days;
                player.LastWin = start.AddDays(new Random().Next(range)).ToString();
                players.Add(player);
            }
            // on ajoute les joueurs à la base de données
            foreach (Player player in players)
            {
                AddPlayerAsync(player);
            }


        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Punto.Database
{
    internal class MySQLUse
    {
        public static MySqlConnection MySqlConnection;

        public MySQLUse() {
            try
            {
                string connectionString = "Server=127.0.0.1;Database=PuntoDatabase;Uid=username;Pwd=password;";
                MySqlConnection = new MySqlConnection(connectionString);
                MySqlConnection.Open();

            }
            catch (Exception e)
            {
                MessageBox.Show($"Erreur connexion à la base de données MySQL : {e}");

            }
        }

        List<Player> players;
        List<Cell> cells;
        List<Game> games;


        private void LoadData()
        {
            players = LoadPlayersFromDatabase();
            cells = LoadCellsFromDatabase();
            games = LoadGamesFromDatabase();
            
        }


        public List<Player> LoadPlayersFromDatabase()
        {
            List<Player> players = new List<Player>();

            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Player", MySqlConnection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Player player = new Player
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("Name"),
                            Color = reader.GetString("Color"),
                            Wins = reader.GetInt32("Wins")
                        };
                        players.Add(player);
                    }
                }
            }

            return players;
        }

        public List<Cell> LoadCellsFromDatabase()
        {
            List<Cell> cases = new List<Cell>();

            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Cell", MySqlConnection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cell singleCase = new Cell
                        {
                            Id = reader.GetInt32("Id"),
                            IsEmpty = reader.GetBoolean("IsEmpty"),
                            PlayerId = reader.IsDBNull(reader.GetOrdinal("PlayerId")) ? (int?)null : reader.GetInt32("PlayerId")
                        };
                        cases.Add(singleCase);
                    }
                }
            }

            return cases;
        }

        public List<Game> LoadGamesFromDatabase()
        {
            List<Game> games = new List<Game>();

            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Game", MySqlConnection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Game game = new Game
                        {
                            Id = reader.GetInt32("id"),
                            Player1Id = reader.GetInt32("player1_id"),
                            Player2Id = reader.GetInt32("player2_id"),
                            Date = reader.GetDateTime("date"),
                            Cells = reader.GetString("cells")
                        };
                        games.Add(game);
                    }
                }
            }

            return games;
        }
        
    }
}

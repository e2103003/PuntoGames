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


        private void LoadData()
        {
            players = LoadPlayersFromDatabase();
            
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
                        // On vérifie si la base n'est pas vide pour éviter les erreurs
                        if (reader.HasRows == false)
                        {
                            return players;
                        } 

                        Player player = new Player
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("Name"),
                            Color = reader.GetString("Color"),
                            Wins = reader.GetInt32("Wins"),
                            LastWin = reader.GetString("LastWin")
                        };
                        players.Add(player);
                    }
                }
            }

            return players;
        }

        public void AddPlayerToDatabase(Player newPlayer)
        {
            string query = "INSERT INTO Player (Name, Color, Wins, LastWin) VALUES (@Name, @Color, @Wins, @LastWin)";

            using (MySqlCommand cmd = new MySqlCommand(query, MySqlConnection))
            {
                cmd.Parameters.AddWithValue("@Name", newPlayer.Name);
                cmd.Parameters.AddWithValue("@Color", newPlayer.Color);
                cmd.Parameters.AddWithValue("@Wins", newPlayer.Wins);
                cmd.Parameters.AddWithValue("@LastWin", newPlayer.LastWin);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePlayerInDatabase(Player updatedPlayer)
        {
            string query = "UPDATE Player SET Name = @Name, Color = @Color, Wins = @Wins, LastWin = @LastWin WHERE Id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(query, MySqlConnection))
            {
                cmd.Parameters.AddWithValue("@Id", updatedPlayer.Id);
                cmd.Parameters.AddWithValue("@Name", updatedPlayer.Name);
                cmd.Parameters.AddWithValue("@Color", updatedPlayer.Color);
                cmd.Parameters.AddWithValue("@Wins", updatedPlayer.Wins);
                cmd.Parameters.AddWithValue("@LastWin", updatedPlayer.LastWin);

                cmd.ExecuteNonQuery();
            }
        }


        public void DeletePlayerFromDatabase(int playerId)
        {
            string query = "DELETE FROM Player WHERE Id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(query, MySqlConnection))
            {
                cmd.Parameters.AddWithValue("@Id", playerId);

                cmd.ExecuteNonQuery();
            }
        }

        public void AddVictoryToDatabase(Player winner)
        {
            string query = "UPDATE Player SET Wins = @Wins, LastWin = @LastWin WHERE Id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(query, MySqlConnection))
            {
                cmd.Parameters.AddWithValue("@Id", winner.Id);
                cmd.Parameters.AddWithValue("@Wins", winner.Wins);
                cmd.Parameters.AddWithValue("@LastWin", winner.LastWin);

                cmd.ExecuteNonQuery();
            }
            
        }
    }
}

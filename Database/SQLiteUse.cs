using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Punto.Database
{
    internal class SQLiteUse
    {

        private SQLiteConnection database;

        public SQLiteUse()
        {
            try
            {
                string databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PuntoDatabase.db");

                string connectionString = $"Data Source={databasePath};Version=3;";
                // MessageBox.Show(connectionString);
                database = new SQLiteConnection(connectionString);
                database.Open();
                InitializeDatabase();
                var test = 1;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Erreur connexion à la base de données SQLite : {e}");
            }


        }


        private void InitializeDatabase()
        {
            // Vérifier si les tables existent et les créer si nécessaire
            if (!TableExists("Player"))
            {
                CreatePlayerTable();
            }

        }

        private bool TableExists(string tableName)
        {
            using (SQLiteCommand cmd = new SQLiteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}'", database))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        private void CreatePlayerTable()
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "CREATE TABLE Player (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "name TEXT," +
                "color TEXT," +
                "wins INTEGER," +
                "lastWin TEXT" +
                ")", database))
            {
                cmd.ExecuteNonQuery();
            }
        }




        List<Player> players;
        List<Cell> cells;
        List<Game> games;

        public List<Player> LoadPlayersFromDatabase()
        {
            List<Player> players = new List<Player>();

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Player", database))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Player player = new Player
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["name"]),
                            Color = Convert.ToString(reader["color"]),
                            Wins = Convert.ToInt32(reader["wins"]),
                            LastWin = Convert.ToString(reader["lastWin"])
                        };
                        players.Add(player);
                    }
                }
            }

            return players;
        }

        public void AddPlayerToDatabase(Player newPlayer)
        {
            string query = "INSERT INTO Player (name, color, wins, lastWin) VALUES (@name, @color, @wins, @lastWin)";

            using (SQLiteCommand cmd = new SQLiteCommand(query, database))
            {
                cmd.Parameters.AddWithValue("@name", newPlayer.Name);
                cmd.Parameters.AddWithValue("@color", newPlayer.Color);
                cmd.Parameters.AddWithValue("@wins", newPlayer.Wins);
                cmd.Parameters.AddWithValue("@lastWin", newPlayer.LastWin);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePlayerInDatabase(Player updatedPlayer)
        {
            string query = "UPDATE Player SET name = @name, color = @color, wins = @wins, lastWin = @lastWin WHERE id = @id";

            using (SQLiteCommand cmd = new SQLiteCommand(query, database))
            {
                cmd.Parameters.AddWithValue("@id", updatedPlayer.Id);
                cmd.Parameters.AddWithValue("@name", updatedPlayer.Name);
                cmd.Parameters.AddWithValue("@color", updatedPlayer.Color);
                cmd.Parameters.AddWithValue("@wins", updatedPlayer.Wins);
                cmd.Parameters.AddWithValue("@lastWin", updatedPlayer.LastWin);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePlayerFromDatabase(int playerId)
        {
            string query = "DELETE FROM Player WHERE id = @id";

            using (SQLiteCommand cmd = new SQLiteCommand(query, database))
            {
                cmd.Parameters.AddWithValue("@id", playerId);

                cmd.ExecuteNonQuery();
            }
        }


    }
}

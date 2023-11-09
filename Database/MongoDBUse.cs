using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Punto.Database
{
    internal class MongoDBUse
    {

        private IMongoDatabase _database;

        List<Player> players;
        List<Cell> cells;
        List<Game> games;


        public MongoDBUse()
        {
            try
            {
                MongoClient client = new MongoClient();
                _database = client.GetDatabase("PuntoDatabase");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Erreur connexion à la base de données MongoDB : {e}");
            }
        }

        private void LoadData()
        {
            players = LoadPlayersFromDatabase();
            cells = LoadCellsFromDatabase();
            games = LoadGamesFromDatabase();
        }

        private List<Player> LoadPlayersFromDatabase()
        {
            var collection = _database.GetCollection<Player>("Player");
            var filter = Builders<Player>.Filter.Empty;
            return collection.Find(filter).ToList();
        }

        private List<Cell> LoadCellsFromDatabase()
        {
            var collection = _database.GetCollection<Cell>("Cells");
            var filter = Builders<Cell>.Filter.Empty;
            return collection.Find(filter).ToList();
        }

        private List<Game> LoadGamesFromDatabase()
        {
            var collection = _database.GetCollection<Game>("Game");
            var filter = Builders<Game>.Filter.Empty;
            return collection.Find(filter).ToList();
        }


        public List<Player> GetPlayers() { return players; }
        public void SetPlayers(List<Player> players) { this.players = players; }


        public List<Cell> GetCells() { return cells; }        
        public void SetCells(List<Cell> cells) { this.cells = cells;  }

        public List<Game> GetGames() { return games; }

        public void SetGames(List<Game> games) { this.games = games;  }

    }
}

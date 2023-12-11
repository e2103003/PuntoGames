using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Punto.Database
{
    internal class MongoDBUse : ITechnoUse
    {

        private IMongoDatabase _database;

        private IMongoCollection<Player> _playersCollection;

        private IMongoCollection<BsonDocument> _countersCollection;



        public MongoDBUse()
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://localhost:27017");
                _database = client.GetDatabase("PuntoDatabase");
                LoadData();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Erreur connexion à la base de données MongoDB : {e}");
            }
        }

        private void LoadData()
        {
            _playersCollection = _database.GetCollection<Player>("Player");
            _countersCollection = _database.GetCollection<BsonDocument>("Counters");
        }

        public List<Player> LoadPlayersFromDatabaseAsync()
        {
            var collection = _database.GetCollection<Player>("Player");
            var filter = Builders<Player>.Filter.Empty;
            return collection.Find(filter).ToList();
        }




        // Ajouter un joueur dans la collection "Player"
        public void AddPlayerToDatabase(Player newPlayer)
        {
            newPlayer.Id = GetNextPlayerId();
            _playersCollection.InsertOne(newPlayer);
        }


        private int GetNextPlayerId()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "playerId");
            var update = Builders<BsonDocument>.Update.Inc("seq", 1);

            var options = new FindOneAndUpdateOptions<BsonDocument>
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = true
            };

            var result = _countersCollection.FindOneAndUpdate(filter, update, options);

            return result["seq"].AsInt32;
        }



        // Mettre à jour les informations d'un joueur dans la collection "Player"
        public void UpdatePlayerInDatabase(Player updatedPlayer)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, updatedPlayer.Id);
            var update = Builders<Player>.Update
                .Set(p => p.Name, updatedPlayer.Name)
                .Set(p => p.Color, updatedPlayer.Color)
                .Set(p => p.Wins, updatedPlayer.Wins)
                .Set(p => p.LastWin, updatedPlayer.LastWin);

            _playersCollection.UpdateOne(filter, update);
        }

        // Supprimer un joueur de la collection "Player"
        public void DeletePlayerFromDatabase(int playerId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            _playersCollection.DeleteOne(filter);
        }

        public void AddVictoryToDatabase(Player winner)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, winner.Id);
            var update = Builders<Player>.Update
                .Set(p => p.Wins, winner.Wins)
                .Set(p => p.LastWin, winner.LastWin);

            _playersCollection.UpdateOne(filter, update);
        }
    }
}

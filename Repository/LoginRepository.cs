using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using thesocialappapiv3.Models.PostModel;
using thesocialappapiv3.Models;

namespace thesocialappapiv3.Repository
{
    public class LoginRepository
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        protected IMongoCollection<LoginViewModel> _collection;

        public LoginRepository()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("demoDB");
            _collection = _database.GetCollection<LoginViewModel>("login");
        }

        public LoginViewModel InsertUser(LoginViewModel user)
        {
            this._collection.InsertOneAsync(user);
            var newUser = this.Get(user.Username);
            return newUser;

        }

        public bool DoesUserExist(string username)
        {
            var filter = Builders<LoginViewModel>.Filter.Eq(x => x.Username, username);
            var results = this._collection.Find(filter).ToList();
            if (results.Count == 0) return false;
            else return true;
        }

        public LoginViewModel Get(string username)
        {
            var filter = Builders<LoginViewModel>.Filter.Eq(x => x.Username, username);
            var singleUser = this._collection.Find(filter).FirstAsync().Result;
            return singleUser;
        }
    }
}
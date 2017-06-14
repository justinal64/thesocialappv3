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
            var newUser = this.Get(user._id.ToString());
            return newUser;
            // return this.Get(user._id.ToString());
            
        }

        // public List<PostModel> SelectAll()
        // {
        //     //var query =  this._collection.Find(_ => true)?.ToList();
        //     try
        //     {
        //         var query = this._collection.Find(_ => true).ToList();
        //         return query;
        //     }
        //     catch (Exception ex) {
        //         Debug.WriteLine(ex.Message);
        //     }

        //     return new List<PostModel>();
        // }

        // public List<PostModel> Filter(string jsonQuery)
        // {
        //     // var queryDoc = new QueryDocument(BsonSerializer.Deserialize<BsonDocument>(jsonQuery));
        //     BsonDocument queryDoc = MongoDB.Bson.Serialization
        //            .BsonSerializer.Deserialize<BsonDocument>(jsonQuery);

        //     return _collection.Find<PostModel>(queryDoc).ToList();
        // }

        public bool DoesUserExist(string username) 
        {
            var filter = Builders<LoginViewModel>.Filter.Eq(x => x.Username, username);
            var results =  this._collection.Find(filter).ToList();
            if(results.Count == 0) return false;
            else return true;
        }

        public LoginViewModel Get(string username)
        {          
            var filter = Builders<LoginViewModel>.Filter.Eq(x => x.Username, username);  
            var test = this._collection.Find(filter).FirstAsync().Result;
            return test;
        }
        // public PostModel UpdatePost(string id, PostModel postmodel)
        // {
        //     postmodel._id = new ObjectId(id);

        //     var filter = Builders<PostModel>.Filter.Eq(s => s._id, postmodel._id);
        //     this._collection.ReplaceOneAsync(filter, postmodel);
        //     return this.Get(id);
        // }
    }
}
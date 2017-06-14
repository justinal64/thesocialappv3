using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using thesocialappapiv3.Models.PostModel;

namespace thesocialappapiv3.Repository
{
    public class PostsRepository
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        protected IMongoCollection<PostModel> _collection;

        public PostsRepository()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("demoDB");
            _collection = _database.GetCollection<PostModel>("posts");
        }

        public PostModel InsertPost(PostModel contact)
        {
            this._collection.InsertOneAsync(contact);
            return this.Get(contact._id.ToString());
        }

        public List<PostModel> SelectAll()
        {
            //var query =  this._collection.Find(_ => true)?.ToList();
            try
            {
                var query = this._collection.Find(_ => true).ToList();
                return query;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }

            return new List<PostModel>();
        }

        public List<PostModel> Filter(string jsonQuery)
        {
            // var queryDoc = new QueryDocument(BsonSerializer.Deserialize<BsonDocument>(jsonQuery));
            BsonDocument queryDoc = MongoDB.Bson.Serialization
                   .BsonSerializer.Deserialize<BsonDocument>(jsonQuery);

            return _collection.Find<PostModel>(queryDoc).ToList();
        }

        public PostModel Get(string id)
        {
            // return this._collection.Find(new BsonDocument { { "id", new ObjectId(id) } }).FirstAsync().Result;
            return this._collection.Find(new BsonDocument { { "_id", new ObjectId(id) } }).FirstAsync().Result;
        }
        public PostModel UpdatePost(string id, PostModel postmodel)
        {
            postmodel._id = new ObjectId(id);

            var filter = Builders<PostModel>.Filter.Eq(s => s._id, postmodel._id);
            this._collection.ReplaceOneAsync(filter, postmodel);
            return this.Get(id);
        }
    }
}
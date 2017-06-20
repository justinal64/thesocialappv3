using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using thesocialappapiv3.Models.NotesModel;

namespace thesocialappapiv3.Repository
{
    public class NotesRepository
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        protected IMongoCollection<NotesModel> _collection;

        public NotesRepository()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("demoDB");
            _collection = _database.GetCollection<NotesModel>("notes");
        }

        public NotesModel InsertPost(NotesModel note)
        {
            this._collection.InsertOneAsync(note);
            return this.Get(note.dbid);
        }

        public List<NotesModel> SelectAll()
        {
            //var query =  this._collection.Find(_ => true)?.ToList();
            try
            {
                var query = this._collection.Find(_ => true).ToList();
                return query;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return new List<NotesModel>();
        }

        public List<NotesModel> Filter(string jsonQuery)
        {
            // var queryDoc = new QueryDocument(BsonSerializer.Deserialize<BsonDocument>(jsonQuery));
            BsonDocument queryDoc = MongoDB.Bson.Serialization
                   .BsonSerializer.Deserialize<BsonDocument>(jsonQuery);

            return _collection.Find<NotesModel>(queryDoc).ToList();
        }

        public List<NotesModel> GetAll(string username)
        {
            var filter = Builders<NotesModel>.Filter.Eq(x => x.Username, username);
            return this._collection.Find(filter).ToList();
        }

        public NotesModel Get(string dbid)
        {
            // return this._collection.Find(new BsonDocument { { "id", new ObjectId(id) } }).FirstAsync().Result;

            var filter = Builders<NotesModel>.Filter.Eq(x => x.dbid, dbid);
            var singleNote = this._collection.Find(filter).FirstAsync().Result;
            return singleNote;
        }
        public NotesModel UpdatePost(string dbid, NotesModel NotesModel)
        {
            var filter = Builders<NotesModel>.Filter.Eq(s => s.dbid, NotesModel.dbid);
            this._collection.ReplaceOneAsync(filter, NotesModel);
            return this.Get(dbid);
        }

        public List<NotesModel> PostByUsername(string username)
        {
            var filter = Builders<NotesModel>.Filter.Eq(x => x.Username, username);
            return this._collection.Find(filter).ToList();
        }

        public void DeletePost(string dbid)
        {
            _collection.DeleteOne(a => a.dbid == dbid);
        }
    }
}
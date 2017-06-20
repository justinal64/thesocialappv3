using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace thesocialappapiv3.Models.PostModel
{
    public class NotesModel
    {
        public ObjectId _id { get; set; }
        [Required]
        public string dbid { get; set; }
        public string Note { get; set; }
        public string Username { get; set; }
    }

}
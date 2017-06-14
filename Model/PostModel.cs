using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace thesocialappapiv3.Models.PostModel
{
    public class PostModel
    {
        public ObjectId _id { get; set; }
        [Required]
        public string dbid {get; set;}
        public string Company { get; set; }
        public string Posts { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
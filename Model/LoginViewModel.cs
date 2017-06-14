using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace thesocialappapiv3.Models
{
    public class LoginViewModel
    {
        public ObjectId _id { get; set; }
        [Required]
        public string dbid {get; set;}
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

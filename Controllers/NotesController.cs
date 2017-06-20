using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using thesocialappapiv3.Models.PostModel;
using thesocialappapiv3.Repository;
using thesocialappapiv3.Models.NotesModel;

namespace thesocialappapiv3.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        protected NotesRepository _repository;
        public NotesController()
        {
            _repository = new NotesRepository();
        }

        // GET api/notes
        [HttpGet]
        [Route("{username}")]
        public List<NotesModel> GetByUsername(string username)
        {
            var notesByUsername = _repository.PostByUsername(username);
            return notesByUsername;
        }

        //POST api/notes
        [HttpPost]
        public ActionResult Post([FromBody]NotesModel notesModel, string id = "")
        {
            notesModel.dbid = UUID();
            _repository.InsertPost(notesModel);
            return StatusCode(200);

        }

        // Delete api/notes
        [HttpDelete]
        [Route("{dbid}")]
        public void Delete(string dbid)
        {
            _repository.DeletePost(dbid);
        }

        private string UUID()
        {
            return System.Guid.NewGuid().ToString();
        }
    }
}

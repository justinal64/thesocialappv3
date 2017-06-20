using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using thesocialappapiv3.Models.PostModel;
using thesocialappapiv3.Repository;

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
        public List<PostModel> GetByUsername(string username)
        {
            var test = _repository.PostByUsername(username);
            return test;
        }

        // GET api/notes/getall
        [HttpGet]
        [Route("getall")]
        public List<PostModel> GetAll(string jsonQuery = "")
        {
            if (jsonQuery == "")
            {
                List<PostModel> data = _repository.SelectAll();
                return data;
            }
            return _repository.Filter(jsonQuery);
        }

        // POST api/notes
        [HttpPost]
        public ActionResult Post([FromBody]PostModel postModel, string id = "")
        {
            postModel.dbid = UUID();
            if (postModel.dbid == null) return StatusCode(404, "Please enter a dbid");
            if (id == "")
            {
                _repository.InsertPost(postModel);
                return StatusCode(200);
            }
            else
            {
                _repository.UpdatePost(id, postModel);
                return StatusCode(200, "Record Updated");
            }
        }

        // PUT api/notes
        [HttpPut]
        public ActionResult Put([FromBody]PostModel postModel)
        {
            // var updatedPost = _repository.UpdateLikes(postModel);
            _repository.UpdateLikes(postModel);

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

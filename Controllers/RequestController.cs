using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using thesocialappapiv3.Models.PostModel;
using thesocialappapiv3.Repository;

namespace thesocialappapiv3.Controllers
{
    [Route("api/[controller]")]    
    public class RequestController : Controller
    {
        protected PostsRepository _repository;
        public RequestController()
        {
            _repository = new PostsRepository();
        }

        // GET api/request
        [HttpGet]
        public List<PostModel> Get(string jsonQuery = "")
        {
            // if (jsonQuery == "") return  _repository.SelectAll();
            if (jsonQuery == "")     
            {
                List<PostModel> data = _repository.SelectAll();
                return data;   
            }
            return _repository.Filter(jsonQuery);
        }
        
        // POST api/request
        [HttpPost]
        public ActionResult Post([FromBody]PostModel postModel, string id = "")
        {
            if(postModel.dbid == null) return StatusCode(404, "Please enter a dbid");
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
        
        // PUT api/request
        [HttpPut]
        public PostModel Put([FromBody]PostModel postModel, string id = "")
        {
            if (id == "") return _repository.InsertPost(postModel);
            return _repository.UpdatePost(id, postModel);
        }
        
        // Delete api/request
        [HttpDelete]
        public PostModel Delete([FromBody]PostModel postModel, string id = "")
        {
            if (id == "") return _repository.InsertPost(postModel);
            return _repository.UpdatePost(id, postModel);
        }
    }
}

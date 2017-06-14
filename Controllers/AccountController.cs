using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using thesocialappapiv3.Models.PostModel;
using thesocialappapiv3.Repository;
using thesocialappapiv3.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;

namespace thesocialappapiv3.Controllers
{
    [Route("api/[controller]")]    
    public class AccountController : Controller
    {
        protected PostsRepository _repository;
        public AccountController()
        {
            _repository = new PostsRepository();
        }

        // GET api/account
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
        
        // POST api/account
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Post([FromBody]LoginViewModel login, string id = "")
        {
            // if(login.Email == null) return StatusCode(404, "Please enter an email address");
            if (id == "")
            {
                _repository.InsertPost(login);
                return StatusCode(200);
            } 
            else 
            {
                _repository.UpdatePost(id, postModel);
                return StatusCode(200, "Record Updated");
            }
        }
        
        // PUT api/account
        // [HttpPut]
        // public PostModel Put([FromBody]PostModel postModel, string id = "")
        // {
        //     if (id == "") return _repository.InsertPost(postModel);
        //     return _repository.UpdatePost(id, postModel);
        // }
        
        // // Delete api/account
        // [HttpDelete]
        // public PostModel Delete([FromBody]PostModel postModel, string id = "")
        // {
        //     if (id == "") return _repository.InsertPost(postModel);
        //     return _repository.UpdatePost(id, postModel);
        // }
        
        private List<PostModel> GetUser(string email)
        {
            if (email == "")     
            {
                List<PostModel> data = _repository.SelectAll();
                return data;   
            }
            return _repository.Filter(email);
        }
    }
}

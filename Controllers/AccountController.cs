using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using thesocialappapiv3.Models.PostModel;
using thesocialappapiv3.Repository;
using Microsoft.AspNetCore.Authorization;
using thesocialappapiv3.Models;

namespace thesocialappapiv3.Controllers
{
    [Route("api/[controller]")]    
    public class AccountController : Controller
    {
        protected LoginRepository _repository;
        public AccountController()
        {
            _repository = new LoginRepository();
        }

        // GET api/account
        // [HttpGet]
        // public List<PostModel> Get(string jsonQuery = "")
        // {
        //     // if (jsonQuery == "") return  _repository.SelectAll();
        //     if (jsonQuery == "")     
        //     {
        //         List<PostModel> data = _repository.SelectAll();
        //         return data;   
        //     }
        //     return _repository.Filter(jsonQuery);
        // }
        
        // POST api/account
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Post([FromBody]LoginViewModel login, string id = "")
        {
            // check for dbid
            // if (login.dbid == null) return StatusCode(404, "Please enter a dbid");
            // check if user exist first
            if(!_repository.DoesUserExist(login.dbid))
            {
                _repository.InsertUser(login);
                return StatusCode(200, login); 
            }
            else 
            {
                // _repository.UpdatePost(id, login);
                return StatusCode(404, _repository.Get(login.dbid));
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
        
        // private List<PostModel> GetUser(string email)
        // {
        //     if (email == "")     
        //     {
        //         List<PostModel> data = _repository.SelectAll();
        //         return data;   
        //     }
        //     return _repository.Filter(email);
        // }
    }
}

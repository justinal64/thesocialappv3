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

        // POST api/account
        [HttpPost]
        // [AllowAnonymous]
        public ActionResult Post([FromBody]LoginViewModel login, string id = "")
        {
            if (!_repository.DoesUserExist(login.Username))
            {
                login.dbid = UUID();
                _repository.InsertUser(login);
                return StatusCode(200, login);
            }
            else
            {
                return StatusCode(200, _repository.Get(login.Username));
            }
        }

        private string UUID()
        {
            return System.Guid.NewGuid().ToString();
        }
    }
}

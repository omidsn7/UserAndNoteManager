﻿using Microsoft.AspNetCore.Mvc;
using UserAndNoteManager.Interface;
using UserAndNoteManager.Models;

namespace UserAndNoteManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager) 
        {
             _userManager = userManager;
        }

        /// <summary>
        /// Method for Create A New User 
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        [Route("CreateUser")]
        public JsonResult CreateUser([FromBody] User user)
        {
            if (user == null)
                return Common.BadRequest();

            string Result = _userManager.Create(user);

            if (Result == "done")
            {
                return Common.OkResult();
            }
            else
            {
                return Common.BadRequest(Result);
            }
        }

        /// <summary>
        /// Method for Get All Of Users
        /// </summary>
        /// <param name="user"></param>
        [HttpGet]
        [Route("GetAllUsers")]
        public JsonResult GetAllUsers()
        {
            List<User> users = _userManager.GetAllUsers();

            if (users.Count == 0)
                return Common.NotFound();

            return new JsonResult(users);
        }

        /// <summary>
        /// Method for Get One Specified User And The User Notes
        /// </summary>
        /// <param name="ID"></param>
        [HttpGet]
        [Route("GetUsersByID")]
        public JsonResult GetUsersByID([FromQuery] int ID)
        {
            User? user = _userManager.GetUsersByID(ID);

            if (user == null)
                return Common.NotFound();

            return new JsonResult(user);
        }

        /// <summary>
        /// Method for Delete One User
        /// </summary>
        /// <param name="user"></param>
        [HttpDelete]
        [Route("DeleteUser")]
        public JsonResult DeleteUser([FromBody] int ID)
        {
            if (_userManager.GetUsersByID(ID) == null)
                return Common.NotFound();

            _userManager.Delete(ID);

            return Common.NoContent();
        }


        /// <summary>
        /// Method for Update One User
        /// </summary>
        /// <param name="user"></param>
        [HttpPut]
        [Route("UpdateUser")]
        public JsonResult UpdateUser([FromBody] User user)
        {
            if (user == null)
                return Common.BadRequest();

            if (_userManager.GetUsersByID(user.ID) == null)
                return Common.NotFound();

            _userManager.Update(user);
            return Common.OkResult();
        }

    }
}

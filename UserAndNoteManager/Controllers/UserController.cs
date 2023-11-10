using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using UserAndNoteManager.Interface;
using UserAndNoteManager.Models;
using UserAndNoteManager.MyHub;

namespace UserAndNoteManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private IHubContext<HubContext> _hubContext;
        public UserController(IUserManager userManager, IHubContext<HubContext> hubContext) 
        {
             _userManager = userManager;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Method for Create A New User 
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        [Route("CreateUser")]
        public async Task<JsonResult> CreateUser([FromBody] User user)
        {
            if (user == null)
                return Common.BadRequest();

            string Result = _userManager.Create(user);

            if (Result == "done")
            {
                await _hubContext.Clients.All.SendAsync("ChangesOnUserAndNotes", $"{user.FirstName} {user.LastName} Added");
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
        public async Task<JsonResult> GetAllUsers()
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
        public async Task<JsonResult> GetUsersByID([FromQuery] int ID)
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
        public async Task<JsonResult> DeleteUser([FromBody] int ID)
        {
            User? user = _userManager.GetUsersByID(ID);
            if (user == null)
                return Common.NotFound();

            _userManager.Delete(ID);

            await _hubContext.Clients.All.SendAsync("ChangesOnUserAndNotes", $"{user.FirstName} {user.LastName} Deleted And All Of This User Notes Deleted");
            return Common.NoContent();
        }


        /// <summary>
        /// Method for Update One User
        /// </summary>
        /// <param name="user"></param>
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<JsonResult> UpdateUser([FromBody] User user)
        {
            if (user == null)
                return Common.BadRequest();


            User? UpdatedUser = _userManager.GetUsersByID(user.ID);
            if (UpdatedUser == null)
                return Common.NotFound();

            await _hubContext.Clients.All.SendAsync("ChangesOnUserAndNotes", $"{UpdatedUser.FirstName} {UpdatedUser.LastName} Updated");
            
            _userManager.Update(user);
            return Common.OkResult();
        }

    }
}

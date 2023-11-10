using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using UserAndNoteManager.Interface;
using UserAndNoteManager.Models;
using UserAndNoteManager.MyHub;

namespace UserAndNoteManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteManager _noteManager;
        private readonly IUserManager _userManager;
        private IHubContext<HubContext> _hubContext;

        public NoteController(INoteManager noteManager, IHubContext<HubContext> hubContext, IUserManager userManager)
        {
            _noteManager = noteManager;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        /// <summary>
        /// Method for Create A New Note 
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        [Route("CreateNote")]
        public async Task<JsonResult> CreateNote([FromBody] Note note)
        {
            if (note == null)
                return Common.BadRequest();

            _noteManager.Create(note);
            User? user = _userManager.GetUsersByID(note.UserID);
            await _hubContext.Clients.All.SendAsync("ChangesOnUserAndNotes", $"{user.FirstName} {user.LastName} Added A New Note");
            return Common.OkResult();
        }

        /// <summary>
        /// Method for Get All Notes Of a User
        /// </summary>
        /// <param name="user">User ID</param>
        [HttpGet]
        [Route("GetNotesOfUser")]
        public async Task<JsonResult> GetNotesOfUser([FromQuery] int ID)
        {
            List<Note> notes = _noteManager.GetNotesOfUser(ID);

            if (notes.Count == 0)
                return Common.NotFound();

            return new JsonResult(notes);
        }

        /// <summary>
        /// Method for Get One Specified Note
        /// </summary>
        /// <param name="ID">Note ID</param>
        [HttpGet]
        [Route("GetNote")]
        public async Task<JsonResult> GetNote([FromQuery] int ID)
        {
            Note? note = _noteManager.GetNote(ID);

            if (note == null)
                return Common.NotFound();

            return new JsonResult(note);
        }

        /// <summary>
        /// Method for Delete One Note
        /// </summary>
        /// <param name="user"></param>
        [HttpDelete]
        [Route("DeleteNote")]
        public async Task<JsonResult> DeleteNote([FromBody] int ID)
        {
            Note? note = _noteManager.GetNote(ID);
            if (note == null)
                return Common.NotFound();

            User? user = _userManager.GetUsersByID(note.UserID);
            _noteManager.Delete(ID);
            await _hubContext.Clients.All.SendAsync("ChangesOnUserAndNotes", $"Note With ID [{note.ID}] Deleted That Note Is For {user.FirstName} {user.LastName}");

            return Common.NoContent();
        }


        /// <summary>
        /// Method for Update One Note
        /// </summary>
        /// <param name="user"></param>
        [HttpPut]
        [Route("UpdateNote")]
        public async Task<JsonResult> UpdateNote([FromBody] Note note)
        {
            if (note == null)
                return Common.BadRequest();

            Note? UpdatedNote = _noteManager.GetNote(note.ID);
            if (UpdatedNote == null)
                return Common.NotFound();

            await _hubContext.Clients.All.SendAsync("ChangesOnUserAndNotes", $"Note With ID [{note.ID}] Updated");

            _noteManager.Update(note);
            return Common.OkResult();
        }

    }
}

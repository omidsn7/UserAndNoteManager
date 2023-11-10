using Microsoft.AspNetCore.Mvc;
using UserAndNoteManager.Interface;
using UserAndNoteManager.Models;

namespace UserAndNoteManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteManager _noteManager;
        public NoteController(INoteManager noteManager)
        {
            _noteManager = noteManager;
        }

        /// <summary>
        /// Method for Create A New Note 
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        [Route("CreateNote")]
        public JsonResult CreateNote([FromBody] Note note)
        {
            if (note == null)
                return Common.BadRequest();

            _noteManager.Create(note);
            return Common.OkResult();
        }

        /// <summary>
        /// Method for Get All Notes Of a User
        /// </summary>
        /// <param name="user">User ID</param>
        [HttpGet]
        [Route("GetNotesOfUser")]
        public JsonResult GetNotesOfUser(int ID)
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
        public JsonResult GetNote(int ID)
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
        public JsonResult DeleteNote(int ID)
        {
            if (_noteManager.GetNote(ID) == null)
                return Common.NotFound();

            _noteManager.Delete(ID);

            return Common.NoContent();
        }


        /// <summary>
        /// Method for Update One Note
        /// </summary>
        /// <param name="user"></param>
        [HttpPut]
        [Route("UpdateNote")]
        public JsonResult UpdateNote(Note note)
        {
            if (note == null)
                return Common.BadRequest();

            if (_noteManager.GetNote(note.ID) == null)
                return Common.NotFound();

            _noteManager.Update(note);
            return Common.OkResult();
        }

    }
}

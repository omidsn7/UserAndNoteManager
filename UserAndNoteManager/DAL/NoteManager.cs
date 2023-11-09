using UserAndNoteManager.Interface;
using UserAndNoteManager.Models;
using UserAndNoteManager.Data;

namespace UserAndNoteManager.DAL
{
    public class NoteManager : INoteManager
    {
        private readonly UANDbContext _context;
        public NoteManager(UANDbContext context)
        {
            _context = context;
        }

        public void Create(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public void Delete(int ID)
        {
            Note note = _context.Notes.First(x => x.ID == ID);
            _context.Notes.Remove(note);
            _context.SaveChanges();
        }

        public Note? GetNote(int ID)
        {
            return _context.Notes.Where(x => x.ID == ID).FirstOrDefault();
        }

        public List<Note> GetNotesOfUser(int UserID)
        {
            return _context.Notes.Where(x => x.UserID == UserID).ToList();
        }

        public void Update(Note note)
        {
            throw new NotImplementedException();
        }
    }
}

using UserAndNoteManager.Models;

namespace UserAndNoteManager.Interface
{
    public interface INoteManager
    {
        void Create(Note note);
        List<Note> GetNotesOfUser(int UserID);
        Note? GetNote(int ID);
        void Delete(int ID);
        void Update(Note note);
    }
}

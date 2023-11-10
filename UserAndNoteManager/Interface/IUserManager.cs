using UserAndNoteManager.Models;

namespace UserAndNoteManager.Interface
{
    public interface IUserManager
    {
        string Create(User user);
        List<User> GetAllUsers();
        User? GetUsersByID(int ID);
        void Update(User user);
        void Delete(int ID);
    }
}

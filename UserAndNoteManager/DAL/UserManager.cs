using UserAndNoteManager.Models;
using UserAndNoteManager.Data;
using UserAndNoteManager.Interface;
using Microsoft.EntityFrameworkCore;

namespace UserAndNoteManager.DAL
{
    public class UserManager : IUserManager
    {
        private readonly UANDbContext _context;
        public UserManager(UANDbContext context)
        {
            _context = context;
        }

        public string Create(User user)
        {
            if (_context.Users.Any(x => x.Email.ToUpper() == user.Email.ToUpper()))
                return "This Email Is Already Taken";

            _context.Users.Add(user);
            _context.SaveChanges();
            return "done";
        }

        public void Delete(int ID)
        {
            User user = _context.Users.First(x => x.ID == ID);
            _context.Users.Remove(user);

            //Delete Notes Of User
            List<Note> notes = _context.Notes.Where(x => x.UserID == ID).ToList();
            foreach (Note note in notes)
                _context.Notes.Remove(note);

            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
             return _context.Users.ToList();
        }

        public User? GetUsersByID(int ID)
        {
            return _context.Users.Where(x => x.ID == ID).Include(us => us.Notes).FirstOrDefault();
        }

        public void Update(User user)
        {
            User UpdateUser = _context.Users.First(x => x.ID == user.ID);

            UpdateUser.FirstName = user.FirstName;
            UpdateUser.LastName = user.LastName;
            UpdateUser.Age = user.Age;
            UpdateUser.Email = user.Email;
            UpdateUser.Website = user.Website;

            _context.SaveChanges();
        }
    }
}

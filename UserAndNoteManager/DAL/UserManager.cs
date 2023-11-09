using UserAndNoteManager.Models;
using UserAndNoteManager.Data;
using UserAndNoteManager.Interface;

namespace UserAndNoteManager.DAL
{
    public class UserManager : IUserManager
    {
        private readonly UANDbContext _context;
        public UserManager(UANDbContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int ID)
        {
            User user = _context.Users.First(x => x.ID == ID);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
             return _context.Users.ToList();
        }

        public User? GetUsersByID(int ID)
        {
            return _context.Users.Where(x => x.ID == ID).FirstOrDefault();
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

using UserAndNoteManager.Models;
using UserAndNoteManager.Data;
using UserAndNoteManager.Interface;

namespace UserAndNoteManager.DAL
{
    public class UserManager : IUserManager
    {
        private readonly UANDbContext _AppDatabase;
        public UserManager(UANDbContext AppDatabase)
        {
            _AppDatabase = AppDatabase;
        }

        public void Create(User user)
        {
            _AppDatabase.Users.Add(user);
            _AppDatabase.SaveChanges();
        }

        public void Delete(int ID)
        {
            User user = _AppDatabase.Users.First(x => x.ID == ID);
            _AppDatabase.Users.Remove(user);
            _AppDatabase.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
             return _AppDatabase.Users.ToList();
        }

        public User? GetUsersByID(int ID)
        {
            return _AppDatabase.Users.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Update(User user)
        {
            User UpdateUser = _AppDatabase.Users.First(x => x.ID == user.ID);

            UpdateUser.FirstName = user.FirstName;
            UpdateUser.LastName = user.LastName;
            UpdateUser.Age = user.Age;
            UpdateUser.Email = user.Email;
            UpdateUser.Website = user.Website;

            _AppDatabase.SaveChanges();
        }
    }
}

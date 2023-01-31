using Tracker.Models;

namespace Tracker.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _context;
        public UserRepository(DataBaseContext context)
        {
            _context = context;
        }
        public User Create(User user)
        {
            _context.Users.Add(user);
            user.id = _context.SaveChanges();
            return user;
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.email == email);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.id == id);
        }

        public void RemoveByEmail(string email)
        {
            var user = _context.Users.Where(u => u.email == email).FirstOrDefault();
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}

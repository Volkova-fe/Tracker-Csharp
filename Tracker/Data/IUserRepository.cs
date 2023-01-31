using Tracker.Models;

namespace Tracker.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetByEmail(string email);
        User GetById(int id);
        void RemoveByEmail(string email);
    }
}

using System.Linq;

namespace DemoApplication.Models
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        User CreateUser(User user);
        void DeleteUser(int userID);
        User UpdateUser(User user);
    }
}

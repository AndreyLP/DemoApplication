using System.Linq;
namespace DemoApplication.Models
{
    public class EFUserRepository : IUserRepository
    {
        private ApplicationDbContext context;
        public EFUserRepository(ApplicationDbContext ctx) => context = ctx;
        public IQueryable<User> Users => context.Users;
        public User CreateUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            User dbEntry = context.Users.FirstOrDefault(u => u.UserID == user.UserID);
            if (dbEntry is null)
                throw new System.Exception("Юзер не найден");
            dbEntry.Login = user.Login;
            dbEntry.Password = user.Password;
            dbEntry.FullName = user.FullName;
            dbEntry.BirthDate = user.BirthDate;
            dbEntry.Email = user.Email;
            dbEntry.Phone = user.Phone;
            context.SaveChanges();
            return user;
        }

        public void DeleteUser(int userID)
        {
            User dbEntry = context.Users.FirstOrDefault(u => u.UserID == userID);
            if (dbEntry != null)
            {
                context.Users.Remove(dbEntry);
                context.SaveChanges();
            }
        }
    }
}

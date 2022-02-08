using DemoApplication.Models;

namespace DemoApplication.Services.Interfaces
{
    public interface IUserService
    {
        User CreateUser(User user, string passwordConfirmation);
        User UpdateUser(User user);
    }
}

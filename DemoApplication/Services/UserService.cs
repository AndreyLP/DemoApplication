using DemoApplication.Exceptions;
using DemoApplication.Models;
using DemoApplication.Services.Interfaces;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DemoApplication.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository UserRepository)
        {
            _userRepository = UserRepository ?? throw new System.ArgumentNullException(nameof(UserRepository));
        }
        public User CreateUser(User user, string passwordConfirmation)
        {
            if (_userRepository.Users.Any(_ => _.Login == user.Login))
                throw new ValidationException("Такой логин занят. Придумайте другой");

            if (user.BirthDate >= System.DateTime.Now)
                throw new ValidationException("Дата рождения из будущего");

            if (user.Password != passwordConfirmation)
                throw new ValidationException("Пароль не совпадает с подтверждением");
            
            var encoder = SHA256.Create();
            var binaryHash = encoder.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            user.Password = Encoding.UTF8.GetString(binaryHash);
            return _userRepository.CreateUser(user);
        }
    }


}

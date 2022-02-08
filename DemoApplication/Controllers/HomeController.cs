using DemoApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using DemoApplication.Services.Interfaces;
using DemoApplication.Exceptions;
using DemoApplication.Models.ViewModels;

namespace DemoApplication.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository _userRepository;

        private IUserService _userService;
        public HomeController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository ?? throw new System.ArgumentNullException(nameof(userRepository));
        }
        public IActionResult Index()
        {
            return View("RegistrationForm", new UserViewModel());
        }
        public ViewResult RegistrationForm(int userId) => View(new UserViewModel(_userRepository.Users.FirstOrDefault(_ => _.UserID == userId)));
        [HttpPost]
        public IActionResult RegistrationForm(UserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User()
                    {
                        Login = userVM.Login,
                        BirthDate = userVM.BirthDate,
                        Email = userVM.Email,
                        FullName = userVM.FullName,
                        Phone = userVM.Phone,
                        Password = userVM.Password,
                    };
                    user = _userService.CreateUser(user, passwordConfirmation: userVM.ConfirmPassword);
                    userVM = new UserViewModel(user);
                    return View("Thanks", userVM);
                }
                catch (ValidationException ex)
                {
                    userVM.Errors.Add(ex.Message);
                }
                catch
                {
                    userVM.Errors.Add("Что-то пошло не так. Наши разрабочики уже разбираются.");
                }
            }
            return View(userVM);
        }
        public ViewResult Create() => View("RegistrationForm", new UserViewModel(new User()));
        public ViewResult ListUser() => View("ListUser", _userRepository.Users);

        [HttpPost]
        public IActionResult Edit(UserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Login = userVM.Login,
                    BirthDate = userVM.BirthDate,
                    Email = userVM.Email,
                    FullName = userVM.FullName,
                    Phone = userVM.Phone,
                    Password = userVM.Password,
                };
                _userRepository.UpdateUser(user);
                return RedirectToAction("ListUser");
            }
            else
                return View(userVM);
        }
        public ViewResult EditUser(int userId) => View(new EditUserViewModel(_userRepository.Users.FirstOrDefault(_ => _.UserID == userId)));
        [HttpPost]
        public IActionResult EditUser(EditUserViewModel editUserVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userRepository.Users.FirstOrDefault(_ => _.UserID == editUserVM.UserID);
                    if (user != null)
                    {
                        user.BirthDate = editUserVM.BirthDate;
                        user.Email = editUserVM.Email;
                        user.FullName = editUserVM.FullName;
                        user.Phone = editUserVM.Phone;
                        _userService.UpdateUser(user);
                        return RedirectToAction("ListUser");
                    }
                }
                catch (ValidationException ex)
                {
                    editUserVM.Errors.Add(ex.Message);
                }
                catch
                {
                    editUserVM.Errors.Add("Что-то пошло не так. Наши разрабочики уже разбираются.");
                }
            }
            return View(editUserVM);
        }
        [HttpPost]
        public IActionResult Delete(int userID)
        {
            _userRepository.DeleteUser(userID);
            return RedirectToAction("ListUser");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

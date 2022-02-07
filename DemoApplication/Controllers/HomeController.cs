using DemoApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DemoApplication.Models;
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
                catch (Exception ex)
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
        //[HttpGet]
        //public ViewResult RegistrationForm()
        //{
        //    return View();
        //}
        public ViewResult RegistrationForm(int userId) => View(new UserViewModel(_userRepository.Users.FirstOrDefault(p => p.UserID == userId)));

    }
}

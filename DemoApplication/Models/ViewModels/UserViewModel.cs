using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoApplication.Models.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Errors = new List<string>();
        }

        public UserViewModel(User userModel)
        {
            UserID = userModel.UserID;
            Login = userModel.Login;
            Password = userModel.Password;
            FullName = userModel.FullName;
            BirthDate = userModel.BirthDate;
            Email = userModel.Email;
            Phone = userModel.Phone;
            Errors = new List<string>();
        }

        public int UserID { get; set; }
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Введите пароль ещё раз")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Введите ФИО")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Введите корректный email адрес")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите телефон")]
        [Phone(ErrorMessage = "Введите корректный номер телефона")]
        public string Phone { get; set; }

        public List<string> Errors { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DemoApplication.Models.ViewModels
{
    public class ListUserViewModel
    {
        public IEnumerable<ListUserViewModel> Users { get; set; }

    }
    public class _UserViewModel
    {
        public int UserID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}

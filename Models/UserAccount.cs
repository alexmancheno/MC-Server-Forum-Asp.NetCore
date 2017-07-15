using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MC_Forum.Models
{
    public class UserAccount
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3]\.)|(([\w-]+\.)+))([a-zA-Z{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter valid email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "UserPassword is required.")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Compare("UserPassword", ErrorMessage = "Please confirm your UserPassword.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
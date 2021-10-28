using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog.ViewModel
{
    public class UserDTO
    {
        [Required(ErrorMessage = "User Name is already in Use")]


        public string ProfilePic { get; set; }

        public string About { get; set; }
        public string userName { get; set; }

        [Required(ErrorMessage = "Email is already in Use")]
        [StringLength(256),  DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string confirmPassword { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
}

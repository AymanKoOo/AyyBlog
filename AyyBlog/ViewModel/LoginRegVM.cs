using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog.ViewModel
{
    public class LoginRegVM
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public IFormFile UserImgF { get; set; }

        public string ProfilePic { get; set; }

        [Required]
        [StringLength(256), DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string password { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string confirmPassword { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
}

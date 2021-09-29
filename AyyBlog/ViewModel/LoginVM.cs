using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog.ViewModel
{
    public class LoginVM
    {
        [Required]
        [StringLength(256), DataType(DataType.EmailAddress)]
        public string email { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
}

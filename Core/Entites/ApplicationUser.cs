using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entites
{
    public class ApplicationUser : IdentityUser
    {

        public string ProfilePic { get; set; }

        public string About { get; set; }


        public ICollection<Post> posts { get; set; }

    }
}

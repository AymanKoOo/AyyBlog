using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog.ViewModel
{
    public class ProfileVM
    {
        public string UserName { get; set; }
        public string email { get; set; }
        public string About { get; set; }

        public string Slug { get; set; }
        public string trending { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public string content { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}

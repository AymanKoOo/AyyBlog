using Core.Entites.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog.ViewModel
{
    public class PostDTO
    {
        public string author { get; set; }

        public IFormFile postImg { get; set; }
        public string picture { get; set; }
        public int CategoryId { get; set; }
        public string Slug { get; set; }

        public string trending { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public string content { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}

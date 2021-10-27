using Core.Entites;
using Core.Entites.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog.ViewModel
{
    public class PostDTO
    {
        public ApplicationUser applicationUser { get; set; }

        [Required]
        public IFormFile postImg { get; set; }
     
        public string picture { get; set; }
        public int CategoryId { get; set; }
        public string Slug { get; set; }
        public string trending { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string summary { get; set; }
        [Required]
        public string content { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}

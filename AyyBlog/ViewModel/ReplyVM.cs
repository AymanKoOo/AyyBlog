using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog.ViewModel
{
    public class ReplyVM
    {
        public string content { get; set; }
        public string userName { get; set; }
        public string ProfilePic { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}

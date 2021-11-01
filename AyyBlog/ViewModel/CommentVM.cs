using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog.ViewModel
{
    public class CommentVM
    {
        public string userName { get; set; }
        public int CommentId { get; set; }
        public string ProfilePic { get; set; }
        public string content { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<ReplyVM> Reply { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entites
{
    public class Reply
    {
        public int id { get; set; }

        public int commentID { get; set; }
        public Comment comment { get; set; }

        public string userId { get; set; }
        public ApplicationUser user { get; set; }

        public string content { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}

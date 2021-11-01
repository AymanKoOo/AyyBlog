using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entites
{
    public class Comment
    {
        public int Id { get; set; }

        public int postId { get; set; }
        public Post post { get; set; }

        public string userId { get; set; }
        public ApplicationUser user { get; set; }

        public string content { get; set; }
        public DateTime CreatedTime { get; set; }

        public ICollection<Reply> replies { get; set; }
    }
}

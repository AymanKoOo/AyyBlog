using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entites
{
    public class post_tag
    {
        public int tagId { get; set; }
        public Tag tags { get; set; }

        public int postId { get; set; }
        public Post posts { get; set; }

    }
}

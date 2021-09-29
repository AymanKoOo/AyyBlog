using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entites
{
    public class Tag
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string tagPic { get; set; }

        public ICollection<post_tag> post_Tags { get; set; }
    }
}

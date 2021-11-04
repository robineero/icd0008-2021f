using System.Collections.Generic;

namespace Domain
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set;  }
        public string? Body { get; set;  }
        
        public List<PostCategory>? PostCategories { get; set; }
    }
}
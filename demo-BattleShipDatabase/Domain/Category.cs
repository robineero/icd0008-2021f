﻿using System.Collections.Generic;

namespace Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        
        public List<PostCategory>? PostCategories { get; set; }
    }
}
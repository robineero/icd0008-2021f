using System;
using System.Collections.Generic;

namespace Domain
{
    public class Game
    {
        public int Id { get; set; }
        public String? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Player>? Players { get; set; }
    }
}
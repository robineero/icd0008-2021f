using System;
using System.Collections.Generic;

namespace Domain
{
    public class Game
    {
        public int Id { get; set; }
        public String? Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Player>? Players { get; set; }
    }
}
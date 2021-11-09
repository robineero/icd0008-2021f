using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Game
    {
        public int Id { get; set; }
        
        [Range(5, 100, ErrorMessage = "Board size must be between 5 and 100.")]
        public int BoardSize { get; set; }
        public String? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Player>? Players { get; set; }
    }
}
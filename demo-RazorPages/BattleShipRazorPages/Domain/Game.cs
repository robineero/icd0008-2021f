using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Game
    {
        public int Id { get; set; }
        
        [Range(5, 50, ErrorMessage = "{0} must be between {1} and {2}.")]
        public int BoardSize { get; set; }
        public String? Comment { get; set; }
        
        public bool HasStarted { get; set; }
        public bool IsOver { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Player>? Players { get; set; }
    }
}
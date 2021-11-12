using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "The {0} value not be less than {1} characters. ")]
        [MaxLength(128, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public String Name { get; set; } = default!;
        public String? Board { get; set; } // serialized json
        public Boolean NextMove { get; set; }
        
        public Char? Code { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int GameId { get; set; }
        public Game? Game { get; set; }
        
    }
}
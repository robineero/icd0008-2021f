using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Player
    {
        public int Id { get; set; }
        
        [MinLength(4, ErrorMessage = "The {0} value not be less than {1} characters. ")]  
        [MaxLength(128, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]  
        public String? Name { get; set; }
        
        public Char? Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int GameId { get; set; }
        public Game? Game { get; set; }
        
    }
}
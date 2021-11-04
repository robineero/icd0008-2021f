using System;
using System.Collections.Generic;

namespace Domain
{
    public class Player
    {
        public int Id { get; set; }
        public String? PlayerString { get; set; }
        
        public int GameId { get; set; }
        public Game? Game { get; set; }
    }
}
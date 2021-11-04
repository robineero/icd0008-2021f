using System;

namespace Domain
{
    public class Player
    {
        public int Id { get; set; } = default!;
        public String? Name { get; set; }
        public String? Code { get; set; }
        // public Board Board { get; set; }
        public Boolean MyTurn { get; set; }

        public Player()
        {
            
        }

        public Player(String code, String name, Boolean myTurn = false)
        {
            Code = code;
            Name = name;
            MyTurn = myTurn;
        }
    }
}
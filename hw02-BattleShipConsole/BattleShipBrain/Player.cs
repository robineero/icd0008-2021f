using System;

namespace BattleShipBrain
{
    public class Player
    {
        public Board Board { get; private set; }
        public String Name { get; set; }

        public Player(String name, Board board)
        {
            Name = name;
            Board = board;
        }
    }
}
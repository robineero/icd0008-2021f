using System;

namespace BattleShipBrain
{
    public class Player
    {
        private Guid _id;
        public Board Board { get; private set; }
        public String Name { get; set; }

        public Player(String name, Board board)
        {
            _id = new Guid();
            Name = name;
            Board = board;
        }
    }
}
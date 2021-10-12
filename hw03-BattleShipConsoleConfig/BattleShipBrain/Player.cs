using System;

namespace BattleShipBrain
{
    public class Player
    {
        public Board Board { get; private set; }
        public String Name { get; set; }
        public Boolean MyTurn { get; set; }

        public Player(String name, Board board, Boolean myTurn = false)
        {
            Name = name;
            Board = board;
            MyTurn = myTurn;
        }
    }
}
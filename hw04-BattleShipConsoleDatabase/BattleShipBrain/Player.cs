using System;

namespace BattleShipBrain
{
    public class Player
    {
        public String Name { get; set; }
        public String Code { get; set; }
        public Board Board { get; set; }
        public Boolean MyTurn { get; set; }

        public Player(String code, String name, Board board, Boolean myTurn = false)
        {
            Code = code;
            Name = name;
            Board = board;
            MyTurn = myTurn;
        }
    }
}
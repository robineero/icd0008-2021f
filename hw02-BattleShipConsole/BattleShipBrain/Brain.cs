using System;

namespace BattleShipBrain
{
    public class Brain
    {
        private readonly Board _boardA;
        private readonly Board _boardB;

        public Brain(int width, int height)
        {
            _boardA = new Board(width, height, true);
            _boardB = new Board(width, height);
        }

        public Board GetBoardA()
        {
            return _boardA;
        }        
        
        public Board GetBoardB()
        {
            return _boardA;
        }
        
    }
}
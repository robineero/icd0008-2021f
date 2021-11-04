namespace BattleShipBrain
{
    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public BoardSquareState BoardSquareState { get; set; }

        public Coordinate(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            if (BoardSquareState.IsBomb && BoardSquareState.IsShip)
            {
                return "X";
            }            
            if (BoardSquareState.IsBomb && !BoardSquareState.IsShip)
            {
                return "O";
            }            
            if (!BoardSquareState.IsBomb && BoardSquareState.IsShip)
            {
                return "S";
            }
            return "_";
        }
    }
}
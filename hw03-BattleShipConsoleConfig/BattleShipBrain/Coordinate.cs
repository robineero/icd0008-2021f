namespace BattleShipBrain
{
    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public BoardSquareState BoardSquareState { get; set; }

        public void PlaceBomb()
        {
            BoardSquareState = new BoardSquareState()
            {
                IsBomb = true,
                IsShip = false
            };
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
        
        // public override string ToString()
        // {
        //     return $"X: {X}, Y: {Y}";
        // }
    }
}
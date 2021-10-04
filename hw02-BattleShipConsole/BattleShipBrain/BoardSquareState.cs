namespace BattleShipBrain
{
    public struct BoardSquareState
    {
        public bool IsShip { get; set; }
        public bool IsBomb { get; set; }

        public override string ToString()
        {
            if (IsBomb && IsShip)
            {
                return "X";
            }            
            if (IsBomb && !IsShip)
            {
                return "O";
            }            
            if (!IsBomb && IsShip)
            {
                return "S";
            }
            return "_";
        }
    }
}
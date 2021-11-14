namespace BattleshipBrain
{
    public class BoardValidator
    {

        public bool HasCorrectNumberOfShipStateSquares(Board board)
        {
            var countOfShips = 0;

            foreach (var row in board.Rows)
            {
                foreach (var coord in row.Coordinates)
                {
                    if (coord.BoardSquareState.IsShip && !coord.BoardSquareState.IsBomb) countOfShips++;
                }
            }

            return countOfShips == 15;
        }
    }
}
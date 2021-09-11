using System;

namespace GameBoard
{
    public class Board
    {
        private Row[] GameBoard { get; set; }
        public Board(int width, int height)
        {
            GameBoard = new Row[height];

            for (int i = 0; i < height; i++)
            {
                GameBoard[i] = new Row(width);
            }
        }

        public void PlaceBomb(int row, int col)
        {
            GameBoard[row].PlaceBomb(col);
        }

        public override string ToString()
        {
            String board = "";
            foreach (var row in GameBoard)
            {
                board += row.ToString() + "\n";
            }

            return board;
        }
    }
}
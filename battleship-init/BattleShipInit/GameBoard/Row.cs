using System;

namespace GameBoard
{
    public class Row
    {
        public Cell[] BoardRow { get; set; }
        
        public Row(int width)
        {
            BoardRow = new Cell[width];
            for (int i = 0; i < width; i++)
            {
                BoardRow[i] = Cell.Empty;
            }
        }

        public void PlaceBomb(int col)
        {
            BoardRow[col] = Cell.Hit;
        }
        
        public override string ToString()
        {
            String row = "";

            foreach (var cell in BoardRow)
            {
                switch (cell)
                {
                    case Cell.Empty:
                        row += "_ ";
                        break;
                    case Cell.Ship:
                        row += "S ";
                        break;
                    case Cell.Hit:
                        row += "X ";
                        break;
                    case Cell.Miss:
                        row += "O ";
                        break;
                }
            }
            
            return row;
        }
    }
}
using System;
using System.Collections.Generic;

namespace BattleShipBrain
{
    public class Board
    {
        private List<Row> Rows { get; set; }
        public int Width { get; }
        
        public Board(int width, int height, bool random = false)
        {
            Rows = new();
            Width = width;

            for (int i = 0; i < height; i++)
            {
                Row row = new(width, random);
                Rows.Add(row);
            }
        }

        public override string ToString()
        {
            List <String> board = new();
            board.Add(GetHeader());
            for (int i = 0; i < Rows.Count; i++)
            {
                String rowNr = GetFormattedRowNr(i, Rows.Count);
                board.Add($"{rowNr}. {Rows[i]}");
            }
            
            return String.Join("\n", board);
        }

        private String GetHeader()
        {
            List <String> header = new();
            header.Add("    "); // padding
            for (int i = 0; i < Width; i++)
            {
                if (i < 10)
                {
                    header.Add(i + "   ");
                }
                else
                {
                    header.Add(i + "  ");
                }
                
            }

            return String.Join("", header);
        }

        private string GetFormattedRowNr(int currentRow, int rowCount)
        {
            if (currentRow < 10)
            {
                return $" {currentRow}";
            }
            return $"{currentRow}";
        }

        public BoardSquareState CurrentBoardSquareState(int x, int y)
        {
            return Rows[y].CurrentBoardSquareState(x);
        }

        public String PlaceBomb(int x, int y)
        {
            BoardSquareState state = CurrentBoardSquareState(x, y);
            Rows[y].PlaceBomb(x);

            if (state.IsShip && !state.IsBomb)
            {
                return "It was A HIT :)";
            }
            return "It was a miss :(";
        }
    }
}
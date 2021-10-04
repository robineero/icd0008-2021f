using System;
using System.Collections.Generic;

namespace BattleShipBrain
{
    public class Board
    {
        private List<Row> Rows { get; set; }
        
        public Board(int width, int height, bool random = false)
        {
            Rows = new();

            for (int i = 0; i < height; i++)
            {
                Row row = new(width, random);
                Rows.Add(row);
            }
        }

        public override string ToString()
        {
            List <String> board = new();

            for (int i = 0; i < Rows.Count; i++)
            {
                String rowNr = GetFormattedRowNr(i, Rows.Count);
                board.Add($"{rowNr}. {Rows[i]}");
            }
            

            return String.Join("\n", board);

        }

        private string GetFormattedRowNr(int currentRow, int rowCount)
        {
            if (currentRow < 10)
            {
                return $" {currentRow}";
            }
            return $"{currentRow}";
        }
    }
}
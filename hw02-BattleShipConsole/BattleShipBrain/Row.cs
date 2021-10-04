using System;
using System.Collections.Generic;

namespace BattleShipBrain
{
    public class Row
    {
        private List<BoardSquareState> _row;
        private readonly Random _random = new Random();
        public Row(int width, bool random = false)
        {
            _row = new List<BoardSquareState>();
            for (int i = 0; i < width; i++)
            {
                BoardSquareState cell = GetCell(random);
                _row.Add(cell);
            }
        }

        private BoardSquareState GetCell(bool random = false)
        {
            var cell = new BoardSquareState();
            if (random)
            {
                cell.IsBomb = _random.Next(0, 2) == 0;
                cell.IsShip = _random.Next(0, 2) == 0;
            }
            else
            {
                cell.IsBomb = false;
                cell.IsShip = false;
            }

            return cell;
        }

        public override string ToString()
        {
            String result = "";
            foreach (var r in _row)
            {
                result += r;
            }

            return result;
        }
    }
}
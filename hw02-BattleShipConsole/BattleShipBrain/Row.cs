using System;
using System.Collections.Generic;

namespace BattleShipBrain
{
    public class Row
    {
        public List<BoardSquareState> _row { get; private set; }
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
            List<String> result = new();
            foreach (var cell in _row)
            {
                result.Add(cell.ToString());
            }

            return String.Join(" | " ,result);
        }
        
        public void PlaceBomb(int x)
        {
            _row[x] = new BoardSquareState()
            {
                IsBomb = true,
                IsShip = false
            };
        }        
        
        public BoardSquareState CurrentBoardSquareState(int x)
        {
            return _row[x];
        }
    }
}
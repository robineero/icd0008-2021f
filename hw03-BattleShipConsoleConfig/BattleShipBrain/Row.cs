using System;
using System.Collections.Generic;

namespace BattleShipBrain
{
    public class Row
    {
        public List<Coordinate> _row { get; private set; } = new();
        private readonly Random _random = new Random();
        public Row(int rowNr, int width, bool random = false)
        {
            for (int i = 0; i < width; i++)
            {
                BoardSquareState squareState = GetSquareState(random);
                Coordinate coordinate = new Coordinate()
                {
                    X = i,
                    Y = rowNr,
                    BoardSquareState = squareState
                };
                    
                _row.Add(coordinate);
            }
        }

        private BoardSquareState GetSquareState(bool random = false)
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
            _row[x].PlaceBomb();
        }        
        
        public BoardSquareState CurrentBoardSquareState(int x)
        {
            return _row[x].BoardSquareState;
        }
    }
}
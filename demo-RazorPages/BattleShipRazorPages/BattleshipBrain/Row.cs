using System;
using System.Collections.Generic;

namespace BattleshipBrain
{
    public class Row
    {
        public List<Coordinate> Coordinates { get; set; } = new();
        private readonly Random _random = new Random();
        private readonly int _width;
        
        public Row()
        {
        }

        public Row(int rowNr, int width, bool random = false)
        {
            _width = width;
            
            for (int i = 0; i < width; i++)
            {
                BoardSquareState squareState = GetSquareState(random);
                Coordinate coordinate = new Coordinate()
                {
                    X = i,
                    Y = rowNr,
                    BoardSquareState = squareState
                };
                    
                Coordinates.Add(coordinate);
            }
        }

        private BoardSquareState GetSquareState(bool random = false)
        {
            var cell = new BoardSquareState();
            if (random)
            {
                // cell.IsBomb = _random.Next(0, 4) == 0;
                cell.IsShip = _random.Next(0, _width) == 0;
            }
            else
            {
                cell.IsBomb = false;
                cell.IsShip = false;
            }

            return cell;
        }

    }
}
using System;
using System.Collections.Generic;

namespace BattleShipBrain
{
    public class Row
    {
        public List<Coordinate> _row { get; set; } = new();
        private readonly Random _random = new Random();

        public Row()
        {
        }

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

        public override string ToString() // acts like voic
        {
            for (int i = 0; i < _row.Count; i++)
            {
                Coordinate cell = _row[i];
                BoardSquareState bss = cell.BoardSquareState;

                if (bss.IsShip && !bss.IsBomb)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(cell);
                    Console.ResetColor();
                } else if (bss.IsShip && bss.IsBomb)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(cell);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(cell);
                }
                
                if (i != _row.Count -1) Console.Write(" | ");
            }
            
            return "";
        }
        
        public void PlaceBomb(int x)
        {
            _row[x] = new Coordinate() { X = _row[x].X, Y = _row[x].Y, BoardSquareState = new BoardSquareState()
            {
                IsBomb = true,
                IsShip = false
            }};
        }        
        
    }
}
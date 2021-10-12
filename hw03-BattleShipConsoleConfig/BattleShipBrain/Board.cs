using System;
using System.Collections.Generic;
using System.Threading;

namespace BattleShipBrain
{
    public class Board
    {
        public List<Row> Rows { get; set; } = new List<Row>();
        public int Width { get; set;  }

        public Board()
        {
        }

        public Board(Config conf)
        {
            Rows = new();
            Width = conf.Width;

            for (int i = 0; i < conf.Height; i++)
            {
                Row row = new(i, conf.Width, conf.Random);
                Rows.Add(row);
            }
        }

        public void AddShip(Ship ship)
        {
            List<Coordinate> coordinates = ship.Coordinates;
            foreach(Coordinate coord in coordinates)
            {
                Rows[coord.Y]._row[coord.X] = new Coordinate()
                {
                    X = coord.X,
                    Y = coord.Y,
                    BoardSquareState = new BoardSquareState()
                    {
                        IsBomb = false,
                        IsShip = true
                    }
                };
            }
        }

        public override string ToString()
        {
            PrintBoardHeader();
            
            for (int i = 0; i < Rows.Count; i++)
            { 
                Thread.Sleep(100);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                String rowNr = GetFormattedRowNr(i, Rows.Count);
                Console.Write($"{rowNr}. ");
                Console.ResetColor();
                Console.Write($"{Rows[i]}");
                if (i != Rows.Count - 1) Console.Write("\n");
            }

            return "";
        }
        
        private void PrintBoardHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" →  "); // padding
            for (int i = 0; i < Width; i++)
            {
                if (i < 10)
                {
                    Console.Write(i + "   ");
                }
                else
                {
                    Console.Write(i + "  ");
                }
            }
            Console.Write("\n");
            Console.ResetColor();
        }

        private string GetFormattedRowNr(int currentRow, int rowCount)
        {
            if (currentRow < 10)
            {
                return $" {currentRow}";
            }
            return $"{currentRow}";
        }
        
        public String PlaceBomb(int x, int y)
        {
            BoardSquareState state = Rows[y]._row[x].BoardSquareState;
            
            if (state.IsShip && !state.IsBomb)
            {
                Rows[y]._row[x] = CoordinateFactory(x, y, true, true);
                return "It was A HIT :)";
            } 
            if (state.IsBomb)
            {
                return "C'mon. You already had bomb there!";
            } 
            if (!state.IsShip && !state.IsBomb)
            {
                Rows[y]._row[x] = CoordinateFactory(x, y, true, false);
                return "It was a miss :(";
            }
            return "It was a miss :(";
        }

        public Coordinate CoordinateFactory(int x, int y, Boolean isBomb, Boolean isShip)
        {
            BoardSquareState newState = new BoardSquareState()
            {
                IsBomb = isBomb,
                IsShip = isShip
            };
            
            Coordinate coordinate = new Coordinate()
            {
                X = x,
                Y = y,
                BoardSquareState = newState
            };

            return coordinate;
        }
    }
}
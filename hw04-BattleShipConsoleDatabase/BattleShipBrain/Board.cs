using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BattleShipBrain
{
    public class Board
    {
        public List<Row> Rows { get; set; } = new();
        public List<Ship> Ships { get; set; } = new();
        public int Width { get; set; }
        public int Height { get; set; }

        public Board()
        {
        }

        public Board(Config conf)
        {
            Rows = new();
            Width = conf.Width;
            Height = conf.Height;

            for (int i = 0; i < conf.Height; i++)
            {
                Row row = new(i, conf.Width, conf.Random);
                Rows.Add(row);
            }
        }

        public void AddShip(Ship ship)
        {
            Ships.Add(ship);
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
            
            if (state.IsBomb)
            {
                Console.WriteLine("C'mon. You already had a bomb there!! Please choose again.");
                return "";
            }
            if (state.IsShip && !state.IsBomb)
            {
                Ship? ship = WhatShipIsThere(x, y);
                Rows[y]._row[x] = CoordinateFactory(x, y, true, true);
                return $"It was A HIT :) and the ship was {ship!.Name}";
            }
            if (!state.IsShip && !state.IsBomb)
            {
                Rows[y]._row[x] = CoordinateFactory(x, y, true, false);
                return "It was a miss. Sooo sad :(";
            }
            return "It was a miss. Sooo sad :(";
        }

        private Ship? WhatShipIsThere(int x, int y)
        {
            foreach (Ship ship in Ships)
            {
                foreach (var coord in ship.Coordinates)
                {
                    if (coord.X == x && coord.Y == y)
                    {
                        return ship;
                    }
                }
            }

            return null;
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

        public void SetupNewBoard()
        {
            ShipFactory shipFactory = new ();
            Dictionary<ShipType, String> names = new()
            {
                // { ShipType.Carrier, "Carrier (1x5)" },
                // { ShipType.Battleship, "Battleship (1x4)" },
                // { ShipType.Submarine, "Submarine (1x3)" },
                { ShipType.Cruiser, "Cruiser (1x2)" },
                { ShipType.Patrol, "Patrol (1x1)" },
            };

            foreach (var name in names)
            {
                Ship ship;
                int row;
                int col;
                string input;
                ShipDirection direction = ShipDirection.NorthSouth;
                Console.WriteLine(ToString());
                Console.WriteLine($"Enter coordinates for {name.Value} ship");
                
                while (true)
                {
                    Console.Write("Col: ");
                    input = Console.ReadLine()?.Trim() ?? "";
                    col = input == "" ? -1 : Int32.Parse(input);
                
                    Console.Write("Row: ");
                    input = Console.ReadLine()?.Trim() ?? "";
                    row = input == "" ? -1 : Int32.Parse(input);
                    
                    Console.Write("Direction (NS*/EW): ");
                    input = Console.ReadLine()?.Trim().ToLower() ?? "";
                    direction = input == "ew" ? ShipDirection.EastWest : ShipDirection.NorthSouth;
                    ship = shipFactory.GetShip(col, row, name.Key, direction);

                    if (row >= 0 && col >= 0 && col < Width && row < Height)
                    {
                        if (direction == ShipDirection.NorthSouth && row <= Height - ship.Coordinates.Count())
                            break;
                        if (direction == ShipDirection.EastWest && row <= Width - ship.Coordinates.Count())
                            break;
                    }
                    Console.WriteLine("Something went wrong with the input. Try again.");

                }

                AddShip(ship);  // TODO: This step needs validation for ArgumentOutOfRangeException
            }

            Console.WriteLine(ToString());
            Console.WriteLine("Your ships are now placed. Press any key to continue...");
            Console.ReadLine();
        }
    }
}
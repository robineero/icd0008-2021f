using System;
using System.Collections.Generic;

namespace BattleshipBrain
{
    public class Board
    {
        public List<Row> Rows { get; set; } = new();
        public int Width { get; set; }
        public int Height { get; set; }
        
        private readonly Random _random = new();
        
        public Board()
        {
        }

        public Board(Config conf)
        {
            Width = conf.Width;
            Height = conf.Height;
            BoardRows(conf);
        }

        private void BoardRows(Config config)
        {
            // Create empty board
            for (int y = 0; y < config.Height; y++)
            {
                Row row = new();
                for (int x = 0; x < config.Width; x++)
                {
                    Coordinate coordinate = new Coordinate()
                    {
                        X = x,
                        Y = y,
                        BoardSquareState = new BoardSquareState()
                    };
                    row.Coordinates.Add(coordinate);
                }
                Rows.Add(row);
            }
            
            // Place carrier
            foreach (var crd in PlaceShip(5))
            {
                Rows[crd.Y].Coordinates[crd.X] = crd;
            }
            
            if (config.Random)
            {
                PlaceRandomShips();
            }
        }

        private void PlaceRandomShips()
        {
            var randomCoords = new List<Coordinate>();
            var bss = new BoardSquareState();
            for (int i = 0; i < 15;)
            {
                bss.IsShip = true;
                Coordinate coordinate = new Coordinate()
                {
                    X = _random.Next(0, Width),
                    Y = _random.Next(0, Height),
                    BoardSquareState = bss
                };
                
                if (!randomCoords.Contains(coordinate))
                {
                    randomCoords.Add(coordinate);
                    i++;
                }
            }

            foreach (var crd in randomCoords)
            {
                Rows[crd.Y].Coordinates[crd.X] = crd;
            }
        }
        
        public List<Coordinate> PlaceShip(int lenght)
        {
            // Direction direction = (Direction) (Enum.GetValues(typeof(Direction)).GetValue(_random.Next(0, 2)) ?? Direction.Ns);
            var dir = _random.Next(0, 2);
            bool ns = dir == 0;
            bool ew = dir == 1;
            
            List<Coordinate> ship = new();
            var shipLenght = lenght;
            
            var startX = _random.Next(0, ew ? Height - shipLenght : Height);
            var startY = _random.Next(0, ns ? Width - shipLenght : Width);
            
            var bss = new BoardSquareState
            {
                IsShip = true
            };
            
            for (int i = 0; i < shipLenght; i++)
            {
                ship.Add(
                    new Coordinate()
                    {
                        X = ew ? startX + i : startX,
                        Y = ns ? startY + i : startY,
                        BoardSquareState = bss
                    }
                    );
            }

            return ship;
        }
        

        public int ShipCount()
        {
            int count = 0;

            foreach (var row in Rows)
            {
                
                foreach (var coord in row.Coordinates)
                {
                    var bss = coord.BoardSquareState;
                    if (bss.IsShip && !bss.IsBomb)
                    {
                        count++;
                    }
                }
            }
            
            return count;
        }

        public void PlaceBomb(int x, int y)
        {
            BoardSquareState state = Rows[y].Coordinates[x].BoardSquareState;
            Rows[y].Coordinates[x] = CoordinateFactory(x, y, true, state.IsShip);
        }
        
        public void PlaceShip(int x, int y)
        {
            BoardSquareState state = Rows[y].Coordinates[x].BoardSquareState;
            Rows[y].Coordinates[x] = CoordinateFactory(x, y, state.IsBomb, true);
        }
        
        public Coordinate CoordinateFactory(int x, int y, bool isBomb, bool isShip)
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
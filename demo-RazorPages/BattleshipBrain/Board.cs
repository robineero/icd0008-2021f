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
            var bss = new BoardSquareState();
            
            for (int y = 0; y < config.Height; y++)
            {
                Row row = new();
                for (int x = 0; x < config.Width; x++)
                {
                    bss.IsShip = false;
                    Coordinate coordinate = new Coordinate()
                    {
                        X = x,
                        Y = y,
                        BoardSquareState = bss
                    };
                    row.Coordinates.Add(coordinate);
                }
                Rows.Add(row);
            }
            
            if (config.Random)
            {
                PlaceRandomCoords();
            }
        }

        private void PlaceRandomCoords()
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
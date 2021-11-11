using System.Collections.Generic;

namespace BattleshipBrain
{
    public class Board
    {
        public List<Row> Rows { get; set; } = new();
        public int Width { get; set; }
        public int Height { get; set; }
        
        public Board()
        {
        }

        public Board(Config conf)
        {
            do
            {
                Rows = new();
                Width = conf.Width;
                Height = conf.Height;

                for (int i = 0; i < conf.Height; i++)
                {
                    if (ShipCount() < 15)
                    {
                        Row row = new(i, conf.Width, conf.Random);
                        Rows.Add(row);
                    }
                    else
                    {
                        Row row = new(i, conf.Width);
                        Rows.Add(row);
                    }
                }

                if (!conf.Random) break;
                
            } while (ShipCount() != 15);
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
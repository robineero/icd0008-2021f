using System.Collections.Generic;

namespace BattleShipBrain
{
    public class ShipFactory
    {
        // Carrier     1 x 5
        // Battleship  1 x 4
        // Submarine   1 x 3
        // Cruiser     1 x 2
        // Patrol      1 x 1
        
        public Ship GetShip(int x, int y, ShipType type, ShipDirection direction = ShipDirection.NorthSouth)
        {
            Ship ship = new (type, GetCoordinates(x, y, type, direction));
            return ship;
        }

        private List<Coordinate> GetCoordinates(int x, int y, ShipType type, ShipDirection direction)
        {
            List<Coordinate> coordinates = new();
            for (int i = 0; i < (int) type; i++)  // sorry for the cast to get Enum to int
            {
                if (direction == ShipDirection.NorthSouth)
                    coordinates.Add(new Coordinate(x, y + i));
                else
                    coordinates.Add(new Coordinate(x + i, y));
            }
            return coordinates;
        }
    }

    public enum ShipDirection
    {
        NorthSouth,
        EastWest
    }

    public enum ShipType
    {
        Patrol = 1,
        Cruiser = 2,
        Submarine = 3,
        Battleship = 4,
        Carrier = 5
    }
}
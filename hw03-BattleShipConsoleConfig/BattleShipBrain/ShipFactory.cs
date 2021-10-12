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

        public Ship GetPatrol(int x, int y, ShipDirection direction = ShipDirection.NorthSouth)
        {
            List<Coordinate> coordinates = new();
            coordinates.Add(new Coordinate(x, y));
            Ship patrol = new Ship("Patrol", coordinates);
            
            return patrol;
        }        
        
        public Ship GetSubmarine(int x, int y, ShipDirection direction = ShipDirection.NorthSouth)
        {
            List<Coordinate> coordinates = new();
            for (int i = 0; i < 3; i++)
            {
                if (direction == ShipDirection.NorthSouth)
                    coordinates.Add(new Coordinate(x, y + i));
                else
                    coordinates.Add(new Coordinate(x + i, y));
            }
            Ship submarine = new ("Submarine", coordinates);
            return submarine;
        }
    }

    public enum ShipDirection
    {
        NorthSouth,
        EastWest
    }
}
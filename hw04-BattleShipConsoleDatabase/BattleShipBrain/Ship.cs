using System;
using System.Collections.Generic;

namespace BattleShipBrain
{
    public class Ship
    {
        public String Name { get; set; } = default!;
        public List<Coordinate> Coordinates { get; set; } = new List<Coordinate>();

        public Ship()
        {
        }

        public Ship(ShipType type, List<Coordinate> coordinates)
        {
            Coordinates = coordinates;
            
            switch (type)
            {
                case ShipType.Carrier:
                    Name = "Carrier";
                    break;
                case ShipType.Battleship:
                    Name = "Battleship";
                    break;
                case ShipType.Submarine:
                    Name = "Submarine";
                    break;
                case ShipType.Cruiser:
                    Name = "Cruiser";
                    break;
                default:
                    Name = "Patrol";
                    break;
            }
        }
    }
}
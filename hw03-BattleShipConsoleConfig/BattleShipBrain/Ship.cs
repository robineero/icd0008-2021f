using System;
using System.Collections.Generic;

namespace BattleShipBrain
{
    public class Ship
    {
        public String Name { get; private set; }
        public List<Coordinate> Coordinates { get; set; }

        public Ship(string name, List<Coordinate> coordinates)
        {
            Name = name;
            Coordinates = coordinates;
        }
    }
}
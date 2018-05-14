using System;

namespace Assets.Scripts.AStarPathFinding {
    public class GlobalDirection {
        public static Direction GetDirection(Position p1, Position p2) {
            double angle = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
            angle += Math.PI;
            angle /= Math.PI / 4;
            int halfQuarter = Convert.ToInt32(angle);
            halfQuarter %= 8;
            return (Direction) halfQuarter;
        }
    }

    public enum Direction {
        North = 0,
        NorthWest = 1,
        West = 2,
        SouthWest = 3,
        South = 4,
        SouthEast = 5,
        East = 6,
        NorthEast = 7,
        Undefined = -1
    }
}


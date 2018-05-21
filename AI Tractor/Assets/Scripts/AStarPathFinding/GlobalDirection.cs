using System;
using UnityEngine;

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
        public static string GetAction(Direction currentDirection, Direction targetDirection) {
            int rotation = ((int) targetDirection - (int) currentDirection + 8) % 8;

            if (rotation == 0)
                return ("Straight");
            else if (rotation == 1)
                return ("Turn left (S)");
            else if (rotation == 2)
                return ("Turn left");
            else if (rotation == 3)
                return ("Turn left (H)");
            else if (rotation == 4)
                return ("Turn back");
            else if (rotation == 5)
                return ("Turn right (H)");
            else if (rotation == 6)
                return ("Turn right");
            else
                return ("Turn left (S)");
        }
        public static Direction GetDirection(GameObject gameObject) {
            // TODO: Angle czasem zwraca zły wynik (zwraca 0)
            float angle = (gameObject.transform.localRotation.eulerAngles.y + 360F) % 360F;

            if (angle >= 0F + (45F / 2) && angle < 90F - (45F / 2)) {
                return Direction.SouthEast;
            } else if (angle >= 90F - (45F / 2) && angle < 90F + (45F / 2)) {
                return Direction.South;
            } else if (angle >= 90F + (45F / 2) && angle < 180F - (45F / 2)) {
                return Direction.SouthWest;
            } else if (angle >= 180F - (45F / 2) && angle < 225F - (45F / 2)) {
                return Direction.West;
            } else if (angle >= 225F - (45F / 2) && angle < 270F - (45F / 2)) {
                return Direction.NorthWest;
            } else if (angle >= 270F - (45F / 2) && angle < 315F - (45F / 2)) {
                return Direction.North;
            } else if (angle >= 315F - (45F / 2) && angle < 360F - (45F / 2)) {
                return Direction.NorthEast;
            } else {
                return Direction.East;
            }
        }
    }

    public enum Direction {
        North,
        NorthWest,
        West,
        SouthWest,
        South,
        SouthEast,
        East,
        NorthEast
    }
}


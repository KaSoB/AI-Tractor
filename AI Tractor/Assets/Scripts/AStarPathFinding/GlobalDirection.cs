using System.Collections.Generic;
using UnityEngine;


public class GlobalDirection {
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
    public static Dictionary<Direction, Position> Locations = new Dictionary<Direction, Position>() {
                { Direction.North,  new Position { X=-1, Y=0 } },
                { Direction.NorthEast, new Position { X=-1, Y=1 } },
                { Direction.East,  new Position { X=0, Y=1 } },
                { Direction.SouthEast, new Position { X=1, Y=1 } },
                { Direction.South,  new Position { X=1, Y=0 } },
                { Direction.SouthWest, new Position { X=1,Y=-1 } },
                { Direction.West,  new Position { X=0, Y=-1 } },
                { Direction.NorthWest, new Position { X=-1, Y=-1 } }
            };
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



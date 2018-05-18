using UnityEngine;

namespace Assets.Scripts.AStarPathFinding {
    public class Position {
        public int X { get; set; }
        public int Y { get; set; }

        public static Position operator +(Position left, Position right) {
            return new Position { X = left.X + right.X, Y = left.Y + right.Y };
        }

        public static implicit operator Position(Vector3 vector) {
            // w 3D component Z odpowiednikiem componentu Y w 2D
            return new Position { X = (int) vector.x, Y = (int) vector.z };
        }
        public override string ToString() {
            return X + " " + Y;
        }
    }
}

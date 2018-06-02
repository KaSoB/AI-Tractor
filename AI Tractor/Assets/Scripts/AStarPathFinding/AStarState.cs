using System;

namespace AStarPathFinding {
    public class AStarState : IComparable<AStarState>, IEquatable<AStarState> {
        public Node Node { get; set; }

        public Direction Action { get; set; }
        public AStarState Parent { get; set; }

        public int CompareTo(AStarState other) {
            return Node.CompareTo(other.Node);
        }
        public override int GetHashCode() {
            return Node.GetHashCode();
        }
        public override bool Equals(object obj) {
            return Node.Equals(obj);
        }

        public bool Equals(AStarState other) {
            return other != null && Node == other.Node;
        }
    }
}

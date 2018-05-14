using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.AStarPathFinding {
    public class MoveState : IComparable<MoveState>, IEquatable<MoveState> {
        public Node Node { get; set; }
        public Direction Direction { get; set; }

        //tego tu nie powinno być :/ jest w Node
        public MoveState Parent { get; set; }


        public int CompareTo(MoveState other) {
            return Node.CompareTo(other.Node);
        }

        public static IEnumerable<Node> ToNodes(IEnumerable<MoveState> moveStates) {
            return moveStates.Select(y => y.Node).ToList();

        }

        public bool Equals(MoveState other) {
            return Node == other.Node;
        }
    }
}

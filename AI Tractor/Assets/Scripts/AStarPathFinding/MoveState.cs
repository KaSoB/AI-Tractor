using System;
using System.Collections.Generic;

namespace Assets.Scripts.AStarPathFinding {
    public class MoveState : IComparable<MoveState>, IEquatable<MoveState> {
        public Node Node { get; set; }
        public Direction Direction { get; set; }
        public MoveState Parent { get; set; }


        public int CompareTo(MoveState other) {
            if (Node.F_Score > other.Node.F_Score) {
                return 1;
            } else if (Node.F_Score == other.Node.F_Score && Node.H_Score > other.Node.H_Score) {
                return 1;
            } else {
                return -1;
            }
        }

        public static IEnumerable<Node> ToNodes(IEnumerable<MoveState> moveStates) {
            List<Node> nodes = new List<Node>();
            foreach (var item in moveStates) {
                nodes.Add(item.Node);
            }
            return nodes;
        }

        public bool Equals(MoveState other) {
            return Node == other.Node;
        }
    }
}

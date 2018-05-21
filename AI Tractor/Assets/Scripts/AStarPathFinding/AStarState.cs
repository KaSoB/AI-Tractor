using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.AStarPathFinding {
    public class AStarState {
        public Node Node { get; set; }
        public Direction Direction { get; set; }
        public string Action { get; set; }

        public static IEnumerable<Node> ToNodes(IEnumerable<AStarState> moveStates) {
            return moveStates.Select(y => y.Node).ToList();
        }

        public static IEnumerable<AStarState> CreateStates(List<Node> nodes, Node startNode) {
            var states = new List<AStarState>();

            // Wybaczcie, ale nie chciałem przekopywać jednej zmiennej przez kilkanaście metod :/ 
            var currentDirection = GlobalDirection.GetDirection(GameObject.FindGameObjectsWithTag("AI").OrderBy(y => Vector3.Distance(startNode.transform.position, y.transform.position)).First());
            var currentNode = startNode;

            foreach (var node in nodes) {

                var state = new AStarState();
                state.Node = node;
                state.Direction = GlobalDirection.GetDirection(currentNode.Position, node.Position);
                state.Action = GlobalDirection.GetAction(currentDirection, state.Direction);

                states.Add(state);

                currentNode = node;
                currentDirection = state.Direction;
            }
            return states;
        }

    }
}

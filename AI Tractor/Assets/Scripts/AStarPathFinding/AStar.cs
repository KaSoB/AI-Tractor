using Assets.Scripts.AStarPathFinding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AStarPathFinding {
    /// <summary>
    /// Info: https://en.wikipedia.org/wiki/A*_search_algorithm
    /// </summary>
    public static class AStar {
        public static IEnumerable<MoveState> FindPath(NodesGrid grid, MoveState startNode, MoveState targetNode) {
            if (!startNode.Node.Walkable || !targetNode.Node.Walkable) {
                return null;
            }


            grid.ClearScore();

            List<MoveState> openSet = new List<MoveState>() {
                startNode
            };
            HashSet<MoveState> closedSet = new HashSet<MoveState>();

            while (openSet.Any()) {
                MoveState currentNode = openSet.Min();

                if (currentNode.Equals(targetNode)) {
                    return ReconstructPath(startNode, targetNode);
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                // znajdź wszystkich sąsiadów obecnego węzła, który jest przechodni i nie był wcześniej odwiedzany
                foreach (MoveState neighbour in GetNeighbours(grid, currentNode).Where(it => it.Node.Walkable && !closedSet.Contains(it))) {

                    int costToNeighbour = currentNode.Node.G_Score + neighbour.Node.Cost + GetDistance(currentNode.Node.Position, neighbour.Node.Position);
                    if (costToNeighbour < neighbour.Node.G_Score || !openSet.Contains(neighbour)) {
                        neighbour.Node.G_Score = costToNeighbour;
                        neighbour.Node.H_Score = GetDistance(neighbour.Node.Position, targetNode.Node.Position);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour)) {
                            openSet.Add(neighbour);
                        }

                    }
                }
            }
            return null;
        }

        private static IEnumerable<MoveState> GetNeighbours(NodesGrid grid, MoveState node) {
            List<MoveState> neighbours = new List<MoveState>();
            foreach (var location in NeighbourLocations) {
                var neighboursPosition = node.Node.Position + location.Value;
                if (grid.IsInsideGrid(neighboursPosition)) {
                    var state = grid.GetMoveState(neighboursPosition);
                    state.Node = grid.GetNode(neighboursPosition);
                    neighbours.Add(state);
                }
            }
            return neighbours;
        }
        private static IEnumerable<MoveState> ReconstructPath(MoveState startNode, MoveState targetNode) {
            List<MoveState> path = new List<MoveState>();
            MoveState currentNode = targetNode;
            Direction currentDirection = Direction.North;
            while (!currentNode.Equals(startNode)) {
                // Dodaj do ścieżki
                path.Add(currentNode);
                // Ustaw kierunek
                currentNode.Direction = GlobalDirection.GetDirection(currentNode.Node.Position, currentNode.Parent.Node.Position);
                // Ustaw akcje
                currentNode.Action = MoveState.SetAction(currentDirection, currentNode.Direction);
                // Odśwież obecny kierunek dla następnej pętli
                currentDirection = currentNode.Direction;

                currentNode = currentNode.Parent;

            }
            path.Reverse();
            return path;
        }
        public static Dictionary<Direction, Position> NeighbourLocations = new Dictionary<Direction, Position>() {
                { Direction.North,  new Position { X=0, Y=1 } },
                { Direction.NorthEast, new Position { X=1, Y=1 } },
                { Direction.East,  new Position { X=1, Y=0 } },
                { Direction.SouthEast, new Position { X=1, Y=-1 } },
                { Direction.South,  new Position { X=0, Y=-1 } },
                { Direction.SouthWest, new Position { X=-1,Y=-1 } },
                { Direction.West,  new Position { X=-1, Y=0 } },
                { Direction.NorthWest, new Position { X=-1, Y=1 } }
            };
        public static int GetDistance(Position position1, Position position2) {
            int dstX = Mathf.Abs(position1.X - position2.X);
            int dstY = Mathf.Abs(position1.Y - position2.Y);

            return (dstX > dstY) ? 14 * dstY + 10 * (dstX - dstY) : 14 * dstX + 10 * (dstY - dstX);
        }
    }
}


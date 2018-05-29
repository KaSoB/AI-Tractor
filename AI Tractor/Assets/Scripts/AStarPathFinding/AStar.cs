using Assets.Scripts.AStarPathFinding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AStarPathFinding {
    /// <summary>
    /// Info: https://en.wikipedia.org/wiki/A*_search_algorithm
    /// </summary>
    public static class AStar {
        public static IEnumerable<AStarState> FindPath(NodesGrid grid, Node startNode, Node targetNode) {
            if (!startNode.Walkable || !targetNode.Walkable) {
                return new List<AStarState>();
            }
            grid.ClearScore();
            // todo: kolejka priorytetowa
            List<Node> openSet = new List<Node>() {
                startNode
            };
            HashSet<Node> closedSet = new HashSet<Node>();

            while (openSet.Any()) {
                var currentNode = openSet.Min();

                if (currentNode == targetNode) {
                    return ReconstructPath(startNode, targetNode);
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                // znajdź wszystkich sąsiadów obecnego węzła, który jest przechodni i nie był wcześniej odwiedzany
                foreach (Node neighbour in GetNeighbours(grid, currentNode).Where(it => it.Walkable && !closedSet.Contains(it))) {

                    int costToNeighbour = currentNode.G_Score + neighbour.Cost + GetDistance(currentNode.Position, neighbour.Position);
                    if (costToNeighbour < neighbour.G_Score || !openSet.Contains(neighbour)) {
                        neighbour.G_Score = costToNeighbour;
                        neighbour.H_Score = GetDistance(neighbour.Position, targetNode.Position);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour)) {
                            openSet.Add(neighbour);
                        }

                    }
                }
            }
            return new List<AStarState>();
        }

        private static IEnumerable<AStarState> ReconstructPath(Node startNode, Node targetNode) {
            List<Node> path = new List<Node>();
            Node currentNode = targetNode;
            while (currentNode != startNode) {
                // Dodaj do ścieżki
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();
            return AStarState.CreateStates(path, startNode);
        }

        private static IEnumerable<Node> GetNeighbours(NodesGrid grid, Node node) {
            List<Node> neighbours = new List<Node>();
            foreach (var location in NeighbourLocations) {
                var neighboursPosition = node.Position + location.Value;

                if (grid.IsInsideGrid(neighboursPosition)) {
                    neighbours.Add(grid.GetNode(neighboursPosition));
                }
            }
            return neighbours;
        }
        private static Dictionary<Direction, Position> NeighbourLocations = new Dictionary<Direction, Position>() {
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


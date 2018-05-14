using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AStarPathFinding {
    /// <summary>
    /// Info: https://en.wikipedia.org/wiki/A*_search_algorithm
    /// </summary>
    public static class AStar {
        public static IEnumerable<Node> FindPath(NodesGrid grid, Node startNode, Node targetNode) {
            if (!startNode.Walkable || !targetNode.Walkable) {
                return null;
            }
            grid.ClearScore();

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Any()) {
                Node currentNode = openSet.Min();

                if (currentNode == targetNode) {
                    return ReconstructPath(startNode, targetNode);
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                // znajdź wszystkich sąsiadów obecnego węzła, który jest przechodni i nie był wcześniej odwiedzany
                foreach (Node neighbour in GetNeighbours(grid, currentNode).Where(it => it.Walkable && !closedSet.Contains(it))) {

                    int costToNeighbour = currentNode.G_Score + neighbour.Cost + GetDistance(currentNode.X, currentNode.Y, neighbour.X, neighbour.Y);
                    if (costToNeighbour < neighbour.G_Score || !openSet.Contains(neighbour)) {
                        neighbour.G_Score = costToNeighbour;
                        neighbour.H_Score = GetDistance(neighbour.X, neighbour.Y, targetNode.X, targetNode.Y);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour)) {
                            // Tutaj sprawdzam kąt
                            Debug.Log(currentNode.X + " " + currentNode.Y + " " + neighbour.X + " " + neighbour.Y + " " + Vector2.Angle(new Vector2(currentNode.X, currentNode.Y), new Vector2(neighbour.X, neighbour.Y)));
                            openSet.Add(neighbour);
                        }

                    }
                }
            }
            return null;
        }

        private static IEnumerable<Node> GetNeighbours(NodesGrid grid, Node node) {
            List<Node> neighbours = new List<Node>();
            for (int x = -1 ; x <= 1 ; x++) {
                for (int y = -1 ; y <= 1 ; y++) {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.X + x;
                    int checkY = node.Y + y;
                    if (grid.IsInsideGrid(checkX, checkY)) {
                        neighbours.Add(grid.GetNode(checkX, checkY));
                    }
                }
            }

            return neighbours;
        }

        private static IEnumerable<Node> ReconstructPath(Node startNode, Node targetNode) {
            List<Node> path = new List<Node>();
            Node currentNode = targetNode;
            while (currentNode != startNode) {
                currentNode.IsCorrectPath = true;
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();
            return path;
        }

        public static int GetDistance(int x1, int y1, int x2, int y2) {
            int dstX = Mathf.Abs(x1 - x2);
            int dstY = Mathf.Abs(y1 - y2);

            return (dstX > dstY) ? 14 * dstY + 10 * (dstX - dstY) : 14 * dstX + 10 * (dstY - dstX);
        }


        public class MoveState {
            public Node Node { get; set; }
            public Direction Direction { get; set; }
        }
        public enum Direction {
            N = 0, NE = 45, E = 90, SE = 135, S = 180, SW = 225, W = 270, NW = 315
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SearchAlgorithm {
    public static class AStar {
        // Info: https://en.wikipedia.org/wiki/A*_search_algorithm

        public static Queue<T> FindPath<T,Y>(Y grid, T startNode, T targetNode) where T : INode<T> where Y : IGrid<T> {
            grid.InitGrid();
            
            List<T> openSet = new List<T>();
            HashSet<T> closedSet = new HashSet<T>();
            openSet.Add(startNode);
            while (openSet.Any()) {
                T currentNode = openSet.Min();

                if (currentNode.Equals(targetNode)) {
                    return new Queue<T>(ReconstructPath(startNode, targetNode));
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                foreach (T neighbour in GetNeighbours(grid, currentNode)) {
                    if (!neighbour.Walkable || closedSet.Contains(neighbour)) {
                        continue;
                    }

                    int costToNeighbour = currentNode.G_Score + GetDistance(currentNode, neighbour);
                    if (costToNeighbour < neighbour.G_Score || !openSet.Contains(neighbour)) {
                        neighbour.G_Score = costToNeighbour;
                        neighbour.H_Score = GetDistance(neighbour, targetNode);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
            return null;
        }
        private static List<T> GetNeighbours<T>(IGrid<T> grid, T node) where T : INode<T> {
            List<T> neighbours = new List<T>();
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

        private static List<T> ReconstructPath<T>(T startNode, T targetNode) where T : INode<T> {
            List<T> path = new List<T>();
            T currentNode = targetNode;
            while (!currentNode.Equals(startNode)) {
                currentNode.IsCorrectPath = true;
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();
            return path;
        }

        private static int GetDistance<T>(T nodeA, T nodeB) where T : INode<T> {
            int dstX = Mathf.Abs(nodeA.X - nodeB.X);
            int dstY = Mathf.Abs(nodeA.Y - nodeB.Y);

            return (dstX > dstY) ? 14 * dstY + 10 * (dstX - dstY) : 14 * dstX + 10 * (dstY - dstX);
        }
    }


}


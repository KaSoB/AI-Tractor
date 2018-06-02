using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AStarPathFinding {
    public static class AStar {
        public static IEnumerable<AStarState> FindPath(NodesGrid grid, AStarState startNode, AStarState targetNode) {
            if (!startNode.Node.Walkable || !targetNode.Node.Walkable) {
                return new List<AStarState>();
            }
            grid.ClearScore();

            MinHeap<AStarState> openSet = new MinHeap<AStarState> {
                startNode
            };
            HashSet<AStarState> closedSet = new HashSet<AStarState>();

            while (openSet.Any()) {
                var currentNode = openSet.Min();

                if (currentNode.Node == targetNode.Node) {
                    return ReconstructPath(startNode, currentNode);
                }
                openSet.ExtractDominating();
                closedSet.Add(currentNode);
                foreach (AStarState successor in GetSuccessors(grid, currentNode).Where(it => it.Node.Walkable && !closedSet.Contains(it))) {

                    int costToNeighbour = currentNode.Node.G_Score + successor.Node.Cost + GetDistance(currentNode.Node.Position, successor.Node.Position);
                    if (costToNeighbour < successor.Node.G_Score || !openSet.Contains(successor)) {
                        successor.Node.G_Score = costToNeighbour;
                        successor.Node.H_Score = GetDistance(successor.Node.Position, targetNode.Node.Position);
                        successor.Node.Parent = currentNode.Node;


                        if (!openSet.Contains(successor)) {
                            openSet.Add(successor);
                        }
                    }
                }
            }
            return new List<AStarState>();
        }

        private static IEnumerable<AStarState> ReconstructPath(AStarState startNode, AStarState targetNode) {
            List<AStarState> path = new List<AStarState>();
            AStarState currentNode = targetNode;
            while (currentNode.Node != startNode.Node) {
                // Dodaj do ścieżki
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();
            return path;
        }

        private static IEnumerable<AStarState> GetSuccessors(NodesGrid grid, AStarState node) {
            List<AStarState> succesors = new List<AStarState>();

            foreach (Direction action in Enum.GetValues(typeof(Direction))) {
                var neighboursPosition = node.Node.Position + GlobalDirection.Locations[action];
                if (grid.IsInsideGrid(neighboursPosition)) {
                    succesors.Add(new AStarState() {
                        Node = grid.GetNode(neighboursPosition),
                        Action = action,
                        Parent = node
                    });
                }
            }
            return succesors;
        }



        public static int GetDistance(Position position1, Position position2) {
            int dstX = Mathf.Abs(position1.X - position2.X);
            int dstY = Mathf.Abs(position1.Y - position2.Y);

            return (dstX > dstY) ? 14 * dstY + 10 * (dstX - dstY) : 14 * dstX + 10 * (dstY - dstX);
        }
    }



}


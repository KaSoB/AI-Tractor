using AStarPathFinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodesGrid : MonoBehaviour {
    private Node[,] nodes;
    public int Width { get; set; }
    public int Height { get; set; }

    void Start() {
        List<Node> tmpNodes = GameObject.FindGameObjectsWithTag("Node").Select(x => x.GetComponent<Node>()).ToList();
        Width = tmpNodes.Max(it => it.X) + 1;
        Height = tmpNodes.Max(it => it.Y) + 1;
        nodes = new Node[Width, Height];
        tmpNodes.ForEach(item => nodes[item.X, item.Y] = item);
    }

    public Queue<Node> GetPath(Vector3 startPosition, Vector3 target) {
        int xStart = (int) startPosition.x;
        int yStart = (int) startPosition.z;
        int xTarget = (int) target.x;
        int yTarget = (int) target.z;
        if (!IsInsideGrid(xStart,yStart) || !IsInsideGrid(xTarget,yTarget)) {
            return null;
        }
        Node startNode = nodes[xStart,yStart]; 
        Node targetNode = nodes[xTarget, yTarget];
        return new Queue<Node>(AStar.FindPath(this, startNode, targetNode));
    }

    public void ClearScore() {
        foreach (var item in nodes) {
            item.ClearScore();
        }
    }
    public bool IsInsideGrid(int x, int y) {
        return (x >= 0 && x < Width && y >= 0 && y < Height);
    }
    public bool IsWalkableNode(int x, int y) {
        return nodes[x, y].Walkable;
    }
    public Node GetNode(int x, int y) {
        return nodes[x, y];
    }
}

using AStarPathFinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodesGrid : MonoBehaviour, IGrid<Node> {
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
        Node startNode = nodes[(int) startPosition.x, (int) startPosition.z]; // may produce out of array
        Node targetNode = nodes[(int) target.x, (int) target.z]; // may produce out of array
        return AStar.FindPath(this, startNode, targetNode);
    }

    public void ClearScore() {
        for (int i = 0 ; i < Width ; i++) {
            for (int j = 0 ; j < Height ; j++) {
                nodes[i, j].ClearScore(); // TODO: Sprawdzic czy nie na odwrót i,j
            }
        }
    }
    public bool IsInsideGrid(int x, int y) {
        return (x >= 0 && x < Width && y >= 0 && y < Height);
    }
    public Node GetNode(int x, int y) {
        return nodes[x, y];
    }
}

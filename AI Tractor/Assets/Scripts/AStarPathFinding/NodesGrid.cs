using Assets.Scripts.AStarPathFinding;
using AStarPathFinding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodesGrid : MonoBehaviour {
    private Node[,] nodes;
    public int Width { get; set; }
    public int Height { get; set; }
    private MoveState[,] moveNodes; // TODO: do pliku Astar
    void Start() {
        List<Node> tmpNodes = GameObject.FindGameObjectsWithTag("Node").Select(x => x.GetComponent<Node>()).ToList();
        Width = tmpNodes.Max(it => it.Position.X) + 1;
        Height = tmpNodes.Max(it => it.Position.Y) + 1;
        nodes = new Node[Width, Height];
        moveNodes = new MoveState[Width, Height];
        tmpNodes.ForEach(item => nodes[item.Position.X, item.Position.Y] = item);
        tmpNodes.ForEach(item => moveNodes[item.Position.X, item.Position.Y] = new MoveState() { Node = item, Direction = Direction.Undefined });
    }

    public Queue<Node> GetPath(Vector3 startPosition, Vector3 target) {
        int xStart = (int) startPosition.x;
        int yStart = (int) startPosition.z;
        int xTarget = (int) target.x;
        int yTarget = (int) target.z;
        if (!IsInsideGrid(xStart, yStart) || !IsInsideGrid(xTarget, yTarget)) {
            return null;
        }
        Node startNode = nodes[xStart, yStart];
        Node targetNode = nodes[xTarget, yTarget];

        var s = moveNodes[xStart, yStart];
        s.Node = startNode;
        var t = moveNodes[xTarget, yTarget];
        t.Node = targetNode;
        var path = AStar.FindPath(this, s, t);
        foreach (var item in path) {
            Debug.Log(item.Node.Position.ToString() + " " + item.Direction);
        }
        return new Queue<Node>(MoveState.ToNodes(path));
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
    public MoveState GetMoveState(int x, int y) {
        return moveNodes[x, y];
    }
}

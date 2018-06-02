using AStarPathFinding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodesGrid : MonoBehaviour {
    private Node[,] nodes;
    public int Width { get; set; }
    public int Height { get; set; }

    void Start() {
        List<Node> tmpNodes = GameObject.FindGameObjectsWithTag("Node").Select(x => x.GetComponent<Node>()).ToList();
        Width = tmpNodes.Max(it => it.Position.X) + 1;
        Height = tmpNodes.Max(it => it.Position.Y) + 1;
        nodes = new Node[Width, Height];
        tmpNodes.ForEach(item => nodes[item.Position.X, item.Position.Y] = item);
    }

    public Queue<AStarState> GetPath(Vector3 startPosition, Vector3 target, GameObject gameObject) {
        if (!IsInsideGrid(startPosition) || !IsInsideGrid(target)) {
            return null;
        }


        AStarState targetNode = new AStarState() {
            Node = GetNode(target)
        };

        AStarState startNode = new AStarState() {
            Node = GetNode(startPosition),
            Action = GlobalDirection.GetDirection(gameObject),
            Parent = targetNode
        };
        var path = AStar.FindPath(this, startNode, targetNode);


        foreach (var item in path) {
            Debug.Log("{" + item.Node.Position.ToString() + "}" + " " + item.Action);
        }
        return new Queue<AStarState>(path);
    }

    public void ClearScore() {
        foreach (var item in nodes) {
            item.ClearScore();
        }
    }

    public bool IsInsideGrid(Position position) {
        return (position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height);
    }

    public bool IsWalkableNode(Position position) {
        return nodes[position.X, position.Y].Walkable;
    }

    public Node GetNode(Position position) {
        return nodes[position.X, position.Y];
    }
}

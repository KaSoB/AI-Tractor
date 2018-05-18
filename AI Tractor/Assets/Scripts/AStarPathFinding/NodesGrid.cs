﻿using Assets.Scripts.AStarPathFinding;
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
        if (!IsInsideGrid(startPosition) || !IsInsideGrid(target)) {
            return null;
        }
        Node startNode = GetNode(startPosition);
        Node targetNode = GetNode(target);

        var s = GetMoveState(startPosition);
        s.Node = startNode;
        var t = GetMoveState(target);
        t.Node = targetNode;
        var path = AStar.FindPath(this, s, t);
        foreach (var item in path) {
            Debug.Log(item.Node.Position.ToString() + " " + item.Direction + " " + item.Action);
        }
        return new Queue<Node>(MoveState.ToNodes(path));
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
    public MoveState GetMoveState(Position position) {
        return moveNodes[position.X, position.Y];
    }
}

using Assets.Scripts.AStarPathFinding;
using System;
using UnityEditor;
using UnityEngine;

public class Node : MonoBehaviour, IComparable<Node> {
    [SerializeField]
    private bool walkable;
    public bool Walkable {
        get { return walkable; }
    }

    [SerializeField]
    [Range(0F, 50F)]
    private int cost;
    public int Cost {
        get { return cost; }
    }

    [SerializeField]
    private bool showInfoInEditMode;

    public bool IsCorrectPath { get; set; }

    public Position Position { get; set; }

    public Node Parent { get; set; }

    public int G_Score { get; set; }
    public int H_Score { get; set; }
    public int F_Score {
        get { return G_Score + H_Score; }
    }

    private void Awake() {
        Position = transform.position;

    }

    public void ClearScore() {
        G_Score = 0;
        H_Score = 0;
        IsCorrectPath = false;
        Parent = null;
    }

    private void OnDrawGizmos() {
        if (!showInfoInEditMode) {
            return;
        }

        var guiStyle = new GUIStyle() { fontSize = 12, fontStyle = FontStyle.Bold };
        guiStyle.normal.textColor = Walkable ? Color.green : Color.red;

        Handles.Label(transform.position + Vector3.up / 3, (Walkable ? string.Format("(C: {0} G: {1} H: {2})", Cost, G_Score, H_Score) : "F"), guiStyle);

        if (Parent != null) {
            Gizmos.color = IsCorrectPath ? Color.green : Color.white;
            Gizmos.DrawLine(transform.position, Parent.transform.position);
        }
    }

    public int CompareTo(Node other) {
        if (F_Score > other.F_Score) {
            return 1;
        } else if (F_Score == other.F_Score && H_Score > other.H_Score) {
            return 1;
        } else {
            return -1;
        }
    }
}

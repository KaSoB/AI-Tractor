using SearchAlgorithm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Node : MonoBehaviour, INode<Node> {
    [SerializeField]
    private bool walkable; 
    public bool Walkable {
        get { return walkable; }
    }

    public bool IsCorrectPath { get; set; }

    public int X { get; set; }
    public int Y { get; set; }


    public Node Parent { get; set; }

    public int G_Score { get; set; }
    public int H_Score { get; set; }
    public int F_Score {
        get { return G_Score + H_Score; }
    }

    private void Awake() {
        X = (int) transform.position.x;
        Y = (int) transform.position.z;  // w 3D component Z odpowiednikiem componentu Y w 2D
    }

    public void ClearScore() {
        G_Score = 0;
        H_Score = 0;
        IsCorrectPath = false;
        Parent = null;
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

    private void OnDrawGizmos() {
        var guiStyle = new GUIStyle() { fontSize = 18, fontStyle = FontStyle.Bold };
        guiStyle.normal.textColor = Walkable ? Color.green : Color.red;

        Handles.Label(transform.position + Vector3.up / 2, (Walkable ? string.Format("T (G: {0} H: {1})", G_Score, H_Score) : "F"), guiStyle);

        if (Parent != null) {
            Gizmos.color = IsCorrectPath ? Color.green : Color.white;
            Gizmos.DrawLine(transform.position, Parent.transform.position);
        }
    }

    public bool Equals(Node other) {
        if(other == null) {
            return false;
        }
        return X == other.X && Y == other.Y;
    }
}

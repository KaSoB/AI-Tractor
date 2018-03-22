using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Node : MonoBehaviour {
    [Range(0.0F, 1.0F)]
    // 0.0F => non-walkable Node.
    public float Cost = 0.5F;

    public int X { get; set; }
    public int Y { get; set; }

    private void Awake() {
        X = (int) transform.position.x;
        // w 3D component Z odpowiednikiem componentu Y w 2D
        Y = (int) transform.position.z;
    }

    private void OnDrawGizmos() {
        // Display Cost on Gizmos
        Handles.Label(transform.position + Vector3.up/2, Cost.ToString(), new GUIStyle() { fontSize = 20, fontStyle = FontStyle.Bold });
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = (Cost == PathFind.Node.NonWalkableCost) ? Color.red : Color.green;
        Gizmos.DrawSphere(transform.position, 0.25F);
    }
}

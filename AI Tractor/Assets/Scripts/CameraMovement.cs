using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField]
    private float cameraOffset;

    // TODO: set bounds on camera movement
    [SerializeField]
    private Vector3 boundsMax = new Vector3(0f, 3f, 1f);
    [SerializeField]
    private Vector3 boundsMin = new Vector3(-3f, 3f, -3f);
    void Update () {
        // Forward
        if (Input.GetKey(KeyCode.W)) {
            gameObject.transform.position += new Vector3(cameraOffset, 0, cameraOffset);
        }
        // Backward
        if (Input.GetKey(KeyCode.S)) {
            gameObject.transform.position += new Vector3(-cameraOffset, 0, -cameraOffset);
        }
        // Left
        if (Input.GetKey(KeyCode.A)) {
            gameObject.transform.position += new Vector3(-cameraOffset, 0, cameraOffset);
        }
        // Right
        if (Input.GetKey(KeyCode.D)) {
            gameObject.transform.position += new Vector3(cameraOffset, 0, -cameraOffset);
        }

    }
}

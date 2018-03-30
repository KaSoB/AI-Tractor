using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private float cameraOffset;

    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

    public float minZ;
    public float maxZ;
 
    void Update() {
        // Forward
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.position += new Vector3(cameraOffset, 0F, cameraOffset);
        }
        // Backward
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.position += new Vector3(-cameraOffset, 0F, -cameraOffset);
        }
        // Left
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += new Vector3(-cameraOffset, 0F, cameraOffset);
        }
        // Right
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position += new Vector3(cameraOffset, 0F, -cameraOffset);
        }
    }

    private void LateUpdate() {
        Vector3 newCameraPos = new Vector3 {
            x = Mathf.Clamp(transform.position.x, minX, maxX),
            y = Mathf.Clamp(transform.position.y, minY, maxY),
            z = Mathf.Clamp(transform.position.z, minZ, maxZ)
        };
        transform.position = newCameraPos;
    }
}

using UnityEngine;

public class CameraController : MonoBehaviour {
    public float cameraDeltaMove;

    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

    public float minZ;
    public float maxZ;

    void Update() {
        // Forward
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.position += new Vector3(cameraDeltaMove, 0F, cameraDeltaMove);
        }
        // Backward
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.position += new Vector3(-cameraDeltaMove, 0F, -cameraDeltaMove);
        }
        // Left
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += new Vector3(-cameraDeltaMove, 0F, cameraDeltaMove);
        }
        // Right
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position += new Vector3(cameraDeltaMove, 0F, -cameraDeltaMove);
        }
    }

    private void LateUpdate() {
        // Avoid leaving boundary
        transform.position = new Vector3 {
            x = Mathf.Clamp(transform.position.x, minX, maxX),
            y = Mathf.Clamp(transform.position.y, minY, maxY),
            z = Mathf.Clamp(transform.position.z, minZ, maxZ)
        };
    }
}

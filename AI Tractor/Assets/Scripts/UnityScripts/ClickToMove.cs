using UnityEngine;

public class ClickToMove : MonoBehaviour {
    void Update() {
        // Right click
        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                GetComponent<Agent>().GoTo(hit.point, Task.State.Start);
            }
        }
    }
}
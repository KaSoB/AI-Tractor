using UnityEngine;

public class ClickToMove : MonoBehaviour {
    private Agent agent;

    void Start() {
        agent = GetComponent<Agent>();
    }

    void Update() {
        // Right click

        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                agent.Goto(hit.point, Task.State.Start);
            }
        }
    }

}

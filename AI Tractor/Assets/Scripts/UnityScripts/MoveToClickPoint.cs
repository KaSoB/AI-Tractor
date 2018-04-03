using UnityEngine;

/// <summary>
/// Script Purpose: Test AI Component.
/// </summary>
public class MoveToClickPoint : MonoBehaviour {
    private AI agent;

    void Start() {
        agent = GetComponent<AI>();
    }

    void Update() {
        // Right click
        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                agent.TaskManager.AddTask(new TaskGoTo(hit.point));
            }
        }
    }

}

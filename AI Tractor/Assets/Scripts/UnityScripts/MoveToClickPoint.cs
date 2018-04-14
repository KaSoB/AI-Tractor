using UnityEngine;

/// <summary>
/// Script Purpose: Test AI Component.
/// </summary>
public class MoveToClickPoint : MonoBehaviour {
    private Agent agent;

    void Start() {
        agent = GetComponent<Agent>();
    }

    void Update() {
        // Right click
     
        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                agent.GoTo(hit.point);
            }
        }
    }

}

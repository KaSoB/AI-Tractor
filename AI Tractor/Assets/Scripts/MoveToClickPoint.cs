using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// Script Purpose: Test AI Component.
/// </summary>
public class MoveToClickPoint : MonoBehaviour {
    private AI agentComponent;

    void Start() {
        agentComponent = GetComponent<AI>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                agentComponent.GoTo(hit.point);
            }
        }
    }

}

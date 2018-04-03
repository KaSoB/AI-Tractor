using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Patrol : StateMachineBehaviour {
    [SerializeField]
    private List<GameObject> waypoints;
    private int currentWaypointIndex;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        currentWaypointIndex = 0;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!waypoints.Any()) {
            return;
        }


    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }

}

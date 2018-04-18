using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Patrol : StateMachineBaseBehaviour {
    private List<Vector3> waypoints;
    private int currentWaypointIndex;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint").Select(y => y.transform.localPosition).ToList();
        hasFinished = false;

        currentWaypointIndex = animator.GetInteger("CurrentWaypointIndex");
        agent.GoTo(waypoints[currentWaypointIndex]);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (hasFinished) {
            return;
        }

        if (!waypoints.Any()) {
            return;
        }

        if (agent.IsReachedTarget()) {
            hasFinished = true;
            animator.SetBool("Scan", true);
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        animator.SetInteger("CurrentWaypointIndex", currentWaypointIndex);
    }

}

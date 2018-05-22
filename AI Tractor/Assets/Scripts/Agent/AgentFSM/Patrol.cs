using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Patrol : StateMachineBaseBehaviour {
    private List<Vector3> waypoints;
    private int currentWaypointIndex;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint").Select(y => y.transform.localPosition).ToList();

        currentWaypointIndex = animator.GetInteger("CurrentWaypointIndex");
        agent.Goto(waypoints[currentWaypointIndex], Task.State.Start);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (agent.TaskManager.HasFinished()) {
            animator.SetBool("Scan", true);
            return;
        }

        if (!waypoints.Any()) {
            return;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        animator.SetInteger("CurrentWaypointIndex", currentWaypointIndex);
    }

}

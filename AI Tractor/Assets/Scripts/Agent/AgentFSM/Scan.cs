using System.Linq;
using UnityEngine;

public class Scan : StateMachineBaseBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        hasFinished = false;
        agent.sScan(1, Task.State.Start);
    }
    int i = 0;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (hasFinished) {
            return;
        }
        if (!agent.TaskManager.HasFinished()) {
            return;
        }

        TaskScan taskScan = agent.TaskManager.CurrentTask as TaskScan;
        var detected = taskScan.FarmFields;
        if (detected.Any()) {
            animator.SetBool("Harvest", true);
        } else {
            animator.SetBool("Scan", false);
        }
        hasFinished = true;
        Debug.Log("ile razy: " + i++);

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);

    }
}

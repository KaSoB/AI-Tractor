using System.Linq;
using UnityEngine;

public class Scan : StateMachineBaseBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        hasFinished = false;
        agent.Scan(1, Task.State.Start);
    }
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
            animator.SetBool("MakeDecision", true);
        } else {
            animator.SetBool("Scan", false);
        }
        hasFinished = true;

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);

    }
}

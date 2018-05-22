using UnityEngine;

public class Harvest : StateMachineBaseBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        hasFinished = false;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (hasFinished) {
            return;
        }
        animator.SetBool("Harvest", false);
        animator.SetBool("Scan", false);
        hasFinished = true;

        //if (agent.hasObject) {
        //    agent.obj.Harvest();
        //    agent.hasObject = false;
        //    animator.SetBool("Harvest", false);
        //    animator.SetBool("Scan", false);
        //}

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}

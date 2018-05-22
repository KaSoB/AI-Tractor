using UnityEngine;

public class Idle : StateMachineBaseBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (Vector3.Distance(agent.transform.position, agent.StartPosition) > 0.5F) {
            animator.SetBool("Return", true);
            return;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }

}

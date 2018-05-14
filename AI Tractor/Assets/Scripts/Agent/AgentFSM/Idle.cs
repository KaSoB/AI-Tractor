using UnityEngine;

public class Idle : StateMachineBaseBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (Vector3.Distance(subjectGameObject.transform.position, agent.startPosition) > 0.5F) {
            animator.SetBool("Return", true);
            return;
        }
    }

}

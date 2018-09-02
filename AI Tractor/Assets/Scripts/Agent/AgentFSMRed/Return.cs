using UnityEngine;

namespace RedAgent {
	public class Return : StateMachineBaseBehaviour {

	    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateEnter(animator, stateInfo, layerIndex);
	        agent.BackToStartPosition();
	    }

	    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        if (agent.TaskManager.HasFinished()) {
	            animator.SetBool("Return", false);
	            return;
	        }
	    }
	}
}
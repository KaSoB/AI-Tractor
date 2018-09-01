using UnityEngine;

namespace RedAgent {
	public class ModifyProperty : StateMachineBaseBehaviour {
	    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateEnter(animator, stateInfo, layerIndex);
	    }

	    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateUpdate(animator, stateInfo, layerIndex);
	        if (agent.TaskManager.HasFinished()) {
	            animator.SetBool("ModifyProperty", false);
	            return;
	        }
	    }

	    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateExit(animator, stateInfo, layerIndex);
	    }
	}
}
using UnityEngine;

namespace RedAgent {
	public class GoToFarmField : StateMachineBaseBehaviour {
	    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateEnter(animator, stateInfo, layerIndex);
	        hasFinished = false;
	        agent.GoTo(MakeDecision.FarmField.transform.position);
	    }

	    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateUpdate(animator, stateInfo, layerIndex);
	        if (agent.TaskManager.HasFinished()) {
	            animator.SetBool("DoFarmAction", true);
	            return;
	        }
	    }

	    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateExit(animator, stateInfo, layerIndex);
	    }
	}
}
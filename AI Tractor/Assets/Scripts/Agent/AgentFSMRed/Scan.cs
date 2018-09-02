using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RedAgent {
	public class Scan : StateMachineBaseBehaviour {
	    public static List<FarmField> farmFields;
	    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateEnter(animator, stateInfo, layerIndex);
	        hasFinished = false;
	        farmFields = new List<FarmField>();
	        agent.Scan(1);
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
	        farmFields = taskScan.FarmFields;
	        if (farmFields.Any()) {
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
}
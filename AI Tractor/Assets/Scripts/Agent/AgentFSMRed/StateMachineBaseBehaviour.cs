using UnityEngine;

namespace RedAgent {
	abstract public class StateMachineBaseBehaviour : StateMachineBehaviour {
	    protected GameObject subjectGameObject;
	    protected Equipment equipment;
	    protected Agent agent;
	    protected bool hasFinished;
	    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
	        subjectGameObject = animator.gameObject;
	        agent = subjectGameObject.GetComponent<Agent>();
	        equipment = agent.GetComponent<Equipment>();
	    }

	}
}
using UnityEngine;

abstract public class StateMachineBaseBehaviour : StateMachineBehaviour {
    protected GameObject subjectGameObject;
    protected Agent agent;
    protected bool hasFinished;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        subjectGameObject = animator.gameObject;
        agent = subjectGameObject.GetComponent<Agent>();
    }

}


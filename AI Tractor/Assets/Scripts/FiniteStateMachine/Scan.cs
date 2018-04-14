using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : FSMBaseBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        hasFinished = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (hasFinished) {
            return;
        }

        agent.Scan();
        hasFinished = true;
        if (agent.findobje) {
            animator.SetBool("Harvest", true);
        } else {
            animator.SetBool("Scan", false);
        }
       
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
       
    }
}

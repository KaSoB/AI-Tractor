﻿using UnityEngine;

public class Return : StateMachineBaseBehaviour {
    [SerializeField]
    private Vector3 stopPosition;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.GoTo(stopPosition);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (Vector3.Distance(subjectGameObject.transform.position, agent.startPosition) <= 0.5F) {
            animator.SetBool("Return", false);
            return;
        }
    }
}

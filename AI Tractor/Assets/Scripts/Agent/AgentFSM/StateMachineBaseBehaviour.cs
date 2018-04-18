using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

abstract public class StateMachineBaseBehaviour : StateMachineBehaviour {
    protected GameObject tractorGameObject;
    protected Agent agent;
    protected bool hasFinished;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        tractorGameObject = animator.gameObject;
        agent = tractorGameObject.GetComponent<Agent>();
    }

}


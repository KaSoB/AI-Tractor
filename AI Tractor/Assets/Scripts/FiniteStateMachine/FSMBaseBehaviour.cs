using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// FSM - Finite State Machine
/// </summary>
abstract public class FSMBaseBehaviour : StateMachineBehaviour {
    protected GameObject tractorGameObject;
    protected Agent agent;
    protected NavMeshAgent navMeshAgent;
    protected bool hasFinished;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        tractorGameObject = animator.gameObject;
        navMeshAgent = tractorGameObject.GetComponent<NavMeshAgent>();
        agent = tractorGameObject.GetComponent<Agent>();
    }

}


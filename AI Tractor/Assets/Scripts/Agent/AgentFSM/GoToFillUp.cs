using UnityEngine;

public class GoToFillUp : StateMachineBaseBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        hasFinished = false;
        Equipment equipment = agent.GetComponent<Equipment>();

        if (equipment.GetResourceLevel(ResourceType.Fertilizer) <= 1) {
            agent.GoToFillUpResource(ResourceType.Fertilizer, Task.State.Start);
        } else if (equipment.GetResourceLevel(ResourceType.Pesticide) <= 1) {
            agent.GoToFillUpResource(ResourceType.Pesticide, Task.State.Start);
        } else if (equipment.GetResourceLevel(ResourceType.Water) <= 1) {
            agent.GoToFillUpResource(ResourceType.Water, Task.State.Start);
        } else if (equipment.GetResourceLevel(ResourceType.Soil) <= 1) {
            agent.GoToFillUpResource(ResourceType.Soil, Task.State.Start);
        } else {
            hasFinished = true;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (hasFinished) {
            animator.SetBool("GoTo", false);
            animator.SetBool("Harvest", false);
            animator.SetBool("MakeDecision", false);
            animator.SetBool("GoToFillUp", false);
            animator.SetBool("Scan", false);
            return;
        }

        if (agent.TaskManager.HasFinished()) {
            hasFinished = true;
            return;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}


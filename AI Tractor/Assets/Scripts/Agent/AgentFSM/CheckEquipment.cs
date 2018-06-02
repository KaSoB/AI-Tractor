using UnityEngine;

public class CheckEquipment : StateMachineBaseBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        hasFinished = false;

        if (equipment.GetResourceLevel(ResourceType.FertilityRes) == 0) {
            agent.GoToFillUpResource(ResourceType.FertilityRes);
            animator.SetBool("GoToFillUp", true);
        } else if (equipment.GetResourceLevel(ResourceType.PollutionRes) == 0) {
            agent.GoToFillUpResource(ResourceType.PollutionRes);
            animator.SetBool("GoToFillUp", true);
        } else if (equipment.GetResourceLevel(ResourceType.HumidityRes) == 0) {
            agent.GoToFillUpResource(ResourceType.HumidityRes);
            animator.SetBool("GoToFillUp", true);
        } else if (equipment.GetResourceLevel(ResourceType.AcidityRes) == 0) {
            agent.GoToFillUpResource(ResourceType.AcidityRes);
            animator.SetBool("GoToFillUp", true);
        } else {
            hasFinished = true;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (hasFinished) {
            animator.SetBool("GoToFarmField", false);
            animator.SetBool("Harvest", false);
            animator.SetBool("ModifyProperty", false);
            animator.SetBool("MakeDecision", false);
            animator.SetBool("DoFarmAction", false);
            animator.SetBool("GoToFillUp", false);
            animator.SetBool("CheckEquipment", false);
            animator.SetBool("Scan", false);
            return;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}

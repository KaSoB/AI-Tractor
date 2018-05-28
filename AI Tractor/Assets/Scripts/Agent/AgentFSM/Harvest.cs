
using UnityEngine;

public class Harvest : StateMachineBaseBehaviour {
    FarmField farmField;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        farmField = MakeDecision.FarmField;
        hasFinished = false;

        if (farmField.Progress == 1) {
            agent.Harvest(farmField, Task.State.Start);
        } else if (farmField.GetLevel(Property.Type.Humidity) == 1) {
            agent.DoFarmAction(farmField, Property.Type.Humidity, ResourceType.Water, 1, Task.State.Start);
        } else if (farmField.GetLevel(Property.Type.Fertylity) <= 2) {
            agent.DoFarmAction(farmField, Property.Type.Fertylity, ResourceType.Fertilizer, 1, Task.State.Start);
        } else if (farmField.GetLevel(Property.Type.Acidity) <= 4) {
            agent.DoFarmAction(farmField, Property.Type.Acidity, ResourceType.Soil, 1, Task.State.Start);
        } else {
            hasFinished = true;
        }

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (hasFinished) {
            animator.SetBool("GoToFillUp", true);
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


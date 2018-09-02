using UnityEngine;

namespace RedAgent {
	public class DoFarmAction : StateMachineBaseBehaviour {
	    FarmField farmField;
	    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateEnter(animator, stateInfo, layerIndex);
	        farmField = MakeDecision.FarmField;
	        hasFinished = false;

	        if (farmField.Progress == 1) {
	            animator.SetBool("Harvest", true);
	            agent.Harvest(farmField, Task.State.Start);
	        } else if (farmField.GetLevel(Property.Type.Humidity) == 1 && equipment.HasResources(ResourceType.HumidityRes)) {
	            animator.SetBool("ModifyProperty", true);
	            agent.DoFarmAction(farmField, Property.Type.Humidity, ResourceType.HumidityRes, 1);
	        } else if (farmField.GetLevel(Property.Type.Fertylity) <= 2 && equipment.HasResources(ResourceType.FertilityRes)) {
	            animator.SetBool("ModifyProperty", true);
	            agent.DoFarmAction(farmField, Property.Type.Fertylity, ResourceType.FertilityRes, 1);
	        } else if (farmField.GetLevel(Property.Type.Acidity) == 1 && equipment.HasResources(ResourceType.AcidityRes)) {
	            animator.SetBool("ModifyProperty", true);
	            agent.DoFarmAction(farmField, Property.Type.Acidity, ResourceType.AcidityRes, 1);
	        } else if (farmField.GetLevel(Property.Type.Pollution) >= 4 && equipment.HasResources(ResourceType.PollutionRes)) {
	            animator.SetBool("ModifyProperty", true);
	            agent.DoFarmAction(farmField, Property.Type.Pollution, ResourceType.PollutionRes, -1);
	        } else {
	            hasFinished = true;
	        }

	    }

	    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateUpdate(animator, stateInfo, layerIndex);
	        if (hasFinished) {
	            animator.SetBool("CheckEquipment", true);
	        }
	    }

	    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateExit(animator, stateInfo, layerIndex);
	    }
	}
}
using DecisionTree;
using System.Collections.Generic;
using UnityEngine;

namespace RedAgent {
	public class MakeDecision : StateMachineBaseBehaviour {
	    public static FarmField FarmField;
	    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateEnter(animator, stateInfo, layerIndex);
	        FarmField = null;
	        hasFinished = false;
	    }

	    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateUpdate(animator, stateInfo, layerIndex);
	        if (hasFinished) {
	            return;
	        }
	        hasFinished = true;

	        foreach (var farmField in Scan.farmFields) {
	            var decisionTree = DecisionTreeRunner.Instance.GetDecision(CreateInfo(farmField));
	            Debug.Log($"DecisionTree: {decisionTree}: {farmField.name}");
	            if (decisionTree) {
	                FarmField = farmField;
	                break;
	            }
	        }

	        if (FarmField != null) {
	            animator.SetBool("GoToFarmField", true);
	        } else {
	            animator.SetBool("MakeDecision", false);
	            animator.SetBool("Scan", false);
	        }
	    }

	    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	        base.OnStateExit(animator, stateInfo, layerIndex);
	    }

	    public Dictionary<string, string> CreateInfo(FarmField farmField) {
	        var isWindy = FindObjectOfType<GameSimulator>().IsWindy ? "yes" : "no";
	        var season = FindObjectOfType<GameSimulator>().CurrentSeason;
	        var fieldtype = farmField.GetFieldType();
	        var completed = (farmField.Progress == 1.0F).ToString().ToLower()[0];
	        var humidityLevel = farmField.GetProperty(Property.Type.Humidity).Level <= 1 ? "1" : "2-5";
	        var fertylityLevel = farmField.GetProperty(Property.Type.Fertylity).Level <= 2 ? "1-2" : "3-5";
	        var acidityLevel = farmField.GetProperty(Property.Type.Acidity).Level <= 4 ? "1-4" : "5";
	        var pollutionLevel = farmField.GetProperty(Property.Type.Pollution).Level <= 3 ? "1-3" : "4-5";
	        var result = new Dictionary<string, string> {
	            { "FarmFieldType", fieldtype.ToString() },
	            { "Completed", completed.ToString() },
	            { "Humidity", humidityLevel },
	            { "Fertylity", fertylityLevel },
	            { "Acidity", acidityLevel },
	            { "Pollution", pollutionLevel },
	            { "Season", season.ToString() },
	            { "IsWindy", isWindy.ToString() }
	        };
	        return result;

	    }

	}
}
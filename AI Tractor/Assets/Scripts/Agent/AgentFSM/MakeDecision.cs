using System.Collections.Generic;
using UnityEngine;

public class MakeDecision : StateMachineBaseBehaviour {
    List<FarmField> farmFields = new List<FarmField>();

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
        TaskScan taskScan = agent.TaskManager.CurrentTask as TaskScan;
        farmFields = taskScan.FarmFields;


        DTRunner.Run();


        LoadInfo();
        if (FarmField != null) {
            animator.SetBool("GoTo", true);
        } else {
            animator.SetBool("MakeDecision", false);
            animator.SetBool("Scan", false);
        }


        hasFinished = true;

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }

    public void LoadInfo() {
        foreach (var farmField in farmFields) {
            var description = CreateLog(farmField);
            if (DTRunner.Check(description) == true) {
                FarmField = farmField;
                return;
            } else {
                Debug.Log("Zwracam false dla " + farmField.name);
            }
        }
    }
    public Dictionary<string, string> CreateLog(FarmField farmField) {
        var isWindy = GameObject.FindObjectOfType<GameSimulator>().IsWindy ? "yes" : "no";
        var season = GameObject.FindObjectOfType<GameSimulator>().CurrentSeason;
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

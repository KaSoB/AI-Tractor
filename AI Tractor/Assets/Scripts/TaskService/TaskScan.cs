using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskScan : Task {
    public List<FarmField> FarmField { get; set; }
    public int Radius { get; set; }

    public void Init_Enter() {
        FarmField = new List<FarmField>();
    }

    public void Start_Update() {
        FSM.ChangeState(State.Execute);
    }

    public void Execute_Enter() {
        Debug.Log("Execute_Enter");
        var detectedFarmFields =
            Physics.OverlapSphere(Subject.transform.position, Radius)
             .Where(y => y.tag == "FarmField")
             .Select(it => it.gameObject.GetComponent<FarmField>());

        FarmField.AddRange(detectedFarmFields);
        FSM.ChangeState(State.Finish);
    }

    public void Finish_Enter() {
        Debug.Log("Znalazłem...");
        foreach (var item in FarmField) {
            Debug.Log(item.name);
        }
    }
}
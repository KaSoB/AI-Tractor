using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskScan : Task {
    public List<FarmField> FarmFields { get; set; }
    public int Radius { get; set; }

    public void Init_Enter() {
    }

    public void Start_Update() {
        FSM.ChangeState(State.Execute);
    }

    public void Execute_Enter() {
        var detectedFarmFields =
            Physics.OverlapSphere(Subject.transform.position, Radius)
             .Where(y => y.tag == "FarmField")
             .Select(it => it.gameObject.GetComponent<FarmField>());
        if (detectedFarmFields != null) {
            FarmFields = detectedFarmFields.ToList();
        }

        FSM.ChangeState(State.Finish);
    }

    public void Finish_Enter() {
        Debug.Log("Znalazłem...");
        foreach (var item in FarmFields) {
            Debug.Log(item.name);
        }
    }
}
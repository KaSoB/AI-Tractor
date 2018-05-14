using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskScan : Task {
    public List<FarmField> FarmField { get; set; }
    public int Radius { get; set; }

    public TaskScan(GameObject subject, object goal, int radius) : base(subject, goal) {
        Radius = radius;
        fsm = StateMachine<State>.Initialize(this);
    }
    protected override void Init_Enter() {
        Debug.Log("Init_Enter");
    }
    protected override void Init_Update() {
        Debug.Log("Init_Update");
        fsm.ChangeState(State.Execute);
    }
    protected override void Execute_Enter() {
        Debug.Log("Execute_Enter");
        var detectedFarmFields =
            Physics.OverlapSphere(subject.transform.position, Radius)
             .Where(y => y.tag == "FarmField")
             .Select(it => it.gameObject.GetComponent<FarmField>());

        FarmField.AddRange(detectedFarmFields);
        fsm.ChangeState(State.Finish);
    }
}
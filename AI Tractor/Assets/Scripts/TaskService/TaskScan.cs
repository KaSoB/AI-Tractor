using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskScan : Task {
    List<FarmField> farmFields = new List<FarmField>();

    public TaskScan(object goal) : base(goal) { }

    private bool IsReachedGoal() {
        return Vector3.Distance(subject.transform.position, (Vector3) goal) < 0.1F;
    }

    public override void Execute(GameObject subject) {
        var items = Physics.OverlapSphere(subject.transform.position, (float) goal).Where(y => y.tag == "FarmField").Select(it => it.gameObject.GetComponent<FarmField>());
        farmFields.AddRange(items);
    }

    public override void Update() {
        throw new NotImplementedException();
    }


}
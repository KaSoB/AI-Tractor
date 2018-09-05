using UnityEngine;
using System.Collections.Generic;

public class TaskHarvest : Task {
    public FarmField FarmField { get; set; }
    private static readonly float MinimumDistanceToSucceed = 1F;

    public void Start_Update() {
        List<FarmField> farms = new List<FarmField>();
        GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();
        farms=geneticAlgorithm.doAlgorithm();
        bool harvest = false;
        foreach(FarmField farmField in farms){
            if (Vector3.Distance(Subject.transform.position, farmField.transform.position) < MinimumDistanceToSucceed)
                harvest = true;
        }
        if (harvest==true) {
            FSM.ChangeState(State.Execute);
        } else {
            ErrorMessage = "Nie zbieram żniw z tego pola.";
            FSM.ChangeState(State.Interrupt);
        }
    }

    public void Execute_Update() {
        FarmField.Progress = 0;
        FSM.ChangeState(State.Finish);
    }

    public void Interrupt_Enter() {
        Debug.Log(ErrorMessage);
    }
    public void Finish_Enter() {
        Debug.Log("Zebrałem żniwa!");
    }
}


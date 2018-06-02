using UnityEngine;

public class TaskHarvest : Task {
    public FarmField FarmField { get; set; }
    private static readonly float MinimumDistanceToSucceed = 1F;

    public void Start_Update() {
        if (Vector3.Distance(Subject.transform.position, FarmField.transform.position) < MinimumDistanceToSucceed) {
            FSM.ChangeState(State.Execute);
        } else {
            ErrorMessage = "Nie udało się. Jestem za daleko od pola.";
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


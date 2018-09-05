
using UnityEngine;

public class TaskFarmAction : Task {
    public FarmField FarmField { get; set; }
    public Property.Type PropertyType { get; set; }
    public Equipment Equipment { get; set; }
    public ResourceType ResourceType { get; set; }

    public int Points { get; set; }

    private static readonly float MinimumDistanceToSucceed = 1F;
    public void Start_Update() {
        if (Vector3.Distance(Subject.transform.position, FarmField.transform.position) < MinimumDistanceToSucceed) {
            FSM.ChangeState(State.Execute);
        } else {
            ErrorMessage = "Nie udało się. Jestem za daleko od pola.";
            FSM.ChangeState(State.Interrupt);
        }// TODO: więcej warunków
    }

    public void Execute_Update() {
        if (Points > 0) {
            // pobierz z agenta points, dodaj polu points
            Equipment.RemovePoints(ResourceType, Points);
            FarmField.AddLevel(PropertyType, Points);
        } else {
            Equipment.RemovePoints(ResourceType, -Points); // jest dobrze 
            FarmField.DeleteLevel(PropertyType, -Points);
        }
        FSM.ChangeState(State.Finish);
    }

    public void Interrupt_Enter() {
        Debug.Log(ErrorMessage);
    }

    public void Finish_Enter() {
        Debug.Log("Wykonana akcja na {FarmField.name}: {ResourceType} {Points}");
    }
}


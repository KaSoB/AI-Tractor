
using UnityEngine;

public class TaskFillUpResource : Task {
    public Equipment Equipment { get; set; }
    public ResourceType ResourceType { get; set; }

    private static readonly float MinimumDistanceToSucceed = 1F;
    public void Start_Update() {
        if (Vector3.Distance(Subject.transform.position, Locations.locations[ResourceType]) < MinimumDistanceToSucceed) {
            FSM.ChangeState(State.Execute);
        } else {
            ErrorMessage = "Nie udało się. Jestem za daleko od lokacji.";
            FSM.ChangeState(State.Interrupt);
        }
    }

    public void Execute_Update() {
        Equipment.SetResourceLevel(ResourceType, Resource.MAX_LEVEL);
        FSM.ChangeState(State.Finish);
    }

    public void Interrupt_Enter() {
        Debug.Log(ErrorMessage);
    }

    public void Finish_Enter() {
        Debug.Log($"Uzupełniłem zasoby: {ResourceType}");
    }
}


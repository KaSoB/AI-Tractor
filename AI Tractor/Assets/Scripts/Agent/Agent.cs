using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour {
    public Vector3 StartPosition { get; private set; }
    public TaskManager TaskManager { get; private set; }

    private void Start() {
        StartPosition = transform.position;
        TaskManager = new TaskManager();

    }

    public void GoTo(Vector3 destination, Task.State state = Task.State.Start) {
        var task = GetComponent<TaskGoTo>();
        task.NavMeshAgent = GetComponent<NavMeshAgent>();
        task.Destination = new Vector3((int) destination.x, 0, (int) destination.z);
        TaskManager.SetTask(task, state);
    }

    public void BackToStartPosition() {
        GoTo(StartPosition, Task.State.Start);
    }

    public void Scan(int radius, Task.State state = Task.State.Start) {
        var task = GetComponent<TaskScan>();
        task.Radius = radius;
        TaskManager.SetTask(task, state);
    }

    public void DoFarmAction(FarmField farmField, Property.Type propertyType, ResourceType resourceType, int points = 1, Task.State state = Task.State.Start) {
        var task = GetComponent<TaskFarmAction>();
        task.FarmField = farmField;
        task.Equipment = GetComponent<Equipment>();
        task.PropertyType = propertyType;
        task.ResourceType = resourceType;
        task.Points = points;

        TaskManager.SetTask(task, state);
    }
    public void FillUpResource(ResourceType resourceType, Task.State state = Task.State.Start) {
        var task = GetComponent<TaskFillUpResource>();
        task.Equipment = GetComponent<Equipment>();
        task.ResourceType = resourceType;
        TaskManager.SetTask(task, state);
    }
    public void GoToFillUpResource(ResourceType resourceType, Task.State state = Task.State.Start) {
        var task = GetComponent<TaskGotoAndFillUpResource>();
        task.Equipment = GetComponent<Equipment>();
        task.NavMeshAgent = GetComponent<NavMeshAgent>();
        task.ResourceType = resourceType;

        TaskManager.SetTask(task, state);
    }
    public void Harvest(FarmField farmField, Task.State state = Task.State.Start) {
        var task = GetComponent<TaskHarvest>();
        task.FarmField = farmField;

        TaskManager.SetTask(task, state);
    }


}



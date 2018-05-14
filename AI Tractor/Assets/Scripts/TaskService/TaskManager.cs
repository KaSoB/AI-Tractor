using System.Collections.Generic;

public class TaskManager {
    public Task CurrentTask { get; private set; }

    public enum TaskType {
        GoTo,
        Scan
    }
    private Dictionary<TaskType, Task> myTasks;

    public TaskManager() {
        //Tasks = new Task();
    }
    public void AddTask(TaskType taskType, Task task) {
        if (!myTasks.ContainsKey(taskType)) {
            myTasks.Add(taskType, task);
        }
    }
    public bool HasTask() {
        return true;//return Tasks.Any();
    }
    public void SetCurrentTask() {
        // CurrentTask = Tasks();
    }
}
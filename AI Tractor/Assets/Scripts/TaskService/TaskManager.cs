using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TaskManager {
    public Queue<Task> Tasks { get; private set; }
    public Task CurrentTask { get; private set; }

    public TaskManager() {
        Tasks = new Queue<Task>();
    }
    public void AddTask(Task task) {
        Tasks.Enqueue(task);
    }
    public bool HasTask() {
        return Tasks.Any();
    }
    public void GetNewTask() {
        CurrentTask = Tasks.Dequeue();
    }
}
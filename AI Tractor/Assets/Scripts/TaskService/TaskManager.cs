using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TaskManager {
    public enum PriorityLevel { High, Normal, Low }
    public List<KeyValuePair<PriorityLevel,Task>> Tasks { get; private set; }
    public Task CurrentTask { get; private set; }

    public TaskManager() {
        Tasks = new List<KeyValuePair<PriorityLevel,Task>>();
    }
    public void AddTask(Task task, PriorityLevel priorityLevel) {
        Tasks.Add(new KeyValuePair<PriorityLevel, Task>(priorityLevel,task));
    }
    public bool HasTask() {
        return Tasks.Any();
    }
    public void GetNewTask() {
       // CurrentTask = Tasks();
    }
}
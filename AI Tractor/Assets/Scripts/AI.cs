using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour, INetworkController {
    public TaskManager TaskManager { get; set; }


    private void Start() {
        TaskManager = new TaskManager();
    }

    private void Update() {
        //if ((TaskManager.CurrentTask == null || TaskManager.CurrentTask.IsCompleted) && TaskManager.HasTask()) {
        //    TaskManager.GetNewTask();
        //}

        //if (TaskManager.CurrentTask == null) {
        //    return;
        //}

        //if (TaskManager.CurrentTask.CheckConditionsToStartTask()) {
        //    TaskManager.CurrentTask.OnTaskStart();
        //} else {
        //    TaskManager.CurrentTask.IsFinished = true;
        //}

        //if (TaskManager.CurrentTask.CheckConditionsToFinishTask()) {
        //    TaskManager.CurrentTask.IsFinished = true;
        //    Debug.Log("Zadanie wykonane!");
        //} else {
        //    TaskManager.CurrentTask.OnTaskUpdate();
        //}

    }

    public string GetTextRaport() {
        int x = (int) transform.position.x;
        int y = (int) transform.position.z;

        return string.Format("{0} {1} {2}", name, x, y);
    }
}



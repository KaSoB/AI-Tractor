using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour, INetworkController {
    public TaskManager TaskManager { get; set; }
    private NavMeshAgent agent;

    private Queue<Node> path = new Queue<Node>();
    private Vector3 target;
    // TODO: usunąć
    public FarmField obje;
    public bool findobje = false;
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        TaskManager = new TaskManager();
        target = transform.position;
    }

    public void GoTo(Vector3 destination) {
        path.Clear();
        path = GameObject.FindGameObjectWithTag("Grid").GetComponent<NodesGrid>().GetPath(transform.position, destination);
        if (path == null) {
            Debug.Log("Nie znalazłem ścieżki do " + destination);
        }
    }
    private void Update() {
        if (path != null && IsReachedTarget() && path.Any()) {
            var point = path.Dequeue();
            target = point.transform.position;
            target = new Vector3(target.x, 0, target.z);
            agent.destination = target;
        }

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
    public bool IsReachedTarget() {
        return Vector3.Distance(transform.position, target) < 0.1F;
    }
    public void Scan() {
        foreach (var item in Physics.OverlapSphere(transform.position, 3F).Where(y=>y.tag=="FarmField").Select(it => it.gameObject.GetComponent<FarmField>())) {
            if (item.Progress == 1F) {
                obje = item;
                Debug.Log(":/");
                findobje = true;
                break;
            }

        }
    }
    public string GetTextRaport() {
        int x = (int) transform.position.x;
        int y = (int) transform.position.z;

        return string.Format("{0} {1} {2}", name, x, y);
    }
}



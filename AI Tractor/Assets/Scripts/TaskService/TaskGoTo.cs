using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class TaskGoTo : Task {
    public NavMeshAgent NavMeshAgent { get; set; }
    public Vector3 Destination { get; set; } // cel na mapie

    private Vector3 target; // aktualny cel agenta
    private Node currentTargetNode = null; // obecny node
    private Queue<Node> path = new Queue<Node>(); // sciezka uzyskana z Grid

    private bool IsReachedTarget() {
        return Vector3.Distance(Subject.transform.position, target) < 0.2F;
    }
    private bool IsReachedDestination() {
        return Vector3.Distance(Subject.transform.position, Destination) < 0.2F;
    }

    public void GoTo() {
        path = GameObject
            .FindGameObjectWithTag("Grid")
            .GetComponent<NodesGrid>()
            .GetPath(Subject.transform.position, Destination);

        if (path == null) {
            Debug.Log("Nie znalazłem ścieżki do " + Destination);
        }
    }

    public void Init_Enter() {
        path.Clear();

    }

    public void Start_Update() {
        GoTo();
        if (path.Any()) {
            currentTargetNode = path.Dequeue();
            var point = currentTargetNode.transform.position;
            target = new Vector3(point.x, 0, point.z);
            NavMeshAgent.destination = target;
            FSM.ChangeState(State.Execute);
        } else {
            FSM.ChangeState(State.Finish);
        }

    }

    public void Execute_Update() {
        if (path.Any() && IsReachedTarget()) {
            currentTargetNode = path.Dequeue();
            var point = currentTargetNode.transform.position;
            target = new Vector3(point.x, 0, point.z);
            NavMeshAgent.destination = target;
        } else if (!path.Any() && IsReachedDestination()) {
            FSM.ChangeState(State.Finish);
        }
    }


    public void Finish_Enter() {
        //  Debug.Log("Koniec drogi...");
    }
}


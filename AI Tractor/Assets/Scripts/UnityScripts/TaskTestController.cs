
using UnityEngine;
using UnityEngine.AI;

public class TaskTestController : MonoBehaviour {
    protected Agent agent;

    private void Start() {
        agent = GetComponent<Agent>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            agent.TaskManager.ChangeState(Task.State.Start);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            var task = GetComponent<TaskScan>();
            task.Subject = transform.gameObject;
            task.Radius = 5;
            agent.TaskManager.SetTask(task);
        }


        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            var task = GetComponent<TaskGoTo>();
            task.Subject = transform.gameObject;
            task.NavMeshAgent = task.Subject.GetComponent<NavMeshAgent>();
            task.Destination = new Vector3(6, 0, 0);

            agent.TaskManager.SetTask(task);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            var task = GetComponent<TaskGoTo>();
            task.Subject = transform.gameObject;
            task.NavMeshAgent = task.Subject.GetComponent<NavMeshAgent>();
            task.Destination = new Vector3(0, 0, 0);

            agent.TaskManager.SetTask(task);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            agent.TaskManager.SetTask(GetComponent<TaskTest>());
        }
    }
}


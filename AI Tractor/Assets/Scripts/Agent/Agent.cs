using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour {
    public Vector3 startPosition;
    public TaskManager TaskManager { get; private set; }
    // private NavMeshAgent agent;
    //private Queue<Node> path = new Queue<Node>();
    //private Vector3 target;
    // TODO: usunąć
    // public FarmField obj;
    // public bool hasObject = false;
    // --

    private void Start() {
        // agent = GetComponent<NavMeshAgent>();
        //  target = transform.position;
        startPosition = transform.position;
        TaskManager = new TaskManager();

    }

    private void Update() {
        //if (path != null && IsReachedTarget() && path.Any()) {
        //    var point = path.Dequeue();
        //    target = point.transform.position;
        //    target = new Vector3(target.x, 0, target.z);
        //    agent.destination = target;
        //}

    }

    //public bool IsReachedTarget() {
    //    return Vector3.Distance(transform.position, target) < 0.1F;
    //}

    //public void Scan() {
    //    foreach (var item in Physics.OverlapSphere(transform.position, 2F).Where(y => y.tag == "FarmField").Select(it => it.gameObject.GetComponent<FarmField>())) {
    //        if (item.Progress == 1F) {
    //            obj = item;
    //            hasObject = true;
    //            break;
    //        }

    //    }
    //}

    //public void GoTo(Vector3 destination) {
    //    path.Clear();
    //    path = GameObject.FindGameObjectWithTag("Grid").GetComponent<NodesGrid>().GetPath(transform.position, destination);
    //    if (path == null) {
    //        Debug.Log("Nie znalazłem ścieżki do " + destination);
    //    }
    //}

    public void sGoTo(Vector3 destination, Task.State state) {
        var task = GetComponent<TaskGoTo>();
        task.NavMeshAgent = GetComponent<NavMeshAgent>();
        task.Destination = new Vector3((int) destination.x, 0, (int) destination.z);
        task.Subject = gameObject;

        TaskManager.SetTask(task, state);
    }
    public void back() {
        sGoTo(startPosition, Task.State.Start);
    }
    public void sScan(int radius, Task.State state) {
        var task = GetComponent<TaskScan>();
        task.Radius = radius;
        task.Subject = gameObject;

        TaskManager.SetTask(task, state);
    }
    public void sTest() {
        var task = GetComponent<TaskTest>();
        task.Subject = gameObject;
        // ?
    }


}



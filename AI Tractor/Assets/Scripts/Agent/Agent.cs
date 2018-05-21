using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour, INetworkController {
    public Vector3 startPosition;
    private NavMeshAgent agent;
    private Queue<Node> path = new Queue<Node>();
    private Vector3 target;
    // TODO: usunąć
    public FarmField obj;
    public bool hasObject = false;
    // --

    public TaskManager TaskManager = new TaskManager();
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        target = transform.position;
        startPosition = transform.position;

    }


    private void Update() {
        if (path != null && IsReachedTarget() && path.Any()) {
            var point = path.Dequeue();
            target = point.transform.position;
            target = new Vector3(target.x, 0, target.z);
            agent.destination = target;
        }

    }

    public bool IsReachedTarget() {
        return Vector3.Distance(transform.position, target) < 0.1F;
    }

    public void Scan() {
        foreach (var item in Physics.OverlapSphere(transform.position, 2F).Where(y => y.tag == "FarmField").Select(it => it.gameObject.GetComponent<FarmField>())) {
            if (item.Progress == 1F) {
                obj = item;
                hasObject = true;
                break;
            }

        }
    }

    public void GoTo(Vector3 destination) {
        path.Clear();
        path = GameObject.FindGameObjectWithTag("Grid").GetComponent<NodesGrid>().GetPath(transform.position, destination);
        if (path == null) {
            Debug.Log("Nie znalazłem ścieżki do " + destination);
        }
    }

    public string GetTextRaport() {

        int x = (int) transform.position.x;
        int y = (int) transform.position.z;

        return string.Format("{0} {1} {2}", name, x, y);
    }
}



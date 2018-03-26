using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
    private NavMeshAgent agentComponent;
    
    // TODO: tu wszystko zostanie przebudowane. Sprawdzam A*

    public NodesGrid nodesGrid;


    private Queue<Node> path = new Queue<Node>();
    private Vector3 target = Vector3.zero;

    private void Start() {
        agentComponent = GetComponent<NavMeshAgent>();
    }

    public void GoTo(Vector3 destination) {
        path = nodesGrid.GetPath(transform.position, destination);
    }

    private void Update() {
        if (path != null && IsReachedTarget() && path.Any()) {
            var point = path.Dequeue();
            target = point.transform.position;
            target = new Vector3(target.x, 0, target.z);
            agentComponent.destination = target;
        }

    }

    private bool IsReachedTarget() {
        return Vector3.Distance(transform.position, target) < 0.1F;
    }


}



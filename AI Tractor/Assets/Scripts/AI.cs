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


    private Queue<PathFind.Point> path = new Queue<PathFind.Point>();
    private Vector3 target = Vector3.zero;

    private void Start() {
        agentComponent = GetComponent<NavMeshAgent>();
    }

    public void GoTo(Vector3 destination) {
        path.Clear();
        path = nodesGrid.GetPath(transform.position, destination);
    }

    private void Update() {
        if (IsReachedTarget() && path.Any()) {
            var point = path.Dequeue();
            target = new Vector3(point.x, 0, point.y);
            agentComponent.destination = target;
        }

    }

    private bool IsReachedTarget() {
        return Vector3.Distance(transform.position, target) < 0.1F;
    }


}



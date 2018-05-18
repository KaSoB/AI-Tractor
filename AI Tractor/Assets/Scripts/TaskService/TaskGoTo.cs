﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskGoTo : Task {
    private NodesGrid grid;
    private NavMeshAgent agent;

    private Queue<Node> path = new Queue<Node>();
    private Node currentTargetNode = null;
    public TaskGoTo(GameObject subject, object goal) : base(goal) {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<NodesGrid>();
        agent = subject.GetComponent<NavMeshAgent>();
    }

    private void SetDestinationToNavMeshAgent() {
        currentTargetNode = path.Dequeue();
        agent.destination = new Vector3(currentTargetNode.transform.position.x, 0, currentTargetNode.transform.position.z);
    }
    private bool IsReachedNode() {
        return Vector3.Distance(subject.transform.position, currentTargetNode.transform.position) < 0.1F;
    }
    private bool IsReachedGoal() {
        return Vector3.Distance(subject.transform.position, (Vector3) goal) < 0.1F;
    }


}


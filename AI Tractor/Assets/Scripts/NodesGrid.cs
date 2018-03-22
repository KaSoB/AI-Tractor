using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodesGrid : MonoBehaviour {
    private PathFind.Grid grid;

    private float[,] tilesMap;

    void Start() {
        var nodes = GameObject.FindGameObjectsWithTag("Node").Select(x => x.GetComponent<Node>()).ToList();
        grid = CreateTilesMap(nodes);
    }

    public Queue<PathFind.Point> GetPath(Vector3 startPosition, Vector3 target) {
        PathFind.Point starPos = new PathFind.Point((int) startPosition.x, (int) startPosition.z);
        PathFind.Point targetPos = new PathFind.Point((int) target.x, (int) target.z);
        return new Queue<PathFind.Point>(PathFind.Pathfinding.FindPath(grid, starPos, targetPos));
    }

    void Update() {

    }

    private PathFind.Grid CreateTilesMap(List<Node> nodes) {
        
        int x = nodes.Max(it => it.X) + 1;
        int y = nodes.Max(it => it.Y) + 1;

        tilesMap = new float[x, y];
     
        foreach (var node in nodes) {
            tilesMap[node.X, node.Y] = node.Cost;
        }

        return new PathFind.Grid(x, y, tilesMap);
    }
}

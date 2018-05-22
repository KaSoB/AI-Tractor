using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShuffleFarmPositions : MonoBehaviour {
    void Awake() {
        var farmFields = GameObject.FindGameObjectsWithTag("FarmField").ToList();
        var farmFieldPositions = farmFields.Select(x => x.transform.position).ToList();
        Shuffle(farmFields);

        for (int i = 0 ; i < farmFields.Count ; i++) {
            farmFields[i].transform.position = farmFieldPositions[i];
        }
    }

    static void Shuffle(List<GameObject> objects) {
        for (int i = 0 ; i < objects.Count ; i++) {
            GameObject temp = objects[i];
            int randomIndex = Random.Range(i, objects.Count);
            objects[i] = objects[randomIndex];
            objects[randomIndex] = temp;
        }
    }
}

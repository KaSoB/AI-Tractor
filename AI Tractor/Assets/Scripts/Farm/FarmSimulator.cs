using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmSimulator : MonoBehaviour {
    private List<FarmField> farmLand = new List<FarmField>();

    private float updateTime = 3.0F;


    void Start() {
        farmLand = GameObject.FindGameObjectsWithTag("FarmField").Select(y => y.GetComponent<FarmField>()).ToList();
        Run();
    }

    private void Run() {
        InvokeRepeating("ModifyFarmland", 1F, updateTime);
    }
    private void Stop() {
        CancelInvoke();
    }

    private void ModifyFarmland() {
        int randomFarmFieldIndex = Random.Range(0, farmLand.Count);
        FarmField farmField = farmLand[randomFarmFieldIndex];

        int randomPropertyIndex = Random.Range(0, farmField.Count());
        Property.Type randomPropertyType = (Property.Type) System.Enum.ToObject(typeof(Property.Type), randomPropertyIndex); // Cast int to enum in c#

        int level = farmField.GetLevel(randomPropertyType);
        bool increaseLevel = Random.value > 0.5F; // Random boolean
        level += increaseLevel ? 1 : -1;

        farmField.SetProperty(randomPropertyType, level);

        Note(farmField, randomPropertyType, level);
    }

    private void Note(FarmField farmField, Property.Type propertyType, int value) {
        // TODO:
        print(string.Format("Pole {0} ma nową wartość ({2}) we właściwości {1}", farmField.gameObject.name, propertyType, value));
    }
    
    public void ChangeUpdateTime(float value01) {
        Stop();
        updateTime = value01 * 3; // multipler
        Run();
    }
}

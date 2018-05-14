using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmSimulator : MonoBehaviour {
    private List<FarmField> farmLand = new List<FarmField>();
    private float updateTime = 5.0F;

    void Start() {
        farmLand = GameObject.FindGameObjectsWithTag("FarmField").Select(y => y.GetComponent<FarmField>()).ToList();
        Run();
    }

    public void Run() {
        if (!IsInvoking("ModifyEnvironment")) {
            InvokeRepeating("ModifyEnvironment", 1F, updateTime);
        }
    }
    public void Stop() {
        CancelInvoke();
    }

    private void ModifyEnvironment() {
        int randomFarmFieldIndex = Random.Range(0, farmLand.Count);
        FarmField farmField = farmLand[randomFarmFieldIndex];

        int randomPropertyIndex = Random.Range(0, farmField.Count());
        Property.Type randomPropertyType = (Property.Type) System.Enum.ToObject(typeof(Property.Type), randomPropertyIndex); // Cast int to enum in c#

        int level = farmField.GetLevel(randomPropertyType);
        bool increaseLevel = Random.value > 0.5F; // Random boolean
        level += increaseLevel ? 1 : -1;

        if (farmField.GetProperty(randomPropertyType).IsInRange(level)) {
            farmField.SetProperty(randomPropertyType, level);
            Debug.Log(string.Format("Pole {0} ma nową wartość ({2}) we właściwości {1}", farmField.gameObject.name, randomPropertyType, level));
        } else {
            Debug.Log(string.Format("Pole {0} nie mogło otrzymać wartości ({2}) we właściwości {1}", farmField.gameObject.name, randomPropertyType, level));

        }
    }

}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameSimulator : MonoBehaviour {
    public enum Season {
        Winter, Summer, Spring, Autumn
    }

    private List<FarmField> farmLands = new List<FarmField>();

    [SerializeField]
    private Text windText;
    private bool isWindy;
    public bool IsWindy {
        get { return isWindy; }
        private set {
            isWindy = value;
            windText.text = value.ToString();
        }
    }

    [SerializeField]
    private Text currentSeasonText;
    private Season currentSeason;
    public Season CurrentSeason {
        get { return currentSeason; }
        private set {
            currentSeason = value;
            currentSeasonText.text = value.ToString();
        }
    }


    void Start() {
        farmLands = GameObject.FindGameObjectsWithTag("FarmField").Select(y => y.GetComponent<FarmField>()).ToList();
        IsWindy = false;
        CurrentSeason = Season.Spring;

        InvokeRepeating("ModifyFarmFields", 1F, 5F);
        InvokeRepeating("ModifyWindCondition", 1F, 10F);
        InvokeRepeating("ChangeSeason", 1F, 20F);
    }

    private void ModifyFarmFields() {
        int randomFarmFieldIndex = Random.Range(0, farmLands.Count);
        FarmField farmField = farmLands[randomFarmFieldIndex];

        int randomPropertyIndex = Random.Range(0, farmField.Count());
        Property.Type randomPropertyType = (Property.Type) System.Enum.ToObject(typeof(Property.Type), randomPropertyIndex); // Cast int to enum in c#

        int level = farmField.GetLevel(randomPropertyType);
        bool increaseLevel = Random.value > 0.5F; // Random boolean
        level += increaseLevel ? 1 : -1;

        if (farmField.GetProperty(randomPropertyType).IsInRange(level)) {
            farmField.SetProperty(randomPropertyType, level);
            //  Debug.Log(string.Format("Pole {0} ma nową wartość ({2}) we właściwości {1}", farmField.gameObject.name, randomPropertyType, level));
        } else {
            //  Debug.Log(string.Format("Pole {0} nie mogło otrzymać wartości ({2}) we właściwości {1}", farmField.gameObject.name, randomPropertyType, level));

        }
    }
    private void ModifyWindCondition() {
        // probability < 20%
        bool changeWindCondition = Random.value > 0.8F; // Random boolean

        if (isWindy != changeWindCondition) {
            IsWindy = !IsWindy;
        }

    }
    private void ChangeSeason() {
        CurrentSeason = (Season) System.Enum.ToObject(typeof(Season), ((int) CurrentSeason + 1) % 4);
    }
}

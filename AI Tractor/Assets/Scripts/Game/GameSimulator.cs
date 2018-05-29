using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameSimulator : MonoBehaviour {
    public enum Season {
        Winter, Summer, Spring, Autumn
    }

    private List<FarmField> farmFields;

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


    void Awake() {
        farmFields = FindObjectsOfType<FarmField>().ToList();

        ShufflePositions(farmFields);

        IsWindy = RandomBoolean();
        CurrentSeason = RandomSeason();

        InvokeRepeating("ModifyFarmFields", 1F, 5F);
        InvokeRepeating("ModifyWindCondition", 10F, 10F);
        InvokeRepeating("ChangeSeason", 20F, 20F);
    }

    void ShufflePositions(List<FarmField> objects) {
        // save positions
        var objectPositions = objects.Select(x => x.transform.position).ToList();
        // shuffle list
        for (int i = 0 ; i < objects.Count ; i++) {
            FarmField temp = objects[i];
            int randomIndex = Random.Range(i, objects.Count);
            objects[i] = objects[randomIndex];
            objects[randomIndex] = temp;
        }
        // assign old positions to new ordered list
        for (int i = 0 ; i < objects.Count ; i++) {
            farmFields[i].transform.position = objectPositions[i];
        }
    }

    private void ModifyFarmFields() {
        FarmField randomFarmField = farmFields[Random.Range(0, farmFields.Count)];
        Property.Type randomPropertyType = (Property.Type) System.Enum.ToObject(typeof(Property.Type), Random.Range(0, randomFarmField.Count())); // Cast int to enum in C#

        int level = randomFarmField.GetLevel(randomPropertyType);
        level += RandomBoolean() ? 1 : -1;

        if (randomFarmField.GetProperty(randomPropertyType).IsInRange(level)) {
            randomFarmField.SetProperty(randomPropertyType, level);
        }
    }
    private void ModifyWindCondition() {
        bool newWindCondition = RandomBoolean();
        if (isWindy != newWindCondition) {
            IsWindy = !IsWindy;
        }
    }
    private void ChangeSeason() {
        CurrentSeason = (Season) System.Enum.ToObject(typeof(Season), ((int) CurrentSeason + 1) % 4);
    }
    private Season RandomSeason() {
        return (Season) System.Enum.ToObject(typeof(Season), Random.Range(0, 4)); // Cast int to enum in C#
    }
    private bool RandomBoolean() {
        return Random.value > 0.5F;
    }
}

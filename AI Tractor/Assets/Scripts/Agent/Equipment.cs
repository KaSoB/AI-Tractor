
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour {
    #region Inspector
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text fertilizerText;
    [SerializeField]
    private Text waterText;
    [SerializeField]
    private Text pesticidesText;
    [SerializeField]
    private Text soilText;
    #endregion

    private Dictionary<ResourceType, Resource> resources;

    void Start() {
        nameText.text = gameObject.name;
        resources = new Dictionary<ResourceType, Resource>() {
            { ResourceType.FertilityRes, new Resource(fertilizerText) },
            { ResourceType.HumidityRes, new Resource(waterText) },
            { ResourceType.PollutionRes, new Resource(pesticidesText) },
            { ResourceType.AcidityRes, new Resource(soilText) }
        };
    }

    public void AddPoints(ResourceType resourceType, int points) {
        resources[resourceType].Amount += points;
    }
    public void RemovePoints(ResourceType resourceType, int points) {
        resources[resourceType].Amount -= points;
    }
    public int GetResourceLevel(ResourceType resourceType) {
        return resources[resourceType].Amount;
    }
    public int SetResourceLevel(ResourceType resourceType, int level = Resource.MAX_LEVEL) {
        return resources[resourceType].Amount = level;
    }
    public bool HasResources(ResourceType resourceType) {
        return GetResourceLevel(resourceType) >= 1;
    }
}

public enum ResourceType {
    HumidityRes, PollutionRes, FertilityRes, AcidityRes
}

public class Resource {
    public const int INIT_LEVEL = 10;
    public const int MAX_LEVEL = 10;

    Text textUI;
    private int amount;
    public int Amount {
        get { return amount; }
        set {
            if (IsInRange(value)) {
                amount = value;
                textUI.text = value.ToString();
            } else {
                Debug.Log(string.Format("Zasobi próbowano przypisać niewłaściwy poziom {0}", value));
            }

        }
    }
    public Resource(Text textUI, int amount = INIT_LEVEL) {
        this.textUI = textUI;
        Amount = amount;
    }
    public bool IsInRange(int level) {
        return level >= 0 && level <= MAX_LEVEL;
    }
}

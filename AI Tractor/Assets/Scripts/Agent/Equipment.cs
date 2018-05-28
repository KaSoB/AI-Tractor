
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
            { ResourceType.Fertilizer, new Resource(fertilizerText) },
            { ResourceType.Water, new Resource(waterText) },
            { ResourceType.Pesticide, new Resource(pesticidesText) },
            { ResourceType.Soil, new Resource(soilText) }
        };
    }

    public int GetResourceLevel(ResourceType resourceType) {
        return resources[resourceType].Amount;
    }
    public void PushPoints(ResourceType resourceType, int points) {
        resources[resourceType].Amount += points;
    }
    public void PopPoints(ResourceType resourceType, int points) {
        resources[resourceType].Amount -= points;
    }
    public int SetResourceLevel(ResourceType resourceType, int level = Resource.MAX_LEVEL) {
        return resources[resourceType].Amount = level;
    }

}

public enum ResourceType {
    Water, Pesticide, Fertilizer, Soil // TODO: Wymyśleć coś zamiast Soil // edycja ctrl+r x2
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

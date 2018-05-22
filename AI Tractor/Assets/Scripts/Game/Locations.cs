
using System.Collections.Generic;
using UnityEngine;

public class Locations : MonoBehaviour {
    [SerializeField]
    private GameObject waterPoint;

    [SerializeField]
    private GameObject fertilizerPoint;

    [SerializeField]
    private GameObject pesticidePoint;

    [SerializeField]
    private GameObject soilPoint;

    public static Dictionary<ResourceType, Vector3> locations;

    private void Start() {
        locations = new Dictionary<ResourceType, Vector3>() {
            { ResourceType.Fertilizer, fertilizerPoint.transform.position },
            { ResourceType.Soil, soilPoint.transform.position },
            { ResourceType.Pesticide, pesticidePoint.transform.position },
            { ResourceType.Water, waterPoint.transform.position }
        };
    }
}


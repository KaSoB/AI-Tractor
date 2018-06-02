
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
            { ResourceType.FertilityRes, fertilizerPoint.transform.position },
            { ResourceType.AcidityRes, soilPoint.transform.position },
            { ResourceType.PollutionRes, pesticidePoint.transform.position },
            { ResourceType.HumidityRes, waterPoint.transform.position }
        };
    }
}


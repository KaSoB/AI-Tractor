using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmFieldSkin : MonoBehaviour {
    public static readonly int WATER_LEVEL_ACTIVE = 5;
    public static readonly int POLLUTION_LEVEL_ACTIVE = 5;

    [SerializeField]
    private List<GameObject> skins = new List<GameObject>();

    private Animator animator;
    private FarmField farmFieldComponent;

    [SerializeField]
    private GameObject waterMarkGameObject;
    [SerializeField]
    private GameObject pollutionMarkGameObject;

    void Awake() {
        animator = GetComponent<Animator>();
        farmFieldComponent = GetComponent<FarmField>();
        farmFieldComponent.OnUpdateProgressListener += (sender, e) => animator.SetFloat("Progress", e.Data);


        farmFieldComponent.OnUpdatePropertyListener += (sender, e) => {
            var property = e.Data;
            switch (property.PropertyType) {
                case Property.Type.Humidity:
                    MarkProperty(property, waterMarkGameObject, WATER_LEVEL_ACTIVE);
                    break;
                case Property.Type.Pollution:
                    MarkProperty(property, pollutionMarkGameObject, POLLUTION_LEVEL_ACTIVE);
                    break;
            }
        };

    }

    /// <param name="property"></param>
    /// <param name="gameObject">GameObject related with property that will be active or non-active</param>
    /// <param name="levelActive">Level of property which must be reached to make gameObject active</param>
    private void MarkProperty(Property property, GameObject gameObject, int levelActive) {
        if (property.Level == levelActive && gameObject.activeSelf == false) {
            gameObject.SetActive(true);
        }
        if (property.Level != levelActive && gameObject.activeSelf == true) {
            gameObject.SetActive(false);
        }
    }

    public void SetSkin(int level) {
        // indeks listy od 0
        level -= 1;

        if (level > skins.Count) {
            Debug.LogError("Incorrect level of skin for " + gameObject.name);
            return;
        }
        // ustaw wskazany element na true;
        skins[level].SetActive(true);

        //pozostałe elementy ustaw na false;
        skins.Where((v, i) => i != level).ToList().ForEach(y => y.SetActive(false));
    }
}


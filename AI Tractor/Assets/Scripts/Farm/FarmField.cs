using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FarmField : ObservableMonoBehaviour {
    public enum FieldType {
        Corn, Wheat, Carrot
    }

    [SerializeField]
    private FieldType fieldType;

    private float progress;
    public float Progress {
        get { return progress; }
        set {
            progress = value;
            NotifyProgress(value);
        }
    }

    private Dictionary<Property.Type, Property> properties = new Dictionary<Property.Type, Property>() {
        { Property.Type.Humidity, new Property(Property.Type.Humidity) },
        { Property.Type.Fertylity, new Property(Property.Type.Fertylity) },
        { Property.Type.Acidity, new Property(Property.Type.Acidity) },
        { Property.Type.Pollution, new Property( Property.Type.Pollution) }
    };

    public void SetProperty(Property.Type id, int value) {
        if (properties.ContainsKey(id)) {
            properties[id].Level = value;
            Notify(properties[id]);
        } else {
            print("Fail");
        }
    }
    public int GetLevel(Property.Type id) {
        return properties[id].Level;
    }
    public Property GetProperty(Property.Type id) {
        return properties[id];
    }
    public int Count() {
        return properties.Count;
    }
    void Start() {
        Progress = Random.Range(0f, 1f);
    }

    private float timeUpdate = 3.5F; // TODO
    private float tmp_timer; // tODO

    public void Update() { // TODO
        tmp_timer += Time.fixedDeltaTime;
        if (tmp_timer < timeUpdate) {
            return;
        }
        tmp_timer = 0F;


        float updateProgress = Progress + (float)(properties[Property.Type.Fertylity].Level) / 70 - (float) (properties[Property.Type.Pollution].Level) / 130;// TODO
        Progress = Mathf.Clamp(updateProgress, 0F, 1F);// TODO


    }

    public void Harvest() {
        Progress = 0F;
    }

    public override string ToString() {
        return fieldType.ToString() + " field";
    }
}


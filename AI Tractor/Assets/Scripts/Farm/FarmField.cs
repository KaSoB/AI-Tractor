using System;
using System.Collections.Generic;
using UnityEngine;


public class EventArgs<T> : EventArgs {
    public T Data { get; set; }
}

public class FarmField : MonoBehaviour {
    public enum FieldType {
        Corn, Wheat, Carrot
    }

    [SerializeField]
    private FieldType fieldType;

    #region Progress
    private float progressRepeatTime = 4F;
    private float progress;
    public float Progress {
        get { return progress; }
        set {
            progress = value;
            // Notify subscribers
            if (OnUpdateProgressListener != null) {
                OnUpdateProgressListener.Invoke(this, new EventArgs<float>() { Data = value });
            }
        }
    }
    public event EventHandler<EventArgs<float>> OnUpdateProgressListener;
    #endregion


    // TODO zmienić value na int
    private Dictionary<Property.Type, Property> properties;

    public void SetProperty(Property.Type id, int value) {
        if (properties.ContainsKey(id)) {
            properties[id].Level = value;
            // Notify subscribers
            if (OnUpdatePropertyListener != null) {
                OnUpdatePropertyListener.Invoke(this, new EventArgs<Property>() { Data = properties[id] });
            }
        } else {
            print("Fail");
        }
    }
    public event EventHandler<EventArgs<Property>> OnUpdatePropertyListener;

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
        properties = new Dictionary<Property.Type, Property>() {
        { Property.Type.Humidity, new Property(Property.Type.Humidity,true) },
        { Property.Type.Fertylity, new Property(Property.Type.Fertylity,true) },
        { Property.Type.Acidity, new Property(Property.Type.Acidity,true) },
        { Property.Type.Pollution, new Property( Property.Type.Pollution,true) }
    };
        Progress = UnityEngine.Random.Range(0F, 1F);
        InvokeRepeating("Grow", 1F, progressRepeatTime);
    }


    private void Grow() {
        if (properties[Property.Type.Fertylity].Level == 1) {
            return;
        }


        float updateProgress =
            Progress
            + (float) (properties[Property.Type.Fertylity].Level) / 70 // jakaś stała
            + (float) (properties[Property.Type.Humidity].Level) / 250 // jakaś stała
            - (float) (properties[Property.Type.Pollution].Level) / 100 // jakaś stała
            - (float) (properties[Property.Type.Acidity].Level) / 300;// jakaś stała

        Progress = Mathf.Clamp(updateProgress, 0F, 1F); // Clamp progress between 0 and 1
    }
    public void Harvest() {
        Progress = 0F;
    }
    public override string ToString() {
        return fieldType.ToString() + " field";
    }

}


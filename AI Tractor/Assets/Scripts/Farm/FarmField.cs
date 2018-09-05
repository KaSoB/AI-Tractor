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

    private Dictionary<Property.Type, Property> properties;
    public event EventHandler<EventArgs<Property>> OnUpdatePropertyListener;
    public event EventHandler<EventArgs<float>> OnUpdateProgressListener;

    private float progress;
    public float Progress {
        get { return progress; }
        set {
            progress = value;
            // Notify subscribers
            OnUpdateProgressListener?.Invoke(this, new EventArgs<float>() { Data = value });
        }
    }

    void Start() {
        properties = new Dictionary<Property.Type, Property>() {
            { Property.Type.Humidity, new Property(Property.Type.Humidity,true) },
            { Property.Type.Fertylity, new Property(Property.Type.Fertylity,true) },
            { Property.Type.Acidity, new Property(Property.Type.Acidity,true) },
            { Property.Type.Pollution, new Property( Property.Type.Pollution,true) }
        };
        Progress = UnityEngine.Random.Range(0F, 1F);
        InvokeRepeating("Grow", 1F, 4F);
    }

    private void Grow() {
        float updateProgress =
            Progress
            + (float) (properties[Property.Type.Fertylity].Level) / 70 // jakaś stała
            + (float) (properties[Property.Type.Humidity].Level) / 250 // jakaś stała
            - (float) (properties[Property.Type.Pollution].Level) / 100 // jakaś stała
            - (float) (properties[Property.Type.Acidity].Level) / 300;// jakaś stała

        Progress = Mathf.Clamp(updateProgress, 0F, 1F); // Clamp progress between 0 and 1
    }
    public void SetProperty(Property.Type id, int value) {
        if (properties.ContainsKey(id) && properties[id].IsInRange(value)) {
            properties[id].Level = value;
            // Notify subscribers
            OnUpdatePropertyListener?.Invoke(this, new EventArgs<Property>() { Data = properties[id] });
        } else {
            Debug.Log($"{gameObject.name} setProperty error: {id} with value {value}");
        }
    }

    public void AddLevel(Property.Type id, int points) {
        SetProperty(id, GetLevel(id) + points);
    }
    public void DeleteLevel(Property.Type id, int points) {
        SetProperty(id, GetLevel(id) - points);
    }

    public Property GetProperty(Property.Type id) {
        return properties[id];
    }
    public int GetLevel(Property.Type id) {
        return properties[id].Level;
    }
    public int Count() {
        return properties.Count;
    }
    public void Harvest() {
        Progress = 0F;
    }

    public FieldType GetFieldType() {
        return fieldType;
    }
    
    public override string ToString() {
        return fieldType.ToString() + " field";
    }
    
	public string GetImage(){
		String basePath = "image_samples/test/";
		System.Random r = new System.Random ();
		var i = r.Next (1, 5);
		return basePath + this.fieldType.ToString () + '/' + i.ToString() + ".jpg";
	}

}


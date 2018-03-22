using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmField : MonoBehaviour {
    public enum FieldType {
        Corn, Wheat, Carrot
    }
    [SerializeField]
    private FieldType fieldType;

    private GameObject my;


    private int _Humidity;
    public int Humidity {
        get {
            return _Humidity;
        }
        set {
            _Humidity = value;
            switch (_Humidity) {
                case 5:
                    break;
                default:
                    break;

            }

            
            }
        }
    
    public int Fertility { get; set; }
    public int Acidity { get; set; }


    void Start() {
        Humidity = 5;
        Fertility = 5;
        Acidity = 5;
    }

    void Update() {

    }

    public override string ToString() {
        return fieldType.ToString() + " field";
    }
}

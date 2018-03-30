using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Property {
    public const int MIN_LEVEL = 0;
    public const int INIT_LEVEL = 3;
    public const int MAX_LEVEL = 5;
    // Zmiana powyższej wartości wymaga zmian w warstwie UI
    public enum Type {
        Humidity, Fertylity, Acidity, Pollution
    }

    public Type PropertyType { get; set; }

    private int _Level;
    public int Level {
        get { return _Level; }
        set {
            if (IsInRange(value)) {
                _Level = value;
            } else {
                Debug.Log(string.Format("Właściwości {0} próbowano przypisać niewłaściwy poziom {1}", PropertyType, value));
            }
        }
    }

    public Property(Type propertyType, int initLevel = INIT_LEVEL) {
        PropertyType = propertyType;
        Level = initLevel;
    }

    public override string ToString() {
        return PropertyType.ToString();
    }

    private bool IsInRange(int level) {
        return level <= MAX_LEVEL && level >= MIN_LEVEL;
    }
}


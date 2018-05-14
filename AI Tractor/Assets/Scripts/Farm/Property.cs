using UnityEngine;

public class Property {
    public const int INIT_LEVEL = 3;
    public const int MAX_LEVEL = 5;
    // Zmiana powyższej wartości wymaga zmian w warstwie UI
    public enum Type {
        Humidity, Fertylity, Acidity, Pollution
    }

    public Type PropertyType { get; set; }

    private int level;
    public int Level {
        get { return level; }
        set {
            if (IsInRange(value)) {
                level = value;
            } else {
                Debug.Log(string.Format("Właściwości {0} próbowano przypisać niewłaściwy poziom {1}", PropertyType, value));
            }
        }
    }

    public Property(Type propertyType, int initLevel = INIT_LEVEL) {
        PropertyType = propertyType;
        Level = initLevel;
    }

    public bool IsInRange(int level) {
        return level >= 1 && level <= MAX_LEVEL;
    }

    public override string ToString() {
        return PropertyType.ToString();
    }
}


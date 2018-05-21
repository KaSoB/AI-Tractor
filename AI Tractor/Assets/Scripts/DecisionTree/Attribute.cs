using System.Collections;

public class Attribute {
    ArrayList mValues;
    object mLabel;
    public string AttributeName { get; private set; }
    public Attribute(string name, string[] values) {
        AttributeName = name;
        mValues = new ArrayList(values);
        mValues.Sort();
    }

    public Attribute(object Label) {
        mLabel = Label;
        AttributeName = string.Empty;
        mValues = null;
    }

    public string[] Values {
        get {
            if (mValues != null)
                return (string[]) mValues.ToArray(typeof(string));
            else
                return null;
        }
    }

    public bool IsValidValue(string value) {
        return IndexValue(value) >= 0;
    }

    public int IndexValue(string value) {
        return (mValues != null) ? mValues.BinarySearch(value) : -1;
    }

    public override string ToString() {
        return (AttributeName != string.Empty) ? AttributeName : mLabel.ToString();
    }
}
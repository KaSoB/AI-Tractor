using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PropertyUI : MonoBehaviour {
    private const string POINT_TAG = "UIPoint";


    [SerializeField]
    private Property.Type propertyType;

    [SerializeField]
    private Sprite fillRectangleSprite; // TODO
    [SerializeField]
    private Sprite blankRectangleSprite;// TODO


    public void SetObject(Property property) {
        // Set property name
        GetComponentInChildren<Text>().text = property.ToString().ToUpper();
        // Set Image  
        var images = GetComponentsInChildren<Image>().Where(x => x.tag == POINT_TAG);
        foreach (var image in images.Take(property.Level)) {
            image.sprite = fillRectangleSprite;
        }
        foreach (var image in images.Skip(property.Level)) {
            image.sprite = blankRectangleSprite;
        }
    }
    public Property.Type GetPropertyType() {
        return propertyType;
    }
}
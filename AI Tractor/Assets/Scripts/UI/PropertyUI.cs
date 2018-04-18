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
    private Sprite fillRectangleSprite;
    [SerializeField]
    private Sprite blankRectangleSprite;

    private void Start() {
        GetComponentInChildren<Text>().text = propertyType.ToString().ToUpper();
    }

    public void SetLevel(int level) {
        var images = GetComponentsInChildren<Image>().Where(x => x.tag == POINT_TAG);
        foreach (var image in images.Take(level)) {
            image.sprite = fillRectangleSprite;
        }
        foreach (var image in images.Skip(level)) {
            image.sprite = blankRectangleSprite;
        }
    }
    public Property.Type GetPropertyType() {
        return propertyType;
    }
}
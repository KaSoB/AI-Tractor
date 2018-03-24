using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FarmFieldUI : MonoBehaviour, IObserver {
    [SerializeField]
    private GameObject panel;

    [Header("Info")]
    [SerializeField]
    private Text title;
    [SerializeField]
    private Slider progressSlider;
    [SerializeField]
    private List<PropertyUI> items = new List<PropertyUI>();


    private FarmField selectedFarmField;

    void Update() {
        // Left Mouse Button
        if (Input.GetMouseButtonDown(0)) {
            ClickToSelectObject();
        }
        // Escape
        if (Input.GetKeyDown(KeyCode.Escape) && panel.activeSelf) {
            Close();
        }
    }

    private void ClickToSelectObject() {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
            selectedFarmField = hit.transform.gameObject.GetComponent<FarmField>();
            if (selectedFarmField != null) {
                panel.SetActive(true); // Show Panel
                selectedFarmField.Attach(this); // Subscribe subject
                SetFarmInfo(selectedFarmField); // Display info on UI panel
            }
        }
    }

    private void Close() {
        if (selectedFarmField != null) {
            selectedFarmField.Detach(this); // Unsubscribe subject
        }
        panel.SetActive(false); // Hide Panel
    }

    private void SetFarmInfo(FarmField farmField) {
        SetTitle(farmField.ToString());
        SetSlider(farmField.Progress);
        foreach (var propertyUI in items) { // Set properties
            propertyUI.SetObject(farmField.properties[propertyUI.GetPropertyType()]);
        }
    }
    private void SetTitle(string title) {
        this.title.text = title.ToString().ToUpper();
    }
    private void SetSlider(float value) {
        progressSlider.value = value;
    }

    public void UpdateProperty(Property property) {
        items.Where(y => y.GetPropertyType() == property.PropertyType).First().SetObject(property);
    }

    public void UpdateProgress(float progress) {
        SetSlider(progress);
    }
}


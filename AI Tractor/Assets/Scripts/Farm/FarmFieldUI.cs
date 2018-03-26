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
    private Text percentageOfCompletion;
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
            var selectedObject = hit.transform.gameObject.GetComponent<FarmField>();
            if (selectedObject != null) {
                UnSubscribeSubject();
                selectedFarmField = selectedObject;
                panel.SetActive(true); // Show Panel
                selectedObject.Attach(this); // Subscribe subject
                SetFarmInfo(selectedObject); // Display info on UI panel
            }
        }
    }

    private void Close() {
        UnSubscribeSubject();
        panel.SetActive(false); // Hide Panel
    }

    private void UnSubscribeSubject() {
        if (selectedFarmField != null) {
            selectedFarmField.Detach(this); // Unsubscribe subject
        }
    }

    private void SetFarmInfo(FarmField farmField) {
        SetTitle(farmField.ToString());
        SetProgress(farmField.Progress);
        foreach (var propertyUI in items) { // Set properties
            propertyUI.SetObject(farmField.GetProperty(propertyUI.GetPropertyType()));
        }
    }
    private void SetTitle(string title) {
        this.title.text = title.ToString().ToUpper();
    }
    private void SetProgress(float value) {
        percentageOfCompletion.text = String.Format("{0:P2}", value);
    }

    public void UpdateProperty(Property property) {
        items.Where(y => y.GetPropertyType() == property.PropertyType).First().SetObject(property);
    }

    public void UpdateProgress(float progress) {
        SetProgress(progress);
    }
}


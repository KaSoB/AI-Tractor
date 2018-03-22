using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmFieldInfo : MonoBehaviour {
    [SerializeField]
    private GameObject panel;


    [SerializeField]
    private Text text_Title;
    [SerializeField]
    private Text text_Prop1;
    [SerializeField]
    private Text text_Prop2;
    [SerializeField]
    private Text text_Prop3;

    void Update() {
        // Left Mouse Button
        if (Input.GetMouseButtonDown(0)) {
            SelectObject();
        }
        // Escape
        if (Input.GetKeyDown(KeyCode.Escape) && panel.activeSelf) {
            HidePanel();
        }
    }

    private void SelectObject() {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
            var farmField = hit.transform.gameObject.GetComponent<FarmField>();
            if (farmField != null) {
                DisplayInfo(farmField);
            }
        }
    }

    private void HidePanel() {
        panel.SetActive(false);
    }

    private void DisplayInfo(FarmField farmField) {
        panel.SetActive(true);

        text_Title.text = farmField.ToString().ToUpper();
        text_Prop1.text = farmField.Humidity.ToString();
        text_Prop2.text = farmField.Fertility.ToString();
        text_Prop3.text = farmField.Acidity.ToString();
    }
}

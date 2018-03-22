using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {

    [SerializeField]
    private Vector3 translation;


    public RaycastHit Hit;

    [SerializeField]
    [Range(0.1F,5F)]
    // Refresh time in seconds.
    private float sensorRefreshTime;

    private float tmp_timer = 0F;

    void Start() {

    }

    void FixedUpdate() {
        // Timer
        tmp_timer += Time.fixedDeltaTime;
        if (tmp_timer < sensorRefreshTime) {
            return;
        }
        tmp_timer = 0F;



        if (Physics.Raycast(transform.position, transform.position + translation, out Hit, 100)) {
            Debug.Log("Sensor Name: " + gameObject.name +  " Detected: " + Hit.collider.tag);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + translation);
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + translation);
    }
}

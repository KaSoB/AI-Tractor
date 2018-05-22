using UnityEngine;

public class SwitchGreenAgent : MonoBehaviour {
    public GameObject agent;
    public void SwitchActivity(bool value) {
        agent.GetComponent<Animator>().SetBool("Idle", value);
    }
}

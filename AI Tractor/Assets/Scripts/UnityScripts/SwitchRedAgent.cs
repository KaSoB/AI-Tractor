using UnityEngine;

public class SwitchRedAgent : MonoBehaviour {
	public GameObject agent;
	public void SwitchActivity(bool value) {
		agent.GetComponent<Animator>().SetBool("Idle", value);
	}
}
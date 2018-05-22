
using System.Linq;
using UnityEngine;

public class TaskTestController : MonoBehaviour {
    protected Agent agent;

    private void Start() {
        agent = GetComponent<Agent>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            agent.TaskManager.ChangeState(Task.State.Start);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            agent.GoToFillUpResource(ResourceType.Soil, Task.State.Start);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            agent.Goto(new Vector3(6, 0, 0), Task.State.Start);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            agent.Goto(new Vector3(0, 0, 0), Task.State.Start);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            agent.Harvest(GameObject.FindGameObjectsWithTag("FarmField").OrderBy(y => Vector3.Distance(transform.position, y.transform.position)).First().GetComponent<FarmField>(), Task.State.Start);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            agent.DoFarmAction(GameObject.FindGameObjectsWithTag("FarmField").OrderBy(y => Vector3.Distance(transform.position, y.transform.position)).First().GetComponent<FarmField>(), Property.Type.Humidity, ResourceType.Water, 1, Task.State.Start);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            agent.DoFarmAction(GameObject.FindGameObjectsWithTag("FarmField").OrderBy(y => Vector3.Distance(transform.position, y.transform.position)).First().GetComponent<FarmField>(), Property.Type.Fertylity, ResourceType.Fertilizer, 1, Task.State.Start);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            agent.FillUpResource(ResourceType.Water, Task.State.Start);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            agent.Scan(5, Task.State.Start);
        }
    }
}


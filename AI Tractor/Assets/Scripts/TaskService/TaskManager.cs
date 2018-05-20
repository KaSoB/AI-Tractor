
public class TaskManager {
    public Task CurrentTask { get; private set; }

    public TaskManager() {

    }

    public void SetTask(Task task) {
        CurrentTask = task;
        CurrentTask.GetStateMachine().ChangeState(Task.State.Init);
    }
    public void ChangeState(Task.State state) {
        CurrentTask.GetStateMachine().ChangeState(state);
    }
    public bool HasTask() {
        return CurrentTask != null;
    }
    public bool HasFinished() {
        return CurrentTask.GetStateMachine().State == Task.State.Finish || CurrentTask.GetStateMachine().State == Task.State.Interrupt;
    }
    public Task.State GetStatus() {
        return CurrentTask.GetStateMachine().State;
    }

}
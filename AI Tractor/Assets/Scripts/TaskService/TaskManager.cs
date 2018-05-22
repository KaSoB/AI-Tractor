
public class TaskManager {
    public Task CurrentTask { get; private set; }

    public void SetTask(Task task, Task.State state = Task.State.Init) {
        CurrentTask = task;
        ChangeState(state);
    }
    public void ChangeState(Task.State state) {
        CurrentTask.GetStateMachine().ChangeState(state);
    }
    public bool HasTask() {
        return CurrentTask != null;
    }
    public bool HasFinished() {
        return GetStatus() == Task.State.Finish || GetStatus() == Task.State.Interrupt;
    }
    public Task.State GetStatus() {
        return CurrentTask.GetStateMachine().State;
    }

}
using UnityEngine;

public abstract class Task : MonoBehaviour {
    public enum State {
        Init,
        Start,
        Execute,
        Finish,
        Interrupt
    }
    public GameObject Subject { get; set; }
    public object Goal { get; set; }
    public StateMachine<State> FSM { get; set; }
    public string ErrorMessage { get; set; }

    private void Start() {
        FSM = StateMachine<State>.Initialize(this);
        Subject = gameObject;
    }

    public StateMachine<State> GetStateMachine() {
        return FSM;
    }
}
using UnityEngine;

public abstract class Task : MonoBehaviour {
    protected GameObject subject;
    protected object goal;
    protected StateMachine<State> fsm;

    public enum State {
        Init,
        Start,
        Execute,
        Finish,
        Interrupt
    }

    public Task(GameObject subject, object goal = null) {
        this.subject = subject;
        this.goal = goal;
    }

    virtual protected void Init_Enter() { }
    virtual protected void Init_Update() { }
    virtual protected void Init_Exit() { }

    virtual protected void Start_Enter() { }
    virtual protected void Start_Update() { }
    virtual protected void Start_Exit() { }

    virtual protected void Execute_Enter() { }
    virtual protected void Execute_Update() { }
    virtual protected void Execute_Exit() { }

    virtual protected void Finish_Enter() { }
    virtual protected void Finish_Update() { }
    virtual protected void Finish_Exit() { }

    virtual protected void Interrupt_Enter() { }
    virtual protected void Interrupt_Update() { }
    virtual protected void Interrupt_Exit() { }

}


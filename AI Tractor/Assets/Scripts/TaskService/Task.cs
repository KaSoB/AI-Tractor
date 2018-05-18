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

    public Task(object goal = null) {
        this.goal = goal;
    }

    private void Start() {
        fsm = StateMachine<State>.Initialize(this);
        fsm.ChangeState(State.Init);
    }

    public StateMachine<State> GetStateMachine() {
        return fsm;
    }


    virtual protected void Init_Enter() {

    }
    virtual protected void Init_Update() {

    }
    virtual protected void Init_Exit() {

    }

    virtual protected void Start_Enter() { Debug.Log(":)14"); }
    virtual protected void Start_Update() {
        Debug.Log(":)13");
        fsm.ChangeState(State.Execute);
    }
    virtual protected void Start_Exit() { Debug.Log(":)12"); }

    virtual protected void Execute_Enter() { Debug.Log(":)0"); }
    virtual protected void Execute_Update() {
        Debug.Log(":)9");
        fsm.ChangeState(State.Finish);
    }
    virtual protected void Execute_Exit() { Debug.Log(":)8"); }

    virtual protected void Finish_Enter() { Debug.Log(":)7"); }
    virtual protected void Finish_Update() {
        Debug.Log(":6)");
        fsm.ChangeState(State.Interrupt);
    }
    virtual protected void Finish_Exit() { Debug.Log(":)5"); }

    virtual protected void Interrupt_Enter() { Debug.Log(":1)"); }
    virtual protected void Interrupt_Update() { Debug.Log(":)2"); }
    virtual protected void Interrupt_Exit() { Debug.Log(":)3"); }

}


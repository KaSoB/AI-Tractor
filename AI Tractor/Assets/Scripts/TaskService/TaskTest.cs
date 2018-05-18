


using UnityEngine;

public class TaskTest : Task {
    public TaskTest() : base(null) {

    }

    virtual new protected void Init_Enter() {
        Debug.Log(":1414141)");
    }
    virtual new protected void Init_Update() {
        fsm.ChangeState(State.Start);
        Debug.Log(":11111)");
    }
    virtual new protected void Init_Exit() {
        Debug.Log(":331313)");
    }

    virtual new protected void Start_Enter() {
        Debug.Log(":)14");
    }
    virtual new protected void Start_Update() {
        Debug.Log(":)13");
        fsm.ChangeState(State.Execute);
    }
    virtual new protected void Start_Exit() {
        Debug.Log(":)12");
    }

    virtual new protected void Execute_Enter() {
        Debug.Log(":)0");
    }
    virtual new protected void Execute_Update() {
        Debug.Log(":)9");
        fsm.ChangeState(State.Finish);
    }
    virtual new protected void Execute_Exit() {
        Debug.Log(":)8");
    }

    virtual new protected void Finish_Enter() {
        Debug.Log(":)7");
    }
    virtual new protected void Finish_Update() {
        Debug.Log(":6)");
        fsm.ChangeState(State.Interrupt);
    }
    virtual new protected void Finish_Exit() {
        Debug.Log(":)5");
    }

    virtual new protected void Interrupt_Enter() {

    }
    virtual new protected void Interrupt_Update() {

    }
    virtual new protected void Interrupt_Exit() {

    }

}


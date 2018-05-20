using System.Reflection.Emit;
using UnityEngine;

public class TaskTest : Task {

    public void Start_Update() {
        Debug.Log(":)startupdate " + MethodBuilder.GetCurrentMethod().Name);
        FSM.ChangeState(State.Execute);
    }

    public void Execute_Update() {
        Debug.Log(":)executeupdate " + MethodBuilder.GetCurrentMethod().Name);
        FSM.ChangeState(State.Finish);
    }

    public void Finish_Update() {
        Debug.Log(":)finishupdate " + MethodBuilder.GetCurrentMethod().Name);
        FSM.ChangeState(State.Interrupt);
    }

    public void Interrupt_Update() {
        Debug.Log(":)ojoj " + MethodBuilder.GetCurrentMethod().Name);
    }

}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Task {
    public enum State { Entry, Preparing, Working, Completed, Suspended, Blocked}
    public enum Transition { Start, Work, Finish, Interrupt, Block }
        
    public State CurrentState { get; protected set; }


    protected GameObject subject;
    protected object goal;

    public Task(object goal = null) {
        this.goal = goal;
        CurrentState = State.Entry;
    }

    public abstract void Execute(GameObject subject);
    public abstract void Update();
}

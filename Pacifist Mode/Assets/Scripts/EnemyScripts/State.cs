using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachine stateMachine;
    //constructor

    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public abstract void CheckTransitions();

    public abstract void Act();

    public virtual void OnStateEnter() {}

    public virtual void OnStateExit() {}
}

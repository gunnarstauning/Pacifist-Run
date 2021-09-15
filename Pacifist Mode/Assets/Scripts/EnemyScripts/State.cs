using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected ChasePlayer chasePlayer;
    //constructor

    public State(ChasePlayer chasePlayer)
    {
        this.chasePlayer = chasePlayer;
    }

    public abstract void CheckTransitions();

    public abstract void Act();

    public virtual void OnStateEnter() {}

    public virtual void OnStateExit() {}
}

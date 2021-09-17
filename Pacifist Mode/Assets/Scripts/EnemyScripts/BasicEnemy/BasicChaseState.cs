using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicChaseState : State
{
    public BasicChaseState(StateMachine stateMachine) : base(stateMachine) { }

    private Vector3 destinationPoint;

    public override void CheckTransitions() 
    {
        if (!stateMachine.CheckIfInRange("Player", 10f))
        {
            stateMachine.SetState(new PatrolState(stateMachine));
        }
    }
    public override void Act() 
    {
        if (stateMachine.enemyToChase != null)
        {
            destinationPoint = new Vector3 (stateMachine.enemyToChase.transform.position.x, stateMachine.enemyToChase.transform.position.y, stateMachine.enemyToChase.transform.position.z);
            stateMachine.agent.SetDestination(destinationPoint);
        }
    }

    public override void OnStateEnter()
    {
        if (stateMachine.agent != null)
        {
            stateMachine.agent.speed = 6f;
        }
        stateMachine.ChangeColor(Color.red);
    }
}

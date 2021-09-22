using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFiringState : State
{
    public ShootFiringState(StateMachine stateMachine) : base(stateMachine) { }

    private Vector3 destinationPoint;

    public override void CheckTransitions() 
    {
        if (!stateMachine.CheckIfInRange("Player", 25f))
        {
            stateMachine.SetState(new ShootPatrolState(stateMachine));
        } else if (!stateMachine.CheckIfInRange("Player", 15f))
        {
            stateMachine.SetState(new ShootChaseState(stateMachine));
        } else if (stateMachine.CheckIfInRange("Player", 8f))
        {
            stateMachine.SetState(new ShootRunningState(stateMachine));
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
            stateMachine.agent.speed = 0f;
        }
        stateMachine.ChangeColor(Color.green);
    }
}

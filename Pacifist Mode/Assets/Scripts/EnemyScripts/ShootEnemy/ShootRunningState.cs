using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRunningState : State
{
    public ShootRunningState(StateMachine stateMachine) : base(stateMachine) { }

    private Vector3 destinationPoint;

    public override void CheckTransitions() 
    {
        if (!stateMachine.CheckIfInRange("Player", 25f))
        {
            stateMachine.SetState(new ShootPatrolState(stateMachine));
        } else if (!stateMachine.CheckIfInRange("Player", 15f))
        {
            stateMachine.SetState(new ShootChaseState(stateMachine));
        } else if (!stateMachine.CheckIfInRange("Player", 8f))
        {
            stateMachine.SetState(new ShootFiringState(stateMachine));
        }
    }

    public override void Act() 
    {
        if (stateMachine.enemyToChase != null)
        {
            Vector3 dirToPlayer = stateMachine.transform.position - stateMachine.enemyToChase.transform.position;

            Vector3 newPos = stateMachine.transform.position + dirToPlayer;

            stateMachine.agent.SetDestination(newPos);
        }
    }

    public override void OnStateEnter()
    {
        if (stateMachine.agent != null)
        {
            stateMachine.agent.speed = 11f;
        }
        stateMachine.ChangeColor(Color.yellow);
    }
}

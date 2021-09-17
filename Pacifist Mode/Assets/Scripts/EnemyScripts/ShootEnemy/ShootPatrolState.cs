using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPatrolState : State
{
    public ShootPatrolState(StateMachine stateMachine) : base(stateMachine) { }

    private Vector3 destinationPoint;

    public override void CheckTransitions() 
    {
        if (stateMachine.CheckIfInRange("Player", 10f))
        {
            stateMachine.SetState(new ShootChaseState(stateMachine));
        }
    }

    public override void Act()
    {
        if (stateMachine.agent == null || stateMachine.agent.remainingDistance > 0.5)
        {
            destinationPoint = new Vector3 (stateMachine.GetNavPoint().position.x, stateMachine.GetNavPoint().position.y, stateMachine.GetNavPoint().position.z);
            stateMachine.agent.SetDestination(destinationPoint);
        } else if (stateMachine.agent.remainingDistance < 0.5)
        {
            stateMachine.GetNextNavPoint();
            destinationPoint = new Vector3 (stateMachine.GetNavPoint().position.x, stateMachine.GetNavPoint().position.y, stateMachine.GetNavPoint().position.z);
            stateMachine.agent.SetDestination(destinationPoint);
        }
    }

    public override void OnStateEnter()
    {
        destinationPoint = new Vector3 (stateMachine.GetNavPoint().position.x, stateMachine.GetNavPoint().position.y, stateMachine.GetNavPoint().position.z);
        stateMachine.agent.SetDestination(destinationPoint);
        if (stateMachine.agent != null)
        {
            stateMachine.agent.speed = 5f;
        }
        stateMachine.ChangeColor(Color.blue);
    }
}

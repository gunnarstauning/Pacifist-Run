using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState: State
{
    public PatrolState(ChasePlayer chasePlayer) : base(chasePlayer) { }

    private Vector3 destinationPoint;

    public override void CheckTransitions() 
    {
        if (chasePlayer.CheckIfInRange("Player"))
        {
            chasePlayer.SetState(new ChaseState(chasePlayer));
        }
    }

    public override void Act()
    {
        if (chasePlayer.agent == null || chasePlayer.agent.remainingDistance > 0.5)
        {
            destinationPoint = new Vector3 (chasePlayer.GetNavPoint().position.x, chasePlayer.GetNavPoint().position.y, chasePlayer.GetNavPoint().position.z);
            chasePlayer.agent.SetDestination(destinationPoint);
        } else if (chasePlayer.agent.remainingDistance < 0.5)
        {
            chasePlayer.GetNextNavPoint();
            destinationPoint = new Vector3 (chasePlayer.GetNavPoint().position.x, chasePlayer.GetNavPoint().position.y, chasePlayer.GetNavPoint().position.z);
            chasePlayer.agent.SetDestination(destinationPoint);
        }
    }

    public override void OnStateEnter()
    {
        destinationPoint = new Vector3 (chasePlayer.GetNavPoint().position.x, chasePlayer.GetNavPoint().position.y, chasePlayer.GetNavPoint().position.z);
        chasePlayer.agent.SetDestination(destinationPoint);
        if (chasePlayer.agent != null)
        {
            chasePlayer.agent.speed = 5f;
        }
        chasePlayer.ChangeColor(Color.blue);
    }
}

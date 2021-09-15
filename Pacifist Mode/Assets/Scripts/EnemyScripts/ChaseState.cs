using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public ChaseState(ChasePlayer chasePlayer) : base(chasePlayer) { }

    private Vector3 destinationPoint;

    public override void CheckTransitions() 
    {
        if (!chasePlayer.CheckIfInRange("Player"))
        {
            chasePlayer.SetState(new PatrolState(chasePlayer));
        }
    }
    public override void Act() 
    {
        if (chasePlayer.enemyToChase != null)
        {
            destinationPoint = new Vector3 (chasePlayer.enemyToChase.transform.position.x, chasePlayer.enemyToChase.transform.position.y, chasePlayer.enemyToChase.transform.position.z);
            chasePlayer.agent.SetDestination(destinationPoint);
        }
    }

    public override void OnStateEnter()
    {
        if (chasePlayer.agent != null)
        {
            chasePlayer.agent.speed = 6f;
        }
        chasePlayer.ChangeColor(Color.red);
    }
}

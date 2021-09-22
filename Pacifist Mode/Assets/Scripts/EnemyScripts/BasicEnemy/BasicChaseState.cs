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
            if (!stateMachine.CheckIfInRange("BasicEnemy", 10f))
            {
                if (!stateMachine.CheckIfInRange("ShootEnemy", 10f))
                {
                    stateMachine.SetState(new BasicPatrolState(stateMachine));
                }
            }
        }
    }
    public override void Act() 
    {
        if (stateMachine.enemyToChase != null)
        {
            destinationPoint = new Vector3(stateMachine.enemyToChase.transform.position.x, stateMachine.enemyToChase.transform.position.y, stateMachine.enemyToChase.transform.position.z);
            stateMachine.agent.SetDestination(destinationPoint);
        }

        if (stateMachine.CheckIfInHitRange("Player", 4))
        {
            stateMachine.anim.SetBool("Hit", true);
            stateMachine.agent.speed = 0.1f;
        }
        else if (stateMachine.CheckIfInHitRange("ShootEnemy", 4))
        {
            stateMachine.anim.SetBool("Hit", true);
            stateMachine.agent.speed = 0.1f;
        }
        else
        {
            stateMachine.anim.SetBool("Hit", false);
            stateMachine.agent.speed = 7f;
        }
        
    }

    public override void OnStateEnter()
    {
        if (stateMachine.agent != null)
        {
            stateMachine.agent.speed = 7f;
        }
        stateMachine.ChangeColor(Color.red);
    }
}

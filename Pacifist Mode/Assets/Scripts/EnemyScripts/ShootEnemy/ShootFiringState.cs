using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFiringState : State
{
    public ShootFiringState(StateMachine stateMachine) : base(stateMachine) { }

    private Vector3 destinationPoint;

    public float timeBetweenShots = 1f;
    public float timeSinceShot = 0;

    public override void CheckTransitions() 
    {
        if (!stateMachine.CheckIfInRange("Player", 25f))
        {
            stateMachine.anim.SetBool("IsShooting", false);
            stateMachine.SetState(new ShootPatrolState(stateMachine));
        } else if (!stateMachine.CheckIfInRange("Player", 18f))
        {
            stateMachine.anim.SetBool("IsShooting", false);
            stateMachine.SetState(new ShootChaseState(stateMachine));
        } else if (stateMachine.CheckIfInRange("Player", 5f))
        {
            stateMachine.anim.SetBool("IsShooting", false);
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

        timeSinceShot += Time.deltaTime;

        Quaternion rotation = Quaternion.LookRotation(stateMachine.enemyToChase.transform.position - stateMachine.agent.transform.position);
        stateMachine.agent.transform.rotation = Quaternion.Lerp(stateMachine.agent.transform.rotation, rotation, Time.deltaTime * 10.0f);

        if (timeSinceShot > timeBetweenShots)
        {
            timeSinceShot = 0;
            stateMachine.fireBullet();
            stateMachine.anim.SetBool("IsShooting",true);
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

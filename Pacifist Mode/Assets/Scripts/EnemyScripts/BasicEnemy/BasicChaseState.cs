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
	    if (stateMachine.CheckIfInRange("Player", 4f))
		{
            Quaternion rotation = Quaternion.LookRotation(stateMachine.enemyToChase.transform.position - stateMachine.agent.transform.position);
            stateMachine.agent.transform.rotation = Quaternion.Lerp(stateMachine.agent.transform.rotation, rotation, Time.deltaTime * 10.0f);
            stateMachine.anim.SetBool("Hit", true);
            stateMachine.ableToPunch = true;
			stateMachine.agent.speed = 0f;
		}
		else if (stateMachine.CheckIfInRange("BasicEnemy", 4f))
		{
			stateMachine.anim.SetBool("Hit", true);
            if (stateMachine.anim.GetBool("Hit") && stateMachine.anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            {
                stateMachine.DestroyEnemy();
            }
            stateMachine.agent.speed = 0f;
		}
		else if (stateMachine.CheckIfInRange("ShootEnemy", 4f))
		{
			stateMachine.anim.SetBool("Hit", true);
            if (stateMachine.anim.GetBool("Hit") && stateMachine.anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            {
                stateMachine.DestroyEnemy();
            }
            stateMachine.agent.speed = 0f;
		}
		else
		{
            stateMachine.ableToPunch = false;
            stateMachine.anim.SetBool("Hit", false);
		    stateMachine.agent.speed = 7f;
		}
		
		
	
        if (stateMachine.enemyToChase != null)
        {
            destinationPoint = new Vector3(stateMachine.enemyToChase.transform.position.x, stateMachine.enemyToChase.transform.position.y, stateMachine.enemyToChase.transform.position.z);
            stateMachine.agent.SetDestination(destinationPoint);
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

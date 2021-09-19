﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    public GameObject[] navPoints;
    public int navPointNum;

    public Renderer[] childrenRend;

    public GameObject[] enemies;
    public GameObject enemyToChase;

    public float detectionRange = 10;
    public float remainingDistance;

    public State currentState;

    public GameObject player;
    public NavMeshAgent agent;
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        //navPoints = GameObject.FindGameObjectsWithTag("navPoint");
        player = GameObject.FindWithTag("Player");
        childrenRend = GetComponentsInChildren<Renderer>();
        agent = this.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        SetState(new PatrolState(this));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.CheckTransitions();
        currentState.Act();
        if(agent.speed <= .5f){
            anim.SetFloat("Speed", 0f);
        }
        else if(agent.speed > .5f && agent.speed < 8f){
            anim.SetFloat("Speed", .5f);
        }
        else{
            anim.SetFloat("Speed", 1f);
        }
        //agent.SetDestination(player.transform.position);
    }

    public void GetNextNavPoint() 
    {
        navPointNum = (navPointNum + 1) % navPoints.Length;
    }

    public Transform GetNavPoint() 
    {
        return navPoints[navPointNum].transform;
    }

    public bool CheckIfInRange(string tag)
    {
        enemies = GameObject.FindGameObjectsWithTag(tag);
        if (enemies != null)
        {
            foreach (GameObject g in enemies)
            {
                if (Vector3.Distance(g.transform.position, transform.position) < detectionRange)
                {
                    enemyToChase = g;
                    agent.speed = 12f;
                    anim.SetFloat("Speed", .5f);
                    if(Vector3.Distance(enemyToChase.transform.position, transform.position) < 2){
                        agent.speed = 0f;
                    }
                    return true;
                }
                
            }
        }
        return false;
    }

    public void SetState(State state)
    {
        if(currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        
        if(currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    public void ChangeColor(Color color)
    {
        foreach(Renderer r in childrenRend)
        {
            foreach(Material m in r.materials)
            {
                m.color = color;
            }
        }
    }
}

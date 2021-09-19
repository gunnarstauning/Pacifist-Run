using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public GameObject runner;
    
    public NavMeshAgent agent;
    //public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        runner = GameObject.FindWithTag("enemy1");
        agent = GetComponent<NavMeshAgent>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.destination = target.position;
            //anim.SetFloat("Speed",.5f);
        }
        else
        {
            //anim.SetFloat("Speed",0f);
        }
    }
}
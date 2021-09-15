using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public GameObject runner;
    
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        runner = GameObject.FindWithTag("enemy1");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.destination = target.position;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    public LayerMask layerMask;

    public GameObject[] navPoints;
    public int navPointNum;

    public Renderer[] childrenRend;

    public GameObject[] enemies;
    public GameObject enemyToChase;

    public float remainingDistance;

    public State currentState;

    public GameObject player;
    public NavMeshAgent agent;
    public Animator anim;

    //Bullet Info
    public GameObject bulletPrefab;
    public Transform launchPosition;
    public float bulletSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        //navPoints = GameObject.FindGameObjectsWithTag("navPoint");
        player = GameObject.FindWithTag("Player");
        childrenRend = GetComponentsInChildren<Renderer>();
        agent = this.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        if (this.tag == "BasicEnemy")
        {
            SetState(new BasicPatrolState(this));
        } else if (this.tag == "ShootEnemy") 
        {
            SetState(new ShootPatrolState(this));
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentState.CheckTransitions();
        currentState.Act();
        if(agent.speed <= .5f){
            //anim.SetFloat("Speed", 0f);
        }
        else if(agent.speed > .5f && agent.speed < 6f){
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

    public bool CheckIfInRange(string tag, float range)
    {
        enemies = GameObject.FindGameObjectsWithTag(tag);
        if (enemies != null)
        {
            foreach (GameObject g in enemies)
            {
                if (g.transform.IsChildOf(this.transform))
                {
                    continue;
                }
                if (Vector3.Distance(g.transform.position, transform.position) < range)
                {
                    float dist = Vector3.Distance(transform.position, g.transform.position);
                    Debug.DrawRay(transform.position, g.transform.position - transform.position);

                    RaycastHit hit;
                    Ray ray = new Ray(transform.position, g.transform.position - transform.position);

                    if (!Physics.Raycast(ray, out hit, dist, layerMask, QueryTriggerInteraction.Ignore))
                    {
                        enemyToChase = g;
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            //Destroy(gameObject);
        }
    }
    public bool CheckIfInHitRange(string tag, float range)
    {
        enemies = GameObject.FindGameObjectsWithTag(tag);
        if (enemies != null)
        {
            foreach (GameObject g in enemies)
            {
                if (g.transform.IsChildOf(this.transform))
                {
                    continue;
                }
                if (Vector3.Distance(g.transform.position, transform.position) < range)
                {
                    enemyToChase = g;

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
    public void fireBullet()
    {
        Rigidbody bullet = createBullet();
        bullet.velocity = transform.forward * bulletSpeed;
    }

    public Rigidbody createBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = launchPosition.position;
        //Vector3 bulletRotation = new Vector3(bullet.transform.position.x, 0.0f, bullet.transform.position.y);
        bullet.transform.rotation = Quaternion.LookRotation(enemyToChase.transform.position - agent.transform.position);
        return bullet.GetComponent<Rigidbody>();
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

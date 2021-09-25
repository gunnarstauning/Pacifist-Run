using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int bulletTestTimer = 0;
    private bool hittable = false;

    //For if bullet doesn't hit anything
    private int bulletDeathTimer = 0;
    private int bulletFinalDeath = 3000;

    private void Update()
    {
        if (bulletDeathTimer > bulletFinalDeath)
        {
            Destroy(gameObject);
        }
        else
        {
            bulletDeathTimer++;
        }
        if (bulletTestTimer > 30)
        {
            hittable = true;
        }
        else
        {
            bulletTestTimer++;
        }
    }
    private void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hittable)
        {
            if (other != this)
            {
                Destroy(gameObject);
                if(other.gameObject.tag != "Environment")
                {
                    if (other.gameObject.tag == "Player")
                    {
                        Debug.Log("Player Took a Hit!");
                        
                    }
                    else
                    {
                        Destroy(other.gameObject);
                    }
                }
            }
        }
    }
}






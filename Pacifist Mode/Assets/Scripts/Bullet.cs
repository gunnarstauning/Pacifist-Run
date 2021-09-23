using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter (Collider other)
    {
        if(other != this)
        {
            Destroy(gameObject);
        }
        
    }
}

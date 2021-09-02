using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public GameObject followTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y + 10, followTarget.transform.position.z - 15);
        transform.LookAt(followTarget.transform.position);
    }
}

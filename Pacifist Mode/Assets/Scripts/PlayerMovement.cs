using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool movingForward;
    public bool movingBackward;
    public bool movingLeft;
    public bool movingRight;

    public float acceleration = 2f;
    public float friction = 0.95f;
    public float maxSpeed = 25f;

    public float zVelocity;
    public float xVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            movingForward = true;
        }
        if (Input.GetKeyDown("s"))
        {
            movingBackward = true;
        }
        if (Input.GetKeyDown("a"))
        {
            movingLeft = true;
        }
        if (Input.GetKeyDown("d"))
        {
            movingRight = true;
        }

        if (Input.GetKeyUp("w"))
        {
            movingForward = false;
        }
        if (Input.GetKeyUp("s"))
        {
            movingBackward = false;
        }
        if (Input.GetKeyUp("a"))
        {
            movingLeft = false;
        }
        if (Input.GetKeyUp("d"))
        {
            movingRight = false;
        }

        if (movingForward == true)
        {
            zVelocity = zVelocity + acceleration;
        }
        if (movingBackward == true)
        {
            zVelocity = zVelocity - acceleration;
        }
        if (movingLeft == true)
        {
            xVelocity = xVelocity - acceleration;
        }
        if (movingRight == true)
        {
            xVelocity = xVelocity + acceleration;
        }

        zVelocity = zVelocity * friction;
        xVelocity = xVelocity * friction;

        if (zVelocity > maxSpeed)
        {
            zVelocity = maxSpeed;
        }
        if (zVelocity < -maxSpeed)
        {
            zVelocity = -maxSpeed;
        }
        if (xVelocity > maxSpeed)
        {
            xVelocity = maxSpeed;
        }
        if (xVelocity < -maxSpeed)
        {
            xVelocity = -maxSpeed;
        }

        if (xVelocity < 0.01 && xVelocity > 0 || xVelocity > -0.01 && xVelocity < 0)
        {
            xVelocity = 0;
        }
        if (zVelocity < 0.01 && zVelocity > 0 || zVelocity > -0.01 && zVelocity < 0)
        {
            zVelocity =0;
        }

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + xVelocity, transform.position.y, transform.position.z + zVelocity), Time.deltaTime);
    }
}

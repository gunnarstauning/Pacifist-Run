﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool movingForward;
    public bool movingBackward;
    public bool movingLeft;
    public bool movingRight;

    public float acceleration = 2f;
    public float friction = 0.95f;
    public float maxSpeed = 7.5f;

    //PlayerHP
    public float health = 10f;
    public List<GameObject> hpObjects = new List<GameObject>();

    public float zVelocity;
    public float xVelocity;

    public GameObject mainCamera;
    public Transform[] views;
    public float transitionSpeed;
    private Transform currentView;

    public Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        roomSwitch RoomSwitch = mainCamera.GetComponent<roomSwitch>();
        currentView = views[0];

        playerAnim = GetComponent<Animator>();
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

        //Character Rotation
        Vector3 movement = new Vector3(xVelocity, 0.0f, zVelocity);
        transform.rotation = Quaternion.LookRotation(movement);
        //Character Movement
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + xVelocity, transform.position.y, transform.position.z + zVelocity), Time.deltaTime);

        //Character Run/Walk Animation
        if(xVelocity != 0 || zVelocity != 0)
        {
            playerAnim.SetFloat("Speed", 1f);
        }
        else
        {
            playerAnim.SetFloat("Speed", 0f);
        }

        //Camera Movement
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, currentView.transform.position, Time.deltaTime * transitionSpeed);

        //Camera Angle
        Vector3 currentAngle = new Vector3(
        Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
        Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
        Mathf.LerpAngle(mainCamera.transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

        mainCamera.transform.eulerAngles = currentAngle;

        switch (health)
        {
            case 9:
                Destroy(hpObjects[9]);
                break;
            case 8:
                Destroy(hpObjects[9]);
                Destroy(hpObjects[8]);
                break;
            case 7:
                Destroy(hpObjects[9]);
                Destroy(hpObjects[8]);
                Destroy(hpObjects[7]);
                break;
            case 6:
                Destroy(hpObjects[9]);
                Destroy(hpObjects[8]);
                Destroy(hpObjects[7]);
                Destroy(hpObjects[6]);
                break;
            case 5:
                Destroy(hpObjects[9]);
                Destroy(hpObjects[8]);
                Destroy(hpObjects[7]);
                Destroy(hpObjects[6]);
                Destroy(hpObjects[5]);
                break;
            case 4:
                Destroy(hpObjects[9]);
                Destroy(hpObjects[8]);
                Destroy(hpObjects[7]);
                Destroy(hpObjects[6]);
                Destroy(hpObjects[5]);
                Destroy(hpObjects[4]);
                break;
            case 3:
                Destroy(hpObjects[9]);
                Destroy(hpObjects[8]);
                Destroy(hpObjects[7]);
                Destroy(hpObjects[6]);
                Destroy(hpObjects[5]);
                Destroy(hpObjects[4]);
                Destroy(hpObjects[3]);
                break;
            case 2:
                Destroy(hpObjects[9]);
                Destroy(hpObjects[8]);
                Destroy(hpObjects[7]);
                Destroy(hpObjects[6]);
                Destroy(hpObjects[5]);
                Destroy(hpObjects[4]);
                Destroy(hpObjects[3]);
                Destroy(hpObjects[2]);
                break;
            case 1:
                Destroy(hpObjects[9]);
                Destroy(hpObjects[8]);
                Destroy(hpObjects[7]);
                Destroy(hpObjects[6]);
                Destroy(hpObjects[5]);
                Destroy(hpObjects[4]);
                Destroy(hpObjects[3]);
                Destroy(hpObjects[2]);
                Destroy(hpObjects[1]);
                break;
            case 0:
                Destroy(hpObjects[9]);
                Destroy(hpObjects[8]);
                Destroy(hpObjects[7]);
                Destroy(hpObjects[6]);
                Destroy(hpObjects[5]);
                Destroy(hpObjects[4]);
                Destroy(hpObjects[3]);
                Destroy(hpObjects[2]);
                Destroy(hpObjects[1]);
                Destroy(hpObjects[0]);
                break;
        }
        //health detection
        if(health <= 0)
        {
            Die();
        }
        Debug.Log(health);
    }
    public void Die()
    {
        //Destroy(this.gameObject);
        SceneManager.LoadScene(0);
    }

    //Change to check if its a room trigger
    public void OnTriggerEnter(Collider other)
    {
        string triggerName = other.name;
        changeRoom(triggerName);
        if(other.gameObject.tag == "Bullet")
        {
            health -= 3;
        }
    }
    public void takeDamage(float f)
    {
        health -= f;
    }

    private void changeRoom(string triggerName)
    {
        switch (triggerName)
        {
            case "Trigger1":
                currentView = views[0];
                break;
            case "Trigger2":
                currentView = views[1];
                break;
            case "Trigger3":
                currentView = views[2];
                break;
            case "Trigger4":
                currentView = views[3];
                break;
            case "Trigger5":
                currentView = views[4];
                break;
            case "Trigger6":
                currentView = views[5];
                break;
            case "Trigger7":
                currentView = views[6];
                break;
            case "Trigger8":
                currentView = views[7];
                break;
            case "Trigger9":
                currentView = views[8];
                break;
            default:
                //currentView = views[0];
                Debug.Log("ERROR");
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = transform.forward * Input.GetAxis("Vertical");
        vel += transform.right * Input.GetAxis("Horizontal");
        // if (vel.magnitude > 1)
        // {
        //     vel.Normalize();
        // }

        //vel.Normalize();

        vel *= speed;
 
        characterController.SimpleMove(vel);
        Debug.Log(characterController.velocity);
    }
}
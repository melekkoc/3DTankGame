using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float move_axsis, turn_axis;
    float moveSpeed=3f;
    float turnSpeed=200f;
    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetComponent<ShootBehavior>().Shoot();
        }

        move_axsis = Input.GetAxis("Vertical");
        turn_axis = Input.GetAxis("Horizontal");

  
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position + transform.forward * move_axsis * moveSpeed * Time.deltaTime );
        Quaternion rot = Quaternion.Euler(0, turn_axis * turnSpeed * Time.deltaTime, 0);
        rigidbody.MoveRotation(transform.rotation * rot);

    }
}


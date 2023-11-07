using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerJoystick : MonoBehaviour
{

    public float speed;              
    public Animator animator;
    public Joystick joystick;
    public bool canMove = true;

    private Rigidbody rb;       
    float moveHorizontal;
    float moveVertical;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!canMove)
        {
           // animator.SetFloat("Speed", 0);
            return;
        }

        moveHorizontal = joystick.Horizontal;
        moveVertical = joystick.Vertical;

        //animator.SetFloat("Horizontal", moveHorizontal);
        //animator.SetFloat("Vertical", moveVertical);
        //animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;
        Vector3 movement = new Vector3(moveHorizontal, 0,moveVertical);
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}


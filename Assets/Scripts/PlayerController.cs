using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;
    [SerializeField]
    private float turnSmooth = 0.1f;

    private Joystick joystick;
    private CharacterController controller;
    private float turnSmoothVelocity;
    private Animator anim;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        joystick = GameObject.FindWithTag("Joystick").GetComponent<Joystick>();
    }

    private void Update()
    {

        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    public void AnimCollect()
    {
        anim.SetTrigger("Attack");
    }
    public void AnimEnd()
    {
        anim.SetTrigger("Jump");
    }

}


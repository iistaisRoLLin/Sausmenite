using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 Velocity;
    public float speed = 3.5f;
    public float sprintSpeed = 5f;
    private bool isGrounded;
    private bool sprinting;
    public float gravity = -9.81f;
    public float jumpHeight = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        Velocity.y += gravity * Time.deltaTime;
        if(isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }
        controller.Move(Velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            Velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = 3.5f;
        }
    }
}

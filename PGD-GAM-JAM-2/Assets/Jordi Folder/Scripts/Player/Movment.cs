using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
                moveDirection = new Vector3(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
                moveDirection *= speed;
                if(Input.GetKey(KeyCode.W))
                {
                    characterController.Move(Vector3.forward * speed * Time.deltaTime);
                }
                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    this.transform.Rotate(0, 20, 0);
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    this.transform.Rotate(0, -20, 0);
                }
            }
            else
            {
                moveDirection = Vector3.zero;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)

        // Move the controller
}

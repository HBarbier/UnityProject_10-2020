using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public CharacterController controller;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Horizontal")) + (transform.right * Input.GetAxis("Vertical"));
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1) * moveSpeed;

        moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
    }

    //Wall jump
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && hit.normal.y < 0.1f)
        { 
            if(Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
    }
}
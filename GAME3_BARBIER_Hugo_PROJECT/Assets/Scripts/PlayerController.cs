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
    public float jump2;

    public float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;

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


        //JUMP
        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            jump2 += Time.deltaTime;
            
            if (Input.GetButtonDown("Jump") && jump2 > 0.3)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                moveDirection.y = jumpForce;
                jump2 = 0;
            }

            if (Input.GetButtonDown("Jump") && jump2 <= 0.3)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                moveDirection.y = jumpForce + 5;
                jump2 = 0;
            }
        }

        if (isJumping == true && Input.GetKey(KeyCode.Space))
        {
            if (jumpTimeCounter > 0)
            {
                moveDirection.y = jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }

            else if (jumpTimeCounter < 0)
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
    }

    //WALL JUMP
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && hit.normal.y < 0.1f)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
    }
}
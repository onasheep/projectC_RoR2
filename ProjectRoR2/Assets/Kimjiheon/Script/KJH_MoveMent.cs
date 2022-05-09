using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_MoveMent : Character
{

    [SerializeField]
    private float ApplySpeed;
    private float gravity = -9.81f;
    private Vector3 moveDirection;
    [SerializeField]
    private float jumpForce = 3.0f;
    public float WalkSpeed = 5.0f;
    public float RunSpeed = 10.0f;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (myCc.isGrounded == false)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
        myCc.Move(moveDirection * ApplySpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
    }
    public void JumpTo()
    {
        if (myCc.isGrounded == true)
        {
            moveDirection.y = jumpForce;
        }
    }
}

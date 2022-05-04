using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSJPlayer : Character
{
    Animator Myanim;
    public enum STATE
    { 
        CREATE,MOVE,ATTACK,DEAD
    }
    public STATE myState = STATE.CREATE;
    //////////////////MoveInput//////////////////
    bool isRun = false;
    public float WalkSpeed = 5.0f;
    public float RunSpeed = 10.0f;
    float ApplySpeed;
    //////////////////JumpInput//////////////////
    int JumpCount = 1;
    public float JumpForce = 10.0f;
    bool isGround = true;
    /////////////////////////////////////////  
    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.MOVE:
                break;
            case STATE.ATTACK:
                break;
            case STATE.DEAD:
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.MOVE:
                PlayerMoving();
                Jump();
                break;
            case STATE.ATTACK:
                break;
            case STATE.DEAD:
                break;
        }
    }
    void Start()
    {
        Myanim = this.GetComponentInChildren<Animator>();
        ApplySpeed = WalkSpeed;
        if (myState == STATE.CREATE)
        {
            ChangeState(STATE.MOVE);
        }
    }

    void Update()
    {
        StateProcess();


    }
    ////////////////////////////////////////Move/////////////////////////////////////////////
    public void PlayerMoving()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            TryRun();
        }
        if (Input.GetKey(KeyCode.W)) 

        {
            this.transform.Translate(Vector3.forward * ApplySpeed * Time.deltaTime);
            Myanim.SetBool("IsWalking", true);
        }
        if (Input.GetKey(KeyCode.S))

        { this.transform.Translate(Vector3.back * ApplySpeed * Time.deltaTime); }

        if (Input.GetKey(KeyCode.D))

        { this.transform.Translate(Vector3.right * ApplySpeed * Time.deltaTime); }

        if (Input.GetKey(KeyCode.A))

        { this.transform.Translate(Vector3.left * ApplySpeed * Time.deltaTime); }
        

    }
    // Ư�� ���ǿ� �°� �޸���
    public void TryRun()
    {
        if (!isRun && isGround)
        {
            Runing();
        }
        else
        {
            RunningCancel();
        }
    }
    // �޸���
    public void Runing()
    {
        isRun = true;
        ApplySpeed = RunSpeed;       
    }
    // �޸��� ����
    public void RunningCancel()
    {
        isRun = false;
        ApplySpeed = WalkSpeed;     
    }
    // ����
    public void Jump()
    {
        if (isGround)
        {
            JumpCount = 1;
            if (Input.GetKeyDown("space"))
            {
                RunningCancel();
                if (JumpCount == 1)
                    myRigid.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                isGround = false;
                JumpCount = 0;
            }
        }
    }
    //���� ������ ��������
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Ground")
        {
            isGround = true;
            JumpCount = 1;
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////
    
    /*
    Ray ray = new Ray(this.transform.position, -this.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, CollisionOffset + myZoom, CrashMask))
        {
            myZoom = Vector3.Distance(hit.point - ray.direction * CollisionOffset, this.transform.position);
        }
        myCam.localPosition = -Vector3.forward * myZoom;
    */

}

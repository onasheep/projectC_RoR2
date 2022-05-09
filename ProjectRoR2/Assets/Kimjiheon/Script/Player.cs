using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
    public int[] equip = new int[14];
    public enum STATE
    {
        CREATE, PLAY, DEAD
    }
    public STATE myState = STATE.CREATE;
    [SerializeField]
    private Transform myCamArm;
    [SerializeField]
    private Transform myFrontAim;
    [SerializeField]
    private Transform myLeftAim;
    [SerializeField]
    private Transform myRightAim;
    [SerializeField]
    private Transform myChest;
    [SerializeField]
    private Transform myPelvis;
    [SerializeField]
    private LayerMask Onground;
    float GroundDist = 0f;
    //State//
    public CharacterData myCharacterdata;
    public CharacterStat myCharacterStat;
    //////////////////MoveInput//////////////////     
    Vector3 moveDir;
    Vector3 lookForward;
    Vector3 lookRight;
    Vector3 moveInput;
    //////////////////JumpInput//////////////////

    /////////////////////////////////////////
    Coroutine cooltime;
    public float RollTimeCheck;
    public float cooltimeCheck = 0;
    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.PLAY:
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
            case STATE.PLAY:
                Jump();
                TryRun();
                TryRoll();
                break;
            case STATE.DEAD:
                break;
        }
    }
    void Start()
    {
        GroundDist = myCapsuleCol.bounds.extents.y + 0.05f;
        myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;       
        if (myState == STATE.CREATE)
        {
            ChangeState(STATE.PLAY);
        }
    }
    private void FixedUpdate()
    {
        if (myState == STATE.PLAY)
        {
            PlayerMovement();
            
        }
    }
    
    private void LateUpdate()
    {        
        if (myCharacterdata.isLookAround)
        {
            myChest.transform.LookAt(myFrontAim);
        }    
    } 
    void Update()
    {        
        StateProcess();
        GetInput();
        //Debug.DrawRay(myCamArm.transform.position, myFrontAim.transform.position - myCamArm.transform.position, Color.red);
        //Debug.DrawRay(transform.position, Vector3.down, Color.red);
    }
    void GetInput()
    {
        //키보드 WSAD 값
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical")).normalized;
        //카메라 정면 방향값
        lookForward = new Vector3(myCamArm.forward.x, Mathf.Epsilon, myCamArm.forward.z).normalized;
        //카메라 오른쪽 방향값
        lookRight = new Vector3(myCamArm.right.x, Mathf.Epsilon, myCamArm.right.z).normalized;
        //카메라 기준/키보드 값으로 이동 방향
        moveDir = lookForward * moveInput.z + lookRight * moveInput.x;
        //움직임 판단 값
        myCharacterdata.ismove = moveInput.magnitude != 0.0f;
        //공격 판단 값
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(DelayTime(3.0f));
        }
        //정면 방향으로 이동 판단 값
        myCharacterdata.onforward = Input.GetKey(KeyCode.W);
        //애니메이션 공격 판단 값
        myAnim.SetBool("isAttack", myCharacterdata.isAttack);
        //애니메이션 움직임 판단 값
        myAnim.SetBool("ismove", myCharacterdata.ismove);       
        //myAnim.SetBool("isRoll", myCharacterdata.isRoll);
        RollTimeCheck += Time.deltaTime;
        Debug.Log(RollTimeCheck);
        
    }
    IEnumerator DelayTime(float cool)
    {
        while (cool > 0.0f)
        {
            cool -= Time.deltaTime;
            myCharacterdata.isAttack = true;
            yield return new WaitForFixedUpdate();
        }
        myCharacterdata.isAttack = false;
    }
    Coroutine RotRoutine = null;
    IEnumerator Rotating(Vector3 pos)
    {
        Vector3 dir = (pos - this.transform.position).normalized;
        GameUtil.CalAngle(myAnim.transform.forward, dir, myAnim.transform.right, out ROTATEDATA data);
        while (data.Angle > Mathf.Epsilon)
        {
            float delta = 360.0f * Time.deltaTime;
            if (data.Angle <= delta)
            {
                delta = data.Angle;
            }
            myAnim.transform.Rotate(Vector3.up * delta * data.Dir);
            data.Angle -= delta;
            yield return null;
        }
        RotRoutine = null;
    }
    ////////////////////////////////////////Move/////////////////////////////////////////////
    public void PlayerMovement()
    {
        if (RotRoutine != null) StopCoroutine(RotRoutine);
        //공격할 때
        if (myCharacterdata.isAttack)
        {    
            //공격 할때 달리기 취소
            RunningCancel();
            //카메라 기준 조준 방향으로 전환
            transform.rotation = Quaternion.LookRotation(new Vector3(lookForward.x, Mathf.Epsilon, lookForward.z));
            
            myAnim.SetFloat("Dir.x", moveInput.x, 0.1f, Time.deltaTime);
            myAnim.SetFloat("Dir.y", moveInput.z, 0.1f, Time.deltaTime);
                     
        }
        //공격하지 않을 때
        else
        {           
            //this.transform.LookAt(this.transform.position + moveDir);           
        }
        //움직일 때       
        if (myCharacterdata.ismove)
        {
            if (!myCharacterdata.isAttack) RotRoutine = StartCoroutine(Rotating(this.transform.position + moveDir));       
            myAnim.transform.position += myCharacterStat.ApplySpeed * Time.deltaTime * moveDir;
            //Debug.Log(myCharacterStat.ApplySpeed);
        }
    }
    // 특정 조건에 맞게 달리기
    public void TryRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!myCharacterdata.isRun)
            {
                Runing();
            }
            else
            {
                RunningCancel();
            }
        }
        else if (!myCharacterdata.onforward)
        {
            RunningCancel();
        }
    }
    // 달리기
    public void Runing()
    {
        myCharacterdata.isRun = true;
        myCharacterdata.isLookAround = false;
        myAnim.SetBool("isSprint", true);
        myAnim.SetTrigger("Sprint");
        myCharacterStat.ApplySpeed = myCharacterStat.RunSpeed;       
    }
    // 달리기 중지
    public void RunningCancel()
    {
        myCharacterdata.isRun = false;
        myCharacterdata.isLookAround = true;
        myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
        myAnim.SetBool("isSprint", false);       
    }
    public void TryRoll()
    {       
        if (Input.GetKeyDown(KeyCode.LeftShift) && myCharacterdata.isRoll == false && RollTimeCheck >= myCharacterStat.RollTime)
        {
            myCharacterdata.isRoll = true;
            RollTimeCheck = 0;
            if (myCharacterdata.isAttack)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    Roll("RollForward");
                }
                if (Input.GetKey(KeyCode.S))
                {
                    Roll("RollBackward");
                }
                if (Input.GetKey(KeyCode.A))
                {
                    Roll("RollLeft");
                }
                if (Input.GetKey(KeyCode.D))
                {
                    Roll("RollRight");
                }
            }
            else
            {
                Roll("RollForward");
            }
        }
        else if (RollTimeCheck >= myCharacterStat.RollTime)
        {
            myCharacterdata.isRoll = false;
        }
    }

    public void Roll(string Name)
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 5.0f))
        {
            myAnim.SetTrigger(Name);
            //if (cooltime != null) StopCoroutine(cooltime);
            //cooltime = StartCoroutine(Rolling(hit.point));
            transform.position = hit.point;
        }
        else
        {
            myAnim.SetTrigger(Name);
            if(cooltime != null) StopCoroutine(cooltime);
            cooltime = StartCoroutine(Rolling(moveDir * 3.0f));
            //transform.position += moveDir * 5.0f;
        }
    }
    IEnumerator Rolling(Vector3 dir)
    {
        float dist = dir.magnitude;
        while (dist > Mathf.Epsilon)
        {
            float delta = 20.0f * Time.deltaTime;
            if (dist < delta)
            {
                delta = dist;
            }
            else
            {
                transform.Translate(dir * delta, Space.World);
            }
            dist -= delta;
            yield return null;
        }
        cooltime = null;
    }
    // 점프
    public void Jump()
    {   
        
        if (Input.GetKeyDown("space") /*&& myCharacterdata.isGround*/)
        {
            if (myCharacterStat.JumpCount < myCharacterStat.JumpItem)
            {
                myCharacterdata.isLookAround = false;
                myAnim.SetTrigger("Jump");
                myAnim.SetBool("Jumping", true);
                RunningCancel();
                myRigid.velocity = Vector3.up * myCharacterStat.JumpForce;
                myCharacterStat.JumpCount++;
            }           
        }       
    }
    //땅에 닿을때 점프가능
    private void OnCollisionEnter(Collision col)
    {
        if ((Onground & (1 << col.gameObject.layer)) > 0)
        {
            //myCharacterdata.isGround = true;
            myAnim.SetBool("OnAir", false);
            myAnim.SetBool("Jumping", false);
            myCharacterStat.JumpCount = 0;
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if ((Onground & (1 << col.gameObject.layer)) > 0)
        {
            //myCharacterdata.isGround = false;
            
            myAnim.SetBool("OnAir", true);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////
    
}

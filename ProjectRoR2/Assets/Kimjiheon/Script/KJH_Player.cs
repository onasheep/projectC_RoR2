using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KJH_Player : Character
{
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
    [SerializeField]
    private Transform ShotPosR;
    [SerializeField]
    private Transform ShotPosL;
    //State//
    public KJH_CharacterData myCharacterdata;
    public KJH_CharacterStat myCharacterStat;
    //////////////////MoveInput//////////////////     
    Vector3 moveDir;
    Vector3 lookForward;
    Vector3 lookRight;
    Vector3 moveInput;
    Vector3 camAngle;
    Vector2 mouseDelta;
    //////////////////JumpInput//////////////////

    /////////////////////////////////////////
    Coroutine cooltime;
    float RollTimeCheck;
    float AttackTimeCheck;
    float RMBTimeCheck;
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
                Attack();
                break;
            case STATE.DEAD:
                break;
        }
    }
    void Start()
    {
        myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
        RollTimeCheck = myCharacterStat.RollTime;
        AttackTimeCheck = myCharacterStat.AttackDelay;
        RMBTimeCheck = myCharacterStat.RMBTime;
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
            LookAround();
        }
    }
    public void LookAround()
    {
        float Angle = Vector3.Angle(myChest.forward, -myPelvis.forward);
        float y = camAngle.y;
        float x = camAngle.x - mouseDelta.y;
        if (x < 180.0f)
        {
            x = Mathf.Clamp(x, -1.0f, 90.0f);
        }
        else
        {
            x = Mathf.Clamp(x, 295.0f, 361f);
        }
        /*
        if (Angle < 180.0f)
        {
            Angle = Mathf.Clamp(x, -1.0f, 60.0f);
        }
        else
        {
            Angle = Mathf.Clamp(x, 295.0f, 361f);
        }
        */
        myChest.transform.LookAt(myFrontAim);
        //myChest.transform.rotation = Quaternion.Euler(x, y, camAngle.z);
        Debug.Log(camAngle);
    }
    void Update()
    {        
        StateProcess();
        GroundCheck();       
        GetInput();
        //Debug.DrawRay(myCamArm.transform.position, myFrontAim.transform.position - myCamArm.transform.position, Color.red);
        //Debug.DrawRay(transform.position, Vector3.down, Color.red);
    }
    void GetInput()
    {
        mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        camAngle = myChest.transform.rotation.eulerAngles;
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
        //쿨타임 시간 체크
        RollTimeCheck += Time.deltaTime;
        AttackTimeCheck += Time.deltaTime;
        RMBTimeCheck += Time.deltaTime;
        //Debug.Log(RollTimeCheck);
        
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
        KJH_GameUtil.CalAngle(myAnim.transform.forward, dir, myAnim.transform.right, out KJH_ROTATEDATA data);
        while (data.Angle > Mathf.Epsilon)
        {
            float delta = 360.0f * Time.deltaTime;
            if (data.Angle <= delta)
            {
                delta = data.Angle;
            }
            transform.Rotate(Vector3.up * delta * data.Dir);
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
            //myChest.transform.rotation = Quaternion.LookRotation(new Vector3(lookForward.x, Mathf.Epsilon, lookForward.z));
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
            transform.position += myCharacterStat.ApplySpeed * Time.deltaTime * moveDir;
            //Debug.Log(myCharacterStat.ApplySpeed);
        }
    }
    // 특정 조건에 맞게 달리기
    public void TryRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!myCharacterdata.isRun && myCharacterdata.onforward)
            {
                Runing();
            }
            else
            {
                RunningCancel();
            }
        }
        else if(!myCharacterdata.onforward)
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
    //구르기
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
                    Roll("RollForward", transform.forward);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    Roll("RollBackward",-transform.forward);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    Roll("RollLeft",-transform.right);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    Roll("RollRight",transform.right);
                }
            }
            else
            {
                Roll("RollForward", transform.forward);
                
            }
        }
        else if (RollTimeCheck >= myCharacterStat.RollTime)
        {
            myCharacterdata.isRoll = false;
        }
    }

    public void Roll(string Name,Vector3 dir)
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 3.0f))
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
            cooltime = StartCoroutine(Rolling(dir * 3.0f));
            //transform.position += moveDir * 5.0f;
        }
    }
    
    IEnumerator Rolling(Vector3 dir)
    {
        float dist = dir.magnitude;

        while (dist > Mathf.Epsilon)
        {
            float delta = 20.0f * Time.deltaTime;
            myCharacterdata.isLookAround = false;
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
        myCharacterdata.isLookAround = true;
        Runing();
        cooltime = null;
    }
    // 점프
    public void Jump()
    {          
        if (Input.GetKeyDown("space"))
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
    //땅체크
    void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(myAnim.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.down, out hit, 0.6f, Onground))
        {
            myAnim.SetBool("OnAir", false);
            myAnim.SetBool("Jumping", false);
            myCharacterStat.JumpCount = 0;
        }
        else
        {
            myAnim.SetBool("OnAir", true);
        }
    }
    void Attack()
    {
        LMB();
        RMB();
        RKB();
    }
    //왼쪽마우스공격
    void LMB()
    {
        if (Input.GetMouseButton(0))
        {
            if (myCharacterdata.GunSwitch == false && AttackTimeCheck >= myCharacterStat.AttackDelay)
            {
                AttackTimeCheck = 0;
                myAnim.SetTrigger("LMBAtkR");
                myCharacterdata.GunSwitch = true;
                StartCoroutine(ShotBullet(ShotPosR, "BulletMouse0"));
            }
            if (myCharacterdata.GunSwitch == true && AttackTimeCheck >= myCharacterStat.AttackDelay)
            {
                AttackTimeCheck = 0;
                myAnim.SetTrigger("LMBAtkL");
                myCharacterdata.GunSwitch = false;
                StartCoroutine(ShotBullet(ShotPosL, "BulletMouse0"));
            }
        }
    }
    IEnumerator ShotBullet(Transform dir, string BulletName)
    {
        GameObject intantBullet = Instantiate(Resources.Load("Prefeb/"+ BulletName), dir.position, Quaternion.identity) as GameObject;
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = dir.forward * 70;
        yield return null;
    }
    //오른쪽 마우스 공격
    void RMB()
    {
        //강공격
        if (Input.GetMouseButton(1))
        {
            if (RMBTimeCheck >= myCharacterStat.RMBTime)
            {
                RMBTimeCheck = 0;
                myAnim.SetTrigger("LMBAtkR"); 
                StartCoroutine(ShotBullet(ShotPosR, "BulletMouse1"));
            }
        }
    }
    void RKB()
    { 
        //특수공격
    }
    ////////////////////////////////////////////////////////////////////////////////////////

}

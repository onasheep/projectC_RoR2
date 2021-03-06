using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KJH_Player : Character
{
    // ESC 메뉴 정지를 위한 PAUSE 추가 
    public enum STATE
    {
        CREATE, PLAY, PAUSE, DEAD
    }
    public STATE myState = STATE.CREATE;
    [SerializeField]
    private Transform myCamArm;
    [SerializeField]
    private LayerMask Onground;

    [Header("Character Joint")]
    [SerializeField] private Transform myChest;
    [SerializeField] private Transform myPelvis;
    
    [Header("Aim")]
    [SerializeField] private Transform ShotPosR;
    [SerializeField] private Transform ShotPosL;
    [SerializeField] private Transform myFrontAim;
    [SerializeField] private Transform myLeftAim;
    [SerializeField] private Transform myRightAim;
    //State//
    public KJH_CharacterData myCharacterdata;
    public KJH_CharacterStat myCharacterStat;
    public AttackSystem myAttackSystem = null;
    //////////////////MoveInput//////////////////     
    Vector3 moveDir;
    Vector3 lookForward;
    Vector3 lookRight;
    Vector3 moveInput;
    Vector3 camAngle;
    Vector2 mouseDelta;
    //////////////////Time//////////////////
    Coroutine Rollingtime;
    float RollTimeCheck;
    float AttackTimeCheck;
    float RMBTimeCheck;
    float RKBTimeCheck;
    float jumpmotionTime;
    // UI 쿨타임 연동을 위한 추가
    public KeyInputControl myKeyControl = null;
    public GameOverCanvas myGameOver = null;

    public void ChangeState(STATE s)
    {
        
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.PLAY:
                break;
            case STATE.PAUSE:
                break;
            case STATE.DEAD:
                myGameOver.GameOver();
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
            case STATE.PAUSE:
                break;
            case STATE.DEAD:
                break;
        }
    }
    void Start()
    {
        // 쿨타임 및 엔딩씬 추가
        myKeyControl = KeyInputControl.KeyInputMachine;
        myGameOver = GameOverCanvas.GameOverMachine;

        CooltimeReset();
        if (myState == STATE.CREATE)
        {
            ChangeState(STATE.PLAY);
        }
    }
    private void LateUpdate()
    {
        if (myCharacterdata.isLookAround)
        {
            LookAround();
        }    
    }
    private void FixedUpdate()
    {
        PlayerMovement();
        GroundCheck();
        ForwardCheck();
    }
    void Update()
    {        
        StateProcess();
        //GroundCheck();
        //ForwardCheck();
        GetInput();
    }
    //값기본설정
    void CooltimeReset()
    {
        myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
        RollTimeCheck = myCharacterStat.RollTime;
        AttackTimeCheck = myCharacterStat.AttackDelay;
        RMBTimeCheck = myCharacterStat.RMBTime;
        RKBTimeCheck = myCharacterStat.RKBTime;
    }
    //정보입력
    void GetInput()
    {
        mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        camAngle = myCamArm.transform.rotation.eulerAngles;
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
        RKBTimeCheck += Time.deltaTime;
    }
      
    #region AttackPositioning
    Coroutine DT = null;
    IEnumerator DelayTime(float cool)
    {
        while (cool > 0.0f)
        {
            cool -= Time.deltaTime;
            myCharacterdata.isAttack = true;
            yield return new WaitForFixedUpdate();
        }
        myCharacterdata.isAttack = false;
        DT = null;
    }
    #endregion
    #region CheckPlace
    //허리체크
    public void LookAround()
    {
        float y = Vector3.Angle(-myPelvis.forward, myChest.forward);
        float x = camAngle.x - mouseDelta.y;
        if (x < 180.0f)
        {
            x = Mathf.Clamp(x, -1.0f, 70.0f);
        }
        else
        {
            x = Mathf.Clamp(x, 295.0f, 361f);
        }
        myChest.transform.rotation = Quaternion.Euler(x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
    //정면체크
    void ForwardCheck()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0.0f, 1f, 0.0f), transform.forward, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0.0f, 1f, 0.0f), transform.forward + -transform.right, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0.0f, 1f, 0.0f), transform.forward + transform.right, Color.red);
        Ray rayforward = new Ray(transform.position + new Vector3(0.0f, 1f, 0.0f), transform.forward);
        Ray rayleft = new Ray(transform.position + new Vector3(0.0f, 1f, 0.0f), transform.forward + -transform.right);
        Ray rayright = new Ray(transform.position + new Vector3(0.0f, 1f, 0.0f), transform.forward + transform.right);
        if (Physics.Raycast(rayforward, out hit, 0.3f) || Physics.Raycast(rayleft, out hit, 0.3f) || Physics.Raycast(rayright, out hit, 0.3f))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall") || hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                if (Physics.Raycast(rayforward, out hit, 0.3f))
                {
                    transform.position = hit.point - new Vector3(0.0f, 1f, 0.0f) - rayforward.direction  * 0.38f;
                }
                else if (Physics.Raycast(rayleft, out hit, 0.3f))
                {
                    transform.position = hit.point - new Vector3(0.0f, 1f, 0.0f) - rayleft.direction  * 0.25f;
                }
                else if (Physics.Raycast(rayright, out hit, 0.3f))
                {
                    transform.position = hit.point - new Vector3(0.0f, 1f, 0.0f) - rayright.direction  * 0.25f;
                }

            }
        }   
     
    }
    //땅체크
    void GroundCheck()
    {
        //Debug.DrawRay(transform.position + new Vector3(0.0f, 0.0f, 0.3f), Vector3.down, Color.red);
        //Debug.DrawRay(transform.position + new Vector3(0.0f, 0.0f, -0.3f), Vector3.down, Color.red);
        //Debug.DrawRay(transform.position + new Vector3(-0.3f, 0.0f, 0.0f), Vector3.down, Color.red);
        //Debug.DrawRay(transform.position + new Vector3(0.3f, 0.0f, 0.0f), Vector3.down, Color.red);
        RaycastHit hit;
        Ray rayCenter = new Ray(transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.down);
        Ray rayforward = new Ray(transform.position + new Vector3(0.0f, 0.5f, 0.3f), Vector3.down);
        Ray raybackward = new Ray(transform.position + new Vector3(0.0f, 0.5f, -0.3f), Vector3.down);
        Ray rayleft = new Ray(transform.position + new Vector3(-0.3f, 0.5f, 0.0f), Vector3.down);
        Ray rayright = new Ray(transform.position + new Vector3(0.3f, 0.5f, 0.0f), Vector3.down);
        if (Physics.Raycast(rayCenter, out hit, 0.6f, Onground) || Physics.Raycast(rayforward, 0.6f, Onground) || 
            Physics.Raycast(raybackward, 0.6f, Onground) || Physics.Raycast(rayleft, 0.6f, Onground) || Physics.Raycast(rayright, 0.6f, Onground))       
        {
           
            myAnim.SetBool("OnAir", false);
            jumpmotionTime = 0.0f;
            myAnim.SetBool("Jumping", false);
            myCharacterdata.isGround = true;
            myCharacterStat.JumpCount = 0;
        }
        else 
        {
            if (myCharacterdata.isRoll == false)
            {
                jumpmotionTime += Time.deltaTime * 0.3f;

                myAnim.SetBool("OnAir", true);
                myAnim.SetFloat("JumpTime", jumpmotionTime);
            }
            myCharacterdata.isGround = false;
        }
    }
    #endregion
    #region CommonMove
    //기본움직임
    public void PlayerMovement()
    {
        //if (RotRoutine != null) StopCoroutine(RotRoutine);
        //공격할 때
        if (myCharacterdata.isAttack)
        {    
            //공격 할때 달리기 취소
            RunningCancel();
            //카메라 기준 조준 방향으로 전환
            //myChest.transform.rotation = Quaternion.LookRotation(new Vector3(lookForward.x, Mathf.Epsilon, lookForward.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(lookForward.x, Mathf.Epsilon, lookForward.z)), Time.deltaTime * 30.0f);
            myAnim.SetFloat("Dir.x", moveInput.x, 0.1f, Time.deltaTime);
            myAnim.SetFloat("Dir.y", moveInput.z, 0.1f, Time.deltaTime);
                     
        }
        //공격하지 않을 때
        else
        {
            //this.transform.LookAt(this.transform.position + moveDir);
            
        }
        //움직일 때
        if (myCharacterdata.ismove && myCharacterdata.isRoll == false)
        {
            if (!myCharacterdata.isAttack) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 10.0f);
            if (myCharacterdata.isBorder) myCharacterStat.ApplySpeed = 0.0f;
            transform.position += myCharacterStat.ApplySpeed * Time.deltaTime * moveDir;
            //Debug.Log(myCharacterStat.ApplySpeed);
        }
    }
    #endregion
    #region Run
    // 특정 조건에 맞게 달리기
    public void TryRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!myCharacterdata.isRun && myCharacterdata.onforward && myCharacterdata.isGround == true)
            {
                if (DT != null) StopCoroutine(DT);
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
    #endregion
    #region Roll
    //구르기
    public void TryRoll()
    {       
        if (Input.GetKeyDown(KeyCode.LeftShift) && myCharacterdata.isRoll == false && RollTimeCheck >= myCharacterStat.RollTime)
        {
            myKeyControl.ShiftCoolTime(myCharacterStat.RollTime);
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
        //myCharacterdata.isRoll = false;
        //myCharacterdata.isLookAround = true;
    }

    public void Roll(string Name,Vector3 dir)
    {
        myAnim.SetBool("OnAir", false);
        myCharacterdata.isRoll = true;
        if (Physics.Raycast(transform.position + new Vector3(0.0f, 0.5f, 0.0f), dir, out RaycastHit hit, 3.5f, Onground))
        {         
            myAnim.SetTrigger(Name);
            transform.position = hit.point;
            myCharacterdata.isRoll = false;       
        }
        else
        {           
            myAnim.SetTrigger(Name);
            if(Rollingtime != null) StopCoroutine(Rollingtime);
            Rollingtime = StartCoroutine(Rolling(dir * 3.5f));
        }
    }
    
    IEnumerator Rolling(Vector3 dir)
    {
        float dist = dir.magnitude;
        yield return new WaitForSeconds(0.1f);
        while (dist > Mathf.Epsilon)
        {
            float delta = 15.0f * Time.deltaTime;
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
        yield return new WaitForSeconds(0.1f);
        myCharacterdata.isLookAround = true;
        myCharacterdata.isRoll = false;
        if(myCharacterdata.isGround == true) Runing();
        Rollingtime = null;
    }
    #endregion
    #region Jump
    // 점프
    public void Jump()
    {          
        if (Input.GetKeyDown("space"))
        {
            if (myCharacterStat.JumpCount < myCharacterStat.JumpItem)
            {              
                myCharacterdata.isLookAround = false;
                myAnim.SetTrigger("Jump");
                RunningCancel();
                myRigid.velocity = Vector3.up * myCharacterStat.JumpForce;
                Invoke("JumpDelayCheck", 0.1f);
            }           
        }       
    }
    void JumpDelayCheck()
    {
        myCharacterStat.JumpCount++;
    }
    #endregion
    #region AttackType
    //공격함수
    void Attack()
    {
        if (myCharacterdata.isRoll == false)
        {
            LMB();
            RMB();
            RKB();
        }
    }
    //왼쪽마우스공격
    void LMB()
    {
        if (Input.GetMouseButton(0))
        {
            if (myCharacterdata.GunSwitch == false && AttackTimeCheck >= myCharacterStat.AttackDelay)
            {
                StartCoroutine(DelayTime(3.0f));
                AttackTimeCheck = 0;
                myAnim.SetTrigger("LMBAtkR");
                myCharacterdata.GunSwitch = true;
                //StartCoroutine(ShotBullet(ShotPosR, "BulletMouse0"));
                myAttackSystem.TwoStepRaycast(ShotPosR, "BulletMouse0");             
            }
            if (myCharacterdata.GunSwitch == true && AttackTimeCheck >= myCharacterStat.AttackDelay)
            {
                StartCoroutine(DelayTime(3.0f));
                AttackTimeCheck = 0;
                myAnim.SetTrigger("LMBAtkL");
                myCharacterdata.GunSwitch = false;
                //StartCoroutine(ShotBullet(ShotPosL, "BulletMouse0"));
                myAttackSystem.TwoStepRaycast(ShotPosL, "BulletMouse0");
                
            }
        }
    }   
    //오른쪽 마우스 공격  
    void RMB()
    {
        //강공격
        if (Input.GetMouseButton(1))
        {
            if (RMBTimeCheck >= myCharacterStat.RMBTime)
            {
                //UI연동
                myKeyControl.M2CoolTime(myCharacterStat.RMBTime);
                StartCoroutine(DelayTime(3.0f));
                RMBTimeCheck = 0;
                myAnim.SetTrigger("LMBAtkR");
                //StartCoroutine(ShotBullet(ShotPosR, "BulletMouse1"));
                myAttackSystem.TwoStepRaycast(ShotPosR, "BulletMouse1");
            }
        }
    }
    //R키 특수공격
    void RKB()
    {
        //특수공격
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (RKBTimeCheck >= myCharacterStat.RKBTime)
            {
                //UI연동
                myKeyControl.RCoolTime(myCharacterStat.RKBTime);

                StartCoroutine(DelayTime(3.0f));
                RKBTimeCheck = 0;
                StartCoroutine(SpecialAtk());
            }
        }
    }
    IEnumerator SpecialAtk()
    {
        for (int i = 0; i < myCharacterStat.RKBNumber; i++)
        {
            yield return new WaitForSeconds(0.1f);
            myAnim.SetTrigger("LMBAtkR");
            myAttackSystem.TwoStepRaycast(ShotPosR, "BulletMouse0"); 
        }
    }
    #endregion
    public void TakeDamage(float damage)
    {
        bool isDie = myAttackSystem.DecreaseHP(damage);
        if (isDie)
        {
            Debug.Log("Die");
            ChangeState(STATE.DEAD);
        }
    }
}

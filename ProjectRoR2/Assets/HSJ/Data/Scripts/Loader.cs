using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Loader : Character
{
    public LayerMask CrashMask;
    public LayerMask AttackMask;
    public enum STATE
    {
        CREATE, PLAY, DEAD
    }
    public STATE myState = STATE.CREATE;
    //State//
    bool isRun = false;
    bool isGround = true;
    //////////////////MoveInput//////////////////
    Vector3 Dir;
    float Dist;
    
    //////////////////JumpInput//////////////////
    float forceGarvity = 0.0f;
    float delta;
    //////////////////AttackInput//////////////////     
    public Transform HookStart;
    public GameObject LoaderFist;
    public Transform MySpringArm;
    public Transform LeftArm;
    public Transform RightArm;
    /// <summary>
    /// 스탯관련 
    /// </summary> 
    [SerializeField]
    private Transform myFrontAim;
    [SerializeField]
    private LayerMask Onground;

    public KJH_CharacterData myCharacterdata;
    public KJH_CharacterStat myCharacterStat;

    // 쿨타임 UI;
    public KeyInputControl myKeyControl;
    

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
                Attack();
                break;
            case STATE.DEAD:
                break;
        }
    }
    public void Start()
    {
        myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
        myCharacterStat.LM2CoolTime = 6.0f;
        myCharacterStat.LShiftCoolTime = 6.0f;
        myCharacterStat.LRCoolTime = 6.0f;
        if (myState == STATE.CREATE)
        {
            ChangeState(STATE.PLAY);
        }

        this.GetComponentInChildren<ComboEvent>().ComboCheck += (value) => Comboable = value;

        // 캐릭터로 부터 화면 정중앙으로 나아가는 선


    }
    private void FixedUpdate()
    {
        if (myState == STATE.PLAY)
        {
            PlayerCrashCheck();
            PlayerMoving();

        }
    }
    private void LateUpdate()
    {
        //if (myCharacterdata.isLookAround)
        //{
        //    LookAround();
        //}
    }
    //public void LookAround()
    //{
    //    float Angle = Vector3.Angle(myChest.forward, -myPelvis.forward);
    //    float y = camAngle.y;
    //    float x = camAngle.x - mouseDelta.y;
    //    if (x < 180.0f)
    //    {
    //        x = Mathf.Clamp(x, -1.0f, 90.0f);
    //    }
    //    else
    //    {
    //        x = Mathf.Clamp(x, 295.0f, 361f);
    //    }
    //    /*
    //    if (Angle < 180.0f)
    //    {
    //        Angle = Mathf.Clamp(x, -1.0f, 60.0f);
    //    }
    //    else
    //    {
    //        Angle = Mathf.Clamp(x, 295.0f, 361f);
    //    }
    //    */
    //    myChest.transform.LookAt(myFrontAim);
    //    myChest.transform.rotation = Quaternion.Euler(x, y, camAngle.z);
    //    Debug.Log(camAngle);
    //}
    void Update()
    {
        StateProcess();
        GroundCheck();
        //GetInput();

    }
    void GetInput()
    {
        //mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //camAngle = myChest.transform.rotation.eulerAngles;
        ////키보드 WSAD 값
        //moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        ////카메라 정면 방향값
        //lookForward = new Vector3(myCamArm.forward.x, Mathf.Epsilon, myCamArm.forward.z).normalized;
        ////카메라 오른쪽 방향값
        //lookRight = new Vector3(myCamArm.right.x, Mathf.Epsilon, myCamArm.right.z).normalized;
        ////카메라 기준/키보드 값으로 이동 방향
        //moveDir = lookForward * moveInput.z + lookRight * moveInput.x;
        ////움직임 판단 값
        //myCharacterdata.ismove = moveInput.magnitude != 0.0f;
        ////공격 판단 값
        //if (Input.GetMouseButton(0))
        //{
        //    StartCoroutine(DelayTime(3.0f));
        //}
        //정면 방향으로 이동 판단 값
        //애니메이션 공격 판단 값
        //myAnim.SetBool("isAttack", myCharacterdata.isAttack);
        //애니메이션 움직임 판단 값
        //myAnim.SetBool("ismove", myCharacterdata.ismove);   // 코만도 공격시 회전 범위 적용 로더는 필요없어서 생략
        //쿨타임 시간 체크
        //RollTimeCheck += Time.deltaTime;
        //AttackTimeCheck += Time.deltaTime;
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
  



    /// <summary>
    ///  이동 관련 
    /// </summary>
    /// 
    void PlayerCrashCheck()
    {
        if(Physics.Raycast(this.transform.position + new Vector3(0.0f,0.5f,-0.5f),this.transform.forward,out RaycastHit hit,0.6f,CrashMask))
        {
            
            myRigid.AddForce(Vector3.zero);
        }
    }


    public void PlayerMoving()
    {


        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirY = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirY;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * myCharacterStat.ApplySpeed;
        //스프링암 시점에 따라서 정면 변경
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0.0f, MySpringArm.rotation.eulerAngles.y, 0.0f), Time.deltaTime * 5.0f);
        //이동
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        
        //방향대로 걷는 애니메이션
        myAnim.SetFloat("Dir.x", _moveDirX, 0.1f, Time.deltaTime);
        myAnim.SetFloat("Dir.y", _moveDirY, 0.1f, Time.deltaTime);

        if (myAnim.GetFloat("Dir.y") > 0.8f ) 
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            { 
                Debug.Log("LControl");
                TryRun();
            }

        }
        else
        {
            RunningCancel();
        }
    }
    // 특정 조건에 맞게 달리기
    public void TryRun()
    {
        if (!isRun)
        {
            Runing();
        }
        else
        {
            RunningCancel();
        }
    }
    // 달리기
    public void Runing()
    {
        isRun = true;
        myAnim.SetTrigger("Sprint");
        myAnim.SetBool("isSprint", true);
        myCharacterStat.ApplySpeed = myCharacterStat.RunSpeed;
    }

    // 달리기 중지
    public void RunningCancel()
    {
        isRun = false;
        myAnim.SetBool("isSprint", false);
        myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
    }
    // 점프
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !(myAnim.GetBool("OnAir") == true))
        {
            
                myCharacterdata.isLookAround = false;
                myAnim.SetTrigger("Jump");
                myAnim.SetBool("OnAir", true);
                //myAnim.SetBool("Jumping", true);
                RunningCancel();
                myRigid.velocity = Vector3.up * myCharacterStat.JumpForce;
                
            
        }
    }
    //땅체크
    void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(myAnim.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.down, out hit, 0.6f, Onground))
        {
            myAnim.SetBool("OnAir", false);
            //myAnim.SetBool("Jumping", false);
            myCharacterStat.JumpCount = 0;
            forceGarvity = 0;
            myAnim.SetBool("IsRun", true);
            
        }
        else
        {
            
            myAnim.SetBool("OnAir", true);
            myAnim.SetBool("IsRun", false);
            // 중력 조정           

            
                delta = forceGarvity * 5.0f * Time.deltaTime;
                forceGarvity ++;
                myRigid.AddForce(Vector3.down * delta, ForceMode.Force);
            Debug.Log(delta);
               
            
           
            //Debug.Log(delta);



        }
    }
  


    /// <summary>
    /// 공격 관련 
    /// </summary>
    public void Attack()
    {


        // LMB
        LMB();

        // RMB;
        RMB();
        // Shift
        Shift();

        // R
        R();
    }

    IEnumerator AttackCool(float cool)
    {
        yield return new WaitForSeconds(cool * Time.deltaTime);
    }
    bool Comboable = false;
    void LMB()
    {
        
        if (!myAnim.GetBool("IsLMBR") && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AttackCool(10.0f));
            myAnim.SetBool("isSprint", false);
            myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
            myAnim.SetTrigger("LMBAtkR");
            
        }
        if (myAnim.GetBool("IsLMBR") && Comboable && !myAnim.GetBool("IsLMBL"))
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                myAnim.SetBool("isSprint", false);
                myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
                myAnim.SetTrigger("LMBAtkL");
                
            }
        }
        
    }
    
    void RMB()
    {

        if (Input.GetMouseButtonDown(1))
        {
            myKeyControl.LM2CoolTime(myCharacterStat.LM2CoolTime);
            myAnim.SetBool("IsRMB", false);
            myAnim.SetTrigger("RMB");
            //주먹 생성
            GameObject obj = Instantiate(LoaderFist, HookStart);
            //부모해제 후 캐릭터 각도로 주먹 각도 변경
            obj.GetComponent<Transform>().parent = null;
            obj.GetComponent<Transform>().transform.eulerAngles = this.transform.eulerAngles;

            //에임 방향으로 주먹 발사
            obj.GetComponent<Rigidbody>().AddForce(MySpringArm.forward * 80.0f, ForceMode.VelocityChange);

            //로더의 오브젝트에 스프링조인트를 생성하고 오브젝트인 주먹과 연결함

            _SJoint.connectedBody = obj.GetComponent<Rigidbody>();
            _SJoint.maxDistance = 10.0f;
            

            // 주먹 되돌아오게 만듬 
            //if (!(MySpringArm.position - S < 10.0f))
            //    {

            //}
        }
        //if (Input.GetMouseButton(1))
        //{
        //    myAnim.SetBool("IsFistOnCollid", true);
        //}

        if (Input.GetMouseButtonUp(1))
        {
            // 주먹 프리팹을 이름으로 찾아서 가져온뒤
            GameObject LHK = GameObject.Find("LoaderHook(Clone)");
            // 로더와 주먹 거리를 재서 
            float Dist = (LHK.transform.position - this.transform.position).magnitude;
            LHK.GetComponent<Rigidbody>().AddForce(-MySpringArm.forward * 80.0f, ForceMode.VelocityChange);

            Debug.Log(Dist);

            myAnim.SetBool("IsRMB", true);
            Destroy(LHK);
            //_SJoint.
        }



    }
    void Shift()
    {


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            myAnim.SetBool("IsShift", false);
            myAnim.SetTrigger("ShiftAtk");
            if (Physics.Raycast(myAnim.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.down, out RaycastHit hit, 0.6f, Onground))
            {
                myAnim.SetBool("OnAir", false);


            }
            else myAnim.SetBool("OnAir", true);
        }


        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            myKeyControl.LShiftCoolTime(myCharacterStat.LShiftCoolTime);

           
            myAnim.SetBool("IsShift", true);
            myRigid.AddForce(Vector3.zero);
            myRigid.AddForce(MySpringArm.forward * 150.0f, ForceMode.Impulse);
            if (Physics.Raycast(myAnim.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.down, out RaycastHit hit, 0.6f, Onground))
            {
                myAnim.SetBool("OnAir", false);


            }
            else myAnim.SetBool("OnAir", true);
        }

    }
    bool IsRCheck = false;
    void R()
    {
        if (Input.GetKeyDown(KeyCode.R) && myAnim.GetBool("OnAir") == false)
        {
            IsRCheck = true;
            myKeyControl.LRCoolTime(myCharacterStat.LRCoolTime);           
            
            myRigid.AddForce(Vector3.up * 10.0f, ForceMode.VelocityChange);
            myAnim.SetTrigger("RAtk");


            myAnim.SetBool("OnAir", true);
            


         

        }
        if (Input.GetKeyDown(KeyCode.R) && myAnim.GetBool("OnAir") == true)
        {
            myRigid.AddForce(Vector3.up * 10.0f, ForceMode.VelocityChange);
            myAnim.SetTrigger("RAtk");
        }



        myAnim.SetBool("Run", true);

    }

}
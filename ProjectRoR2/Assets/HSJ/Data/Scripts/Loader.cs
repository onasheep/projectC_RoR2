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
    //////////////////MoveInput//////////////////  
    
    //////////////////JumpInput//////////////////
    float forceGarvity = 0.0f;
    float delta;
    //////////////////AttackInput//////////////////     
    public Transform HookStart;
    public GameObject LoaderFist;
    public Transform MySpringArm;
    public Transform LeftArm;
    public Transform RightArm;
    public GrappleFist myGrapple;

    [SerializeField]
    private Transform myFrontAim;
    [SerializeField]
    private LayerMask Onground;

    // 스탯 가져오기
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
                M1checkT += Time.deltaTime;
                M2checkT += Time.deltaTime;
                ShiftcheckT += Time.deltaTime;
                RcheckT += Time.deltaTime;
                ChargingTime += Time.deltaTime;
                Jump();
                Attack();
                break;
            case STATE.DEAD:
                break;
        }
    }
    public void Start()
    {
        // 쿨타임 잴 float 값 저장
        M1checkT = M1Cool;
        M2checkT = M2Cool;
        ShiftcheckT = ShiftCool;
        RcheckT = RCool;

        myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;       
        
        if (myState == STATE.CREATE)
        {
            ChangeState(STATE.PLAY);
        }

        // 콤보이벤트 함수에서 애니메이션에 콤보체크에 해당 되면 콤보블을 treu 아니면 false를 반환하게 함
        this.GetComponentInChildren<ComboEvent>().ComboCheck += (value) => Comboable = value;

        


    }
    private void FixedUpdate()
    {
        if (myState == STATE.PLAY)
        {
            PlayerCrashCheck();
            PlayerMoving();
            GroundCheck();
        }
    }
    
    void Update()
    {
        StateProcess();
        
        //GetInput();
        Debug.Log(ChargingTime);
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
            forceGarvity++;
            myRigid.AddForce(Vector3.down * delta, ForceMode.Force);
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
        if(ShiftcheckT >= ShiftCool)
        { 
        Shift();
        }
        // R      
        R();


    }


    bool Comboable = false;
    void LMB()
    {

        if (M1checkT >= M1Cool && Input.GetMouseButtonDown(0))
        {


            if (!myAnim.GetBool("IsLMBR"))
            {
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
            M1checkT = 0.0f;
        }

    }

    void RMB()
    {
        if (M2checkT >= M2Cool && Input.GetMouseButtonDown(1))
        {
            
            myAnim.SetBool("IsRMB", true);
            
           
            if (myAnim.GetBool("IsRMB"))
            {
                myAnim.SetTrigger("RMB");
                myKeyControl.LM2CoolTime(M2Cool);
                //주먹 생성
                GameObject obj = Instantiate(LoaderFist, HookStart);
                //부모해제 후 캐릭터 각도로 주먹 각도 변경
                obj.GetComponent<Transform>().parent = null;
                obj.GetComponent<Transform>().transform.eulerAngles = this.transform.eulerAngles;

                //에임 방향으로 주먹 발사
                obj.GetComponent<Rigidbody>().AddForce(MySpringArm.forward * 80.0f, ForceMode.VelocityChange);

                //로더의 오브젝트에 스프링조인트를 생성하고 오브젝트인 주먹과 연결함
                
                
                _SJoint.connectedBody = obj.GetComponent<Rigidbody>();
                _SJoint.maxDistance = 8.0f;



            }

           

            M2checkT = 0.0f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            //// 주먹 프리팹을 이름으로 찾아서 가져온뒤
            GameObject LHK = GameObject.Find("LoaderHook(Clone)");
            // RMB 애니메이션 끝내고 파괴
            myAnim.SetBool("IsRMB", false);
            Destroy(LHK);

        }


    }
    void Shift()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChargingTime = 0.0f;
            myAnim.SetTrigger("ShiftAtk");
            myAnim.SetBool("IsShift", true);
            myAnim.SetBool("IsPunchLoop", true);
            //if (Physics.Raycast(myAnim.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.down, out RaycastHit hit, 0.6f, Onground))
            //{
            //    myAnim.SetBool("OnAir", false);
            //}
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && ChargingTime <= 1.0f)
        {
            myAnim.SetBool("IsPunchLoop", false);
            myRigid.AddForce(Vector3.zero);
            myRigid.AddForce(MySpringArm.forward * 50.0f, ForceMode.Impulse);
            myKeyControl.LShiftCoolTime(ShiftCool);         
            ShiftcheckT = 0.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && 1.0f < ChargingTime && ChargingTime <= 3.0f)
        {
            myAnim.SetBool("IsPunchLoop", false);
            myRigid.AddForce(Vector3.zero);
            myRigid.AddForce(MySpringArm.forward * 100.0f, ForceMode.Impulse);
            myKeyControl.LShiftCoolTime(ShiftCool);
            ShiftcheckT = 0.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && ChargingTime >= 5.0f)
        {
            myAnim.SetBool("IsPunchLoop", false);
            myRigid.AddForce(Vector3.zero);
            myRigid.AddForce(MySpringArm.forward * 150.0f, ForceMode.Impulse);
            myKeyControl.LShiftCoolTime(ShiftCool);
            ShiftcheckT = 0.0f;
        }
        myAnim.SetBool("IsShift", false);
    }

    void ShiftMovement()
    {

    }
    void R()
    {
        if (Input.GetKeyDown(KeyCode.R) && myAnim.GetBool("OnAir") == false)
        {
            myKeyControl.LRCoolTime(RCool);           
            
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
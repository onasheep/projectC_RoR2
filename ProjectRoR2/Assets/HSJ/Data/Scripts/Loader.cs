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
    bool isJump = false;
    //////////////////MoveInput//////////////////  

    //////////////////JumpInput//////////////////
    float forceGarvity = 0.0f;
    float delta;
    float Rtime;

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
    // 사운드 UI;
    public SoundManager mySound;



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
            //PlayerCrashCheck();
            PlayerMoving();
            
        }
    }

    void Update()
    {
        StateProcess();

        //GetInput();
    }






    /// <summary>
    ///  이동 관련 
    /// </summary>
    /// 
    //void PlayerCrashCheck()
    //{
    //    if(Physics.Raycast(this.transform.position + new Vector3(0.0f,0.5f,-0.5f),this.transform.forward,out RaycastHit hit,0.6f,CrashMask))
    //    {

    //        myRigid.AddForce(Vector3.zero);
    //    }
    //}


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

        if (myAnim.GetFloat("Dir.y") > 0.8f)
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
        if (Input.GetKeyDown(KeyCode.Space) && !(myAnim.GetBool("OnAir") == true) && !isJump)
        {
            isJump = true;
            myCharacterdata.isLookAround = false;
            myAnim.SetTrigger("Jump");
            myAnim.SetBool("OnAir", true);
            //myAnim.SetBool("Jumping", true);
            RunningCancel();
            myRigid.velocity = Vector3.up * myCharacterStat.JumpForce;


        }
        isJump = false;
        GroundCheck();
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


        }
        else
        {

            myAnim.SetBool("OnAir", true);

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
        if (ShiftcheckT >= ShiftCool)
        {
            Shift();
        }
        // R      
        R();


    }

    // 공격 판단하고 이펙트 출력 
    //IEnumerator Attacking(Transform target)
    //{
    //    Collider[] Monsters = Physics.OverlapSphere(target.position,
    //        1.0f, 1 << LayerMask.NameToLayer("Monster"));
    //    foreach(Collider mon in Monsters)
    //    {
    //        Instantiate(Effectsource, target.position, Quaternion.identity);
    //        mon.GetComponent<Monster>()?.OnDamage(Damage);
    //    }
        
    //}

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
                mySound.PlaySound("LMB");

            }
            if (myAnim.GetBool("IsLMBR") && Comboable && !myAnim.GetBool("IsLMBL"))
            {

                if (Input.GetMouseButtonDown(0))
                {
                    myAnim.SetBool("isSprint", false);
                    myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
                    myAnim.SetTrigger("LMBAtkL");
                    mySound.PlaySound("LMB");
                }
            }
            M1checkT = 0.0f;
        }

    }

    void RMB()
    {
        if (M2checkT >= M2Cool && Input.GetMouseButtonDown(1) && !myAnim.GetBool("IsRMB"))
        {

            myAnim.SetBool("IsRMB", true);


            if (myAnim.GetBool("IsRMB"))
            {
                myAnim.SetTrigger("RMB");
                mySound.PlaySound("RMB");
                myKeyControl.LM2CoolTime(M2Cool);
                //주먹 생성
                GameObject obj = Instantiate(LoaderFist, HookStart);
                //부모해제 후 캐릭터 각도로 주먹 각도 변경
                obj.transform.parent = null;
                obj.transform.eulerAngles = this.transform.eulerAngles;
                //에임 방향으로 주먹 발사
                obj.GetComponent<GrappleFist>().Dir = MySpringArm.forward;
                obj.GetComponent<GrappleFist>().Lsjoint = _SJoint;
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

        if (ShiftcheckT >= ShiftCool && Input.GetKeyDown(KeyCode.LeftShift) & !myAnim.GetBool("IsShift"))
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
            mySound.PlaySound("Shift");

            myKeyControl.LShiftCoolTime(ShiftCool);
            ShiftcheckT = 0.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && 1.0f < ChargingTime && ChargingTime <= 3.0f)
        {
            myAnim.SetBool("IsPunchLoop", false);
            myRigid.AddForce(Vector3.zero);
            myRigid.AddForce(MySpringArm.forward * 100.0f, ForceMode.Impulse);
            mySound.PlaySound("Shift");
            myKeyControl.LShiftCoolTime(ShiftCool);
            ShiftcheckT = 0.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && ChargingTime >= 3.0f)
        {
            myAnim.SetBool("IsPunchLoop", false);
            myRigid.AddForce(Vector3.zero);
            myRigid.AddForce(MySpringArm.forward * 150.0f, ForceMode.Impulse);
            mySound.PlaySound("Shift");

            myKeyControl.LShiftCoolTime(ShiftCool);
            ShiftcheckT = 0.0f;
        }
        myAnim.SetBool("IsShift", false);
    }


    void R()
    {
        if (RcheckT >= RCool && Input.GetKeyDown(KeyCode.R) && !myAnim.GetBool("IsR"))
        {
            Rtime += Time.deltaTime * 10.0f;           
            myKeyControl.LRCoolTime(RCool);
            myAnim.SetBool("IsR", true);
            Debug.Log(myAnim.GetBool("IsR"));
            myRigid.AddForce(Vector3.up * 20.0f, ForceMode.VelocityChange);
            myAnim.SetBool("OnAir", true);
            myAnim.SetTrigger("RAtk");
            
            Debug.Log(Rtime);
            if (myAnim.GetBool("OnAir") && Rtime >= 0.3f)
            {
                
                myAnim.SetBool("OnAir", false);
                myAnim.SetBool("IsR", false);
                myRigid.AddForce(Vector3.down * 100.0f, ForceMode.VelocityChange);

                
                mySound.PlaySound("R");
                Rtime = 0.0f;

            }
            
            RcheckT = 0.0f;
            Rtime = 0.0f;
            myAnim.SetBool("IsR", false);
        }
        

    }
}
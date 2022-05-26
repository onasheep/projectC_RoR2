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

    // ���� ��������
    public KJH_CharacterData myCharacterdata;
    public KJH_CharacterStat myCharacterStat;

    // ��Ÿ�� UI;
    public KeyInputControl myKeyControl;
    // ���� UI;
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
        // ��Ÿ�� �� float �� ����
        M1checkT = M1Cool;
        M2checkT = M2Cool;
        ShiftcheckT = ShiftCool;
        RcheckT = RCool;

        myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;

        if (myState == STATE.CREATE)
        {
            ChangeState(STATE.PLAY);
        }

        // �޺��̺�Ʈ �Լ����� �ִϸ��̼ǿ� �޺�üũ�� �ش� �Ǹ� �޺����� treu �ƴϸ� false�� ��ȯ�ϰ� ��
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
    ///  �̵� ���� 
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
        //�������� ������ ���� ���� ����
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0.0f, MySpringArm.rotation.eulerAngles.y, 0.0f), Time.deltaTime * 5.0f);
        //�̵�
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);

        //������ �ȴ� �ִϸ��̼�
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
    // Ư�� ���ǿ� �°� �޸���
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
    // �޸���
    public void Runing()
    {
        isRun = true;
        myAnim.SetTrigger("Sprint");
        myAnim.SetBool("isSprint", true);
        myCharacterStat.ApplySpeed = myCharacterStat.RunSpeed;
    }

    // �޸��� ����
    public void RunningCancel()
    {
        isRun = false;
        myAnim.SetBool("isSprint", false);
        myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
    }
    // ����
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
    //��üũ
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

            // �߷� ����           

            delta = forceGarvity * 5.0f * Time.deltaTime;
            forceGarvity++;
            myRigid.AddForce(Vector3.down * delta, ForceMode.Force);
        }
    }



    /// <summary>
    /// ���� ���� 
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

    // ���� �Ǵ��ϰ� ����Ʈ ��� 
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
                //�ָ� ����
                GameObject obj = Instantiate(LoaderFist, HookStart);
                //�θ����� �� ĳ���� ������ �ָ� ���� ����
                obj.transform.parent = null;
                obj.transform.eulerAngles = this.transform.eulerAngles;
                //���� �������� �ָ� �߻�
                obj.GetComponent<GrappleFist>().Dir = MySpringArm.forward;
                obj.GetComponent<GrappleFist>().Lsjoint = _SJoint;
                //�δ��� ������Ʈ�� ����������Ʈ�� �����ϰ� ������Ʈ�� �ָ԰� ������             
                _SJoint.connectedBody = obj.GetComponent<Rigidbody>();
                _SJoint.maxDistance = 8.0f;
                
            }

            M2checkT = 0.0f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            //// �ָ� �������� �̸����� ã�Ƽ� �����µ�
            GameObject LHK = GameObject.Find("LoaderHook(Clone)");
            // RMB �ִϸ��̼� ������ �ı�
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
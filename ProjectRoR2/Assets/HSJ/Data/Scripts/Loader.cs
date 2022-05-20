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
    /// ���Ȱ��� 
    /// </summary> 
    [SerializeField]
    private Transform myFrontAim;
    [SerializeField]
    private LayerMask Onground;

    public KJH_CharacterData myCharacterdata;
    public KJH_CharacterStat myCharacterStat;

    // ��Ÿ�� UI;
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

        // ĳ���ͷ� ���� ȭ�� ���߾����� ���ư��� ��


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
        ////Ű���� WSAD ��
        //moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        ////ī�޶� ���� ���Ⱚ
        //lookForward = new Vector3(myCamArm.forward.x, Mathf.Epsilon, myCamArm.forward.z).normalized;
        ////ī�޶� ������ ���Ⱚ
        //lookRight = new Vector3(myCamArm.right.x, Mathf.Epsilon, myCamArm.right.z).normalized;
        ////ī�޶� ����/Ű���� ������ �̵� ����
        //moveDir = lookForward * moveInput.z + lookRight * moveInput.x;
        ////������ �Ǵ� ��
        //myCharacterdata.ismove = moveInput.magnitude != 0.0f;
        ////���� �Ǵ� ��
        //if (Input.GetMouseButton(0))
        //{
        //    StartCoroutine(DelayTime(3.0f));
        //}
        //���� �������� �̵� �Ǵ� ��
        //�ִϸ��̼� ���� �Ǵ� ��
        //myAnim.SetBool("isAttack", myCharacterdata.isAttack);
        //�ִϸ��̼� ������ �Ǵ� ��
        //myAnim.SetBool("ismove", myCharacterdata.ismove);   // �ڸ��� ���ݽ� ȸ�� ���� ���� �δ��� �ʿ��� ����
        //��Ÿ�� �ð� üũ
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
    ///  �̵� ���� 
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
        //�������� ������ ���� ���� ����
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0.0f, MySpringArm.rotation.eulerAngles.y, 0.0f), Time.deltaTime * 5.0f);
        //�̵�
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        
        //������ �ȴ� �ִϸ��̼�
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
            myAnim.SetBool("IsRun", true);
            
        }
        else
        {
            
            myAnim.SetBool("OnAir", true);
            myAnim.SetBool("IsRun", false);
            // �߷� ����           

            
                delta = forceGarvity * 5.0f * Time.deltaTime;
                forceGarvity ++;
                myRigid.AddForce(Vector3.down * delta, ForceMode.Force);
            Debug.Log(delta);
               
            
           
            //Debug.Log(delta);



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
            //�ָ� ����
            GameObject obj = Instantiate(LoaderFist, HookStart);
            //�θ����� �� ĳ���� ������ �ָ� ���� ����
            obj.GetComponent<Transform>().parent = null;
            obj.GetComponent<Transform>().transform.eulerAngles = this.transform.eulerAngles;

            //���� �������� �ָ� �߻�
            obj.GetComponent<Rigidbody>().AddForce(MySpringArm.forward * 80.0f, ForceMode.VelocityChange);

            //�δ��� ������Ʈ�� ����������Ʈ�� �����ϰ� ������Ʈ�� �ָ԰� ������

            _SJoint.connectedBody = obj.GetComponent<Rigidbody>();
            _SJoint.maxDistance = 10.0f;
            

            // �ָ� �ǵ��ƿ��� ���� 
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
            // �ָ� �������� �̸����� ã�Ƽ� �����µ�
            GameObject LHK = GameObject.Find("LoaderHook(Clone)");
            // �δ��� �ָ� �Ÿ��� �缭 
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
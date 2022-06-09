using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Loader : Character, BattlecombatSystem
{
    public LayerMask CrashMask;
    public LayerMask AttackMask;
    public enum STATE
    {
        CREATE, PLAY, PAUSE, DEAD
    }
    public STATE myState = STATE.CREATE;
    //State//
    bool isRun = false;
    bool isJump = false;
    bool isR = false;
    bool isShift = false;
    //////////////////MoveInput//////////////////  
    Vector3 StartPos = Vector3.zero;
    //////////////////JumpInput//////////////////

    //////////////////AttackInput//////////////////     
    public Transform HookStart;
    public GameObject LoaderFist;
    public Transform MySpringArm;
    public Transform LeftArm;
    public Transform RightArm;
    public GrappleFist myGrapple;
    public Transform Back;

    public AttackSystem myAttackSystem = null;


    [SerializeField]
    private Transform myFrontAim;
    [SerializeField]
    private LayerMask Onground;

    // ���� ��������
    public KJH_CharacterData myCharacterdata;
    public KJH_CharacterStat myCharacterStat;

    float jumpmotionTime;

    // ��Ÿ�� UI;
    public KeyInputControl myKeyControl = null;
    // ���� UI;
    public SoundManager mySound = null;
    // ���ӿ��� UI;
    public GameOverCanvas myGameOver = null;
    // ����Ʈ UI;
    public GameObject ShiftEffect;
    public GameObject LMBEffect;
    public GameObject ChargingEffect;
    public GameObject HitEffect;

    public bool IsLivekkj()  //���������  
    {
        return true; 
    }
    public void OnDamagekkj(float Damage) //�÷��̾ ������ �ް� ������ 
    {
        myCharacterStat.maxHP -= Damage; //�������� �޽��ϴ�.

        Debug.Log("�÷��̾ ���� �ް� �ֽ��ϴ�.");
        if (myCharacterStat.maxHP <= 0)
        {
            ChangeState(STATE.DEAD);
            Debug.Log("�׾����ϴ�.");
        }
  
    }


    public void Start()
    {
        myKeyControl = KeyInputControl.KeyInputMachine;
        mySound = SoundManager.SoundManagerMachine;
        myGameOver = GameOverCanvas.GameOverMachine;

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

    public void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.PLAY:
                StartPos = this.transform.position;
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
                M1checkT += Time.deltaTime;
                M2checkT += Time.deltaTime;
                ShiftcheckT += Time.deltaTime;
                RcheckT += Time.deltaTime;
                ChargingTime += Time.deltaTime;               
                Attack();
                Jump();
                GroundCheck();                
                break;
            case STATE.PAUSE:
                break;
            case STATE.DEAD:
                break;
        }
    }
    
    private void FixedUpdate()
    {
       
            PlayerMoving();
        //PlayerCrashCheck(); 
            ForwardCheck();

    }

    void Update()
    {      
        StateProcess();
    }






    /// <summary>
    ///  �̵� ���� 
    /// </summary>
    /// 
    //void PlayerCrashCheck()
    //{
    //    GameObject obj = GameObject.Find("pelvis");
    //    Ray ray = new Ray();
    //    ray.origin = obj.transform.position;
    //    ray.direction = this.transform.forward;
        
    //    if (Physics.Raycast(ray, out RaycastHit hit, 0.5f, CrashMask))
    //    {
    //        myRigid.constraints = RigidbodyConstraints.FreezeRotation|RigidbodyConstraints.FreezePositionZ;
    //        if (myAnim.GetFloat("Dir.y") < 0.0f)
    //            myRigid.constraints = RigidbodyConstraints.FreezeRotation;


    //    }
    //    else
    //        myRigid.constraints = RigidbodyConstraints.FreezeRotation;
    //    Debug.DrawRay(ray.origin, hit.point);

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

        if (myAnim.GetFloat("Dir.y") > 0.2f)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
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
            RunningCancel();
            myRigid.velocity = Vector3.up * myCharacterStat.JumpForce;


        }
        isJump = false;
        myAnim.SetBool("OnAir", false);

    }
    // ����üũ
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
                    transform.position = hit.point - new Vector3(0.0f, 1f, 0.0f) - rayforward.direction * 0.38f;
                }
                else if (Physics.Raycast(rayleft, out hit, 0.3f))
                {
                    transform.position = hit.point - new Vector3(0.0f, 1f, 0.0f) - rayleft.direction * 0.25f;
                }
                else if (Physics.Raycast(rayright, out hit, 0.3f))
                {
                    transform.position = hit.point - new Vector3(0.0f, 1f, 0.0f) - rayright.direction * 0.25f;
                }

            }
        }

    }

    //��üũ
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
            myAnim.SetBool("Jumping", false);
            myCharacterdata.isGround = true;
            myCharacterStat.JumpCount = 0;
        }
        else
        {
            

                myAnim.SetBool("OnAir", true);
            myAnim.SetBool("isSprint", false);
            myAnim.SetFloat("IsRun", 0.0f);
            myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
            myCharacterdata.isGround = false;
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
        if (ShiftcheckT >= ShiftCool && isR != true)
            Shift();
        // R
        if (isShift != true)
            R();


    }

    public void TakeDamage(float damage)
    {
        bool isDie = myAttackSystem.DecreaseHP(damage);
        if (isDie)
        {
            Debug.Log("Die");
            ChangeState(STATE.DEAD);
        }
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
                mySound.PlaySound("LMB");
                StartCoroutine(Attacking(RightArm, 5.0f));


            }
            if (myAnim.GetBool("IsLMBR") && Comboable && !myAnim.GetBool("IsLMBL"))
            {

                if (Input.GetMouseButtonDown(0))
                {
                    myAnim.SetBool("isSprint", false);
                    myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
                    myAnim.SetTrigger("LMBAtkL");
                    mySound.PlaySound("LMB");
                    StartCoroutine(Attacking(LeftArm, 5.0f));

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
                myKeyControl.M2CoolTime(M2Cool);
                //�ָ� ����
                GameObject obj = Instantiate(LoaderFist, HookStart);
                //�θ����� �� ĳ���� ������ �ָ� ���� ����
                obj.transform.parent = null;
                obj.transform.eulerAngles = MySpringArm.eulerAngles;
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
        
        if (ShiftcheckT >= ShiftCool && Input.GetKeyDown(KeyCode.LeftShift) && !myAnim.GetBool("IsShift"))
        {
            isShift = true;

            ChargingTime = 0.0f;
            myAnim.SetTrigger("ShiftAtk");
            myAnim.SetBool("IsShift", true);
            myAnim.SetBool("IsPunchLoop", true);
            Debug.Log(isShift);
        }       
        if (Input.GetKeyUp(KeyCode.LeftShift) && ChargingTime <= 1.0f)
        {
            isShift = true;

            PunchActive(80.0f);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && 1.0f < ChargingTime && ChargingTime <= 3.0f)
        {
            isShift = true;

            PunchActive(160.0f);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && ChargingTime >= 3.0f)
        {
            isShift = true;

            PunchActive(240.0f);
        }
        myAnim.SetBool("IsShift", false);
        

    }
    void PunchActive(float speed)
    {
        myAnim.SetBool("IsPunchLoop", false);
        myRigid.AddForce(Vector3.zero);
        myRigid.AddForce(MySpringArm.forward * speed, ForceMode.Impulse);
        mySound.PlaySound("Shift");
        myKeyControl.ShiftCoolTime(ShiftCool);
        ShiftcheckT = 0.0f;
        isShift = false;
    }

    void R()
    {
        if (RcheckT >= RCool && Input.GetKeyDown(KeyCode.R) && !myAnim.GetBool("IsR"))
        {
            isR = true;
            myKeyControl.RCoolTime(RCool);
            myAnim.SetBool("IsR", true);
            myRigid.AddForce(Vector3.up * 20.0f, ForceMode.VelocityChange);
            myAnim.SetBool("OnAir", true);
            myAnim.SetTrigger("RAtk");                      
            RcheckT = 0.0f;
        }
        if (myAnim.GetBool("IsR") && !myAnim.GetBool("OnAir"))
        {
            isR = false;
            myAnim.SetBool("IsR", false);

        }

    }

    //// ���� �Ǵ��ϰ� ����Ʈ ��� 
    IEnumerator Attacking(Transform target, float Range)
    {
        Collider[] Beetles = Physics.OverlapSphere(target.position,
            Range, 1 << LayerMask.NameToLayer("Beetle"), QueryTriggerInteraction.Collide);
        foreach (Collider mon in Beetles)
        {

            Instantiate(HitEffect, mon.transform.position, Quaternion.identity);
            mon.GetComponent<Beetle>()?.HJSGetDamage(myCharacterStat.LoaderDamage);



            Debug.Log("������������ " + myCharacterStat.LoaderDamage + "��ŭ�� ������");

            yield return null;
        }

    }
}
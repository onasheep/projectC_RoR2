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
    /////////////////////////////////////////
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
                PlayerMovement();
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
        CooltimeReset();
        if (myState == STATE.CREATE)
        {
            ChangeState(STATE.PLAY);
        }
    }
    private void FixedUpdate()
    {
        if (myState == STATE.PLAY)
        {
                     
        }
    }
    private void LateUpdate()
    {
        if (myCharacterdata.isLookAround)
        {
            LookAround();
        }    
    }
    
    void Update()
    {        
        StateProcess();
        GroundCheck();
        ForwardCheck();
        GetInput();
    }
    //���⺻����
    void CooltimeReset()
    {
        myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
        RollTimeCheck = myCharacterStat.RollTime;
        AttackTimeCheck = myCharacterStat.AttackDelay;
        RMBTimeCheck = myCharacterStat.RMBTime;
        RKBTimeCheck = myCharacterStat.RKBTime;
    }
    //�����Է�
    void GetInput()
    {
        mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        camAngle = myCamArm.transform.rotation.eulerAngles;
        //Ű���� WSAD ��
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical")).normalized;
        //ī�޶� ���� ���Ⱚ
        lookForward = new Vector3(myCamArm.forward.x, Mathf.Epsilon, myCamArm.forward.z).normalized;
        //ī�޶� ������ ���Ⱚ
        lookRight = new Vector3(myCamArm.right.x, Mathf.Epsilon, myCamArm.right.z).normalized;
        //ī�޶� ����/Ű���� ������ �̵� ����
        moveDir = lookForward * moveInput.z + lookRight * moveInput.x;
        //������ �Ǵ� ��
        myCharacterdata.ismove = moveInput.magnitude != 0.0f;
        //���� �������� �̵� �Ǵ� ��
        myCharacterdata.onforward = Input.GetKey(KeyCode.W);
        //�ִϸ��̼� ���� �Ǵ� ��
        myAnim.SetBool("isAttack", myCharacterdata.isAttack);
        //�ִϸ��̼� ������ �Ǵ� ��
        myAnim.SetBool("ismove", myCharacterdata.ismove);
        //��Ÿ�� �ð� üũ
        RollTimeCheck += Time.deltaTime;
        AttackTimeCheck += Time.deltaTime;
        RMBTimeCheck += Time.deltaTime;
        RKBTimeCheck += Time.deltaTime;

    }
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
        /*
        if (camAngle.y < 180.0f)
        {
            camAngle.y = Mathf.Clamp(camAngle.y, -1.0f, 80.0f);
        }
        else
        {
            camAngle.y = Mathf.Clamp(camAngle.y, 260.0f, 361f);
        }
        */
        //myChest.transform.LookAt(myFrontAim);
        myChest.transform.rotation = Quaternion.Euler(x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //Debug.Log(camAngle.y);
    }
    //�����Լ�
    void Attack()
    {
        if (myCharacterdata.isRoll == false)
        {
            LMB();
            RMB();
            RKB();
        }
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
    //����üũ
    void ForwardCheck()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.5f))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall") || hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                transform.position = hit.point - ray.direction * 0.47f;
            }
        }
    }
    //��üũ
    void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(myAnim.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.down, out hit, 0.6f, Onground))
        {
            myAnim.SetBool("OnAir", false);
            myAnim.SetBool("Jumping", false);
            myCharacterdata.isGround = true;
            myCharacterStat.JumpCount = 0;
        }
        else 
        {
            if (myCharacterdata.isRoll == false)
            {
                myAnim.SetBool("OnAir", true);
            }                       
            myCharacterdata.isGround = false;
        }
    }
    #endregion
    #region CommonMove
    //�⺻������
    public void PlayerMovement()
    {
        //if (RotRoutine != null) StopCoroutine(RotRoutine);
        //������ ��
        if (myCharacterdata.isAttack)
        {    
            //���� �Ҷ� �޸��� ���
            RunningCancel();
            //ī�޶� ���� ���� �������� ��ȯ
            //myChest.transform.rotation = Quaternion.LookRotation(new Vector3(lookForward.x, Mathf.Epsilon, lookForward.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(lookForward.x, Mathf.Epsilon, lookForward.z)), Time.deltaTime * 30.0f);
            myAnim.SetFloat("Dir.x", moveInput.x, 0.1f, Time.deltaTime);
            myAnim.SetFloat("Dir.y", moveInput.z, 0.1f, Time.deltaTime);
                     
        }
        //�������� ���� ��
        else
        {
            //this.transform.LookAt(this.transform.position + moveDir);
            
        }
        //������ ��       
        if (myCharacterdata.ismove && myCharacterdata.isRoll == false)
        {
            if (!myCharacterdata.isAttack) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 10.0f); //RotRoutine = StartCoroutine(Rotating(myAnim.transform.position + moveDir));

            transform.position += myCharacterStat.ApplySpeed * Time.deltaTime * moveDir;
            //Debug.Log(myCharacterStat.ApplySpeed);
        }
    }
    #endregion
    #region Run
    // Ư�� ���ǿ� �°� �޸���
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
    // �޸���
    public void Runing()
    {      
        myCharacterdata.isRun = true;
        myCharacterdata.isLookAround = false;
        myAnim.SetBool("isSprint", true);
        myAnim.SetTrigger("Sprint");
        myCharacterStat.ApplySpeed = myCharacterStat.RunSpeed;                
    }
    // �޸��� ����
    public void RunningCancel()
    {
        myCharacterdata.isRun = false;
        myCharacterdata.isLookAround = true;
        myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
        myAnim.SetBool("isSprint", false);       
    }
    #endregion
    #region Roll
    //������
    public void TryRoll()
    {       
        if (Input.GetKeyDown(KeyCode.LeftShift) && myCharacterdata.isRoll == false && RollTimeCheck >= myCharacterStat.RollTime)
        {
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
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 3.5f))
        {
            myAnim.SetTrigger(Name);
            //if (cooltime != null) StopCoroutine(cooltime);
            //cooltime = StartCoroutine(Rolling(hit.point));
            transform.position = hit.point;
        }
        else
        {           
            myAnim.SetTrigger(Name);           
            if(Rollingtime != null) StopCoroutine(Rollingtime);
            Rollingtime = StartCoroutine(Rolling(dir * 3.5f));
            //transform.position += moveDir * 5.0f;
        }
    }
    
    IEnumerator Rolling(Vector3 dir)
    {
        float dist = dir.magnitude;
        yield return new WaitForSeconds(0.1f);
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
        yield return new WaitForSeconds(0.1f);
        myCharacterdata.isLookAround = true;
        myCharacterdata.isRoll = false;
        if(myCharacterdata.isGround == true) Runing();
        Rollingtime = null;
    }
    #endregion
    #region Jump
    // ����
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
    //���ʸ��콺����
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
    //������ ���콺 ����  
    void RMB()
    {
        //������
        if (Input.GetMouseButton(1))
        {
            if (RMBTimeCheck >= myCharacterStat.RMBTime)
            {
                StartCoroutine(DelayTime(3.0f));
                RMBTimeCheck = 0;
                myAnim.SetTrigger("LMBAtkR");
                //StartCoroutine(ShotBullet(ShotPosR, "BulletMouse1"));
                myAttackSystem.TwoStepRaycast(ShotPosR, "BulletMouse1");
            }
        }
    }
    //RŰ Ư������
    void RKB()
    {
        //Ư������
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (RKBTimeCheck >= myCharacterStat.RKBTime)
            {
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
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////

}

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
    float forceGarvity = 10.0f;
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

    Vector3 lookForward;

    public KJH_CharacterData myCharacterdata;
    public KJH_CharacterStat myCharacterStat;




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
                Attack();
                break;
            case STATE.DEAD:
                break;
        }
    }
    public void Start()
    {
        myCharacterStat.ApplySpeed = myCharacterStat.WalkSpeed;
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
            PlayerMovement();
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
        myCharacterdata.onforward = Input.GetKey(KeyCode.W);
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
    //Coroutine RotRoutine = null;
    //IEnumerator Rotating(Vector3 pos)
    //{
    //    Vector3 dir = (pos - this.transform.position).normalized;
    //    KJH_GameUtil.CalAngle(myAnim.transform.forward, dir, myAnim.transform.right, out KJH_ROTATEDATA data);
    //    while (data.Angle > Mathf.Epsilon)
    //    {
    //        float delta = 360.0f * Time.deltaTime;
    //        if (data.Angle <= delta)
    //        {
    //            delta = data.Angle;
    //        }
    //        transform.Rotate(Vector3.up * delta * data.Dir);
    //        data.Angle -= delta;
    //        yield return null;
    //    }
    //    RotRoutine = null;
    //}



    /// <summary>
    ///  �̵� ���� 
    /// </summary>
    /// 



    public void PlayerMovement()
    {
        float _moveDirx = Input.GetAxisRaw("Horizontal");
        float _moveDiry = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirx;
        Vector3 _moveVertical = transform.forward * _moveDiry;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * myCharacterStat.ApplySpeed;

        myRigid.MovePosition(this.transform.position + _velocity * Time.deltaTime);
        lookForward = new Vector3(MySpringArm.forward.x, Mathf.Epsilon, MySpringArm.forward.z).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(lookForward.x, Mathf.Epsilon, lookForward.z));
        //this.transform.eulerAngles = MySpringArm.eulerAngles;
        if (isGround)
        {
            myAnim.SetFloat("Dir.x", _moveDirx, 0.1f, Time.deltaTime);
            myAnim.SetFloat("Dir.y", _moveDiry, 0.1f, Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {

            TryRun();

        }
        else
        {
            RunningCancel();
        }

    }


    // Ư�� ���ǿ� �°� �޸���
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
        else if (!myCharacterdata.onforward)
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
    // ����
    public void Jump()
    {
        if (Input.GetKeyDown("space"))
        {
            if (myCharacterStat.JumpCount < myCharacterStat.JumpItem)
            {
                myCharacterdata.isLookAround = false;
                myAnim.SetTrigger("Jump");
                myAnim.SetBool("OnAir", true);
                //myAnim.SetBool("Jumping", true);
                RunningCancel();
                myRigid.velocity = Vector3.up * myCharacterStat.JumpForce;
                myCharacterStat.JumpCount++;
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
            //myAnim.SetBool("Jumping", false);
            myCharacterStat.JumpCount = 0;
        }
        //else if(Physics.Raycast(myAnim.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.down, out hit, 2.0f, Onground))
        //{
        //    myAnim.SetBool("OnAir", false);

        //}
        else
        {

            myAnim.SetBool("OnAir", true);
            myRigid.AddForce(Vector3.down, ForceMode.Force);

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


    bool Comboable = false;
    void LMB()
    {

        if (!myAnim.GetBool("IsLMBR") && Input.GetMouseButtonDown(0))
        {

            myAnim.SetTrigger("LMBAtkR");

        }
        if (myAnim.GetBool("IsLMBR") && Comboable && !myAnim.GetBool("IsLMBL"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                myAnim.SetTrigger("LMBAtkL");

            }
        }
    }
    SpringJoint Spjoint;
    void RMB()
    {

        if (Input.GetMouseButtonDown(1))
        {
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
            Spjoint = this.GetComponent<SpringJoint>();
            Spjoint.connectedBody = obj.GetComponent<Rigidbody>();
            Spjoint.maxDistance = 10.0f;

            // �ָ� �ǵ��ƿ��� ���� 
            //if (!(MySpringArm.position - S < 10.0f))
            //    {

            //}
        }
        if (Input.GetMouseButton(1))
        {
            myAnim.SetBool("IsFistOnCollid", true);
        }

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

        }



    }
    void Shift()
    {


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            myAnim.SetBool("IsShift", false);
            myAnim.SetTrigger("ShiftAtk");

        }


        if (Input.GetKeyUp(KeyCode.LeftShift))
        {



            myAnim.SetBool("IsShift", true);
            myRigid.AddForce(MySpringArm.forward * 80.0f, ForceMode.Impulse);

        }
    }
    void R()
    {
        if (isGround & Input.GetKeyDown(KeyCode.R))
        {
            Dist = Dir.magnitude;
            Dir.Normalize();
            myRigid.AddForce(Vector3.zero);
            myRigid.AddForce(Vector3.up * 40.0f, ForceMode.VelocityChange);
            myAnim.SetTrigger("RAtk");


            myAnim.SetBool("OnAir", true);
            Ray ray = new Ray();
            ray.origin = this.transform.position;
            ray.direction = -this.transform.up;
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, CrashMask))
            {
                float Dist = (this.transform.position - hit.point).magnitude;
                if (Dist < 20.0f)
                    myRigid.AddForce(Vector3.down * 80.0f, ForceMode.Impulse);
                myAnim.SetBool("OnAir", true);

            }


            //if (Dist == 1.0f)      // �ٴڿ� ������� ��� ���߿� �߰� ����� �κ�;
            //{
            //    myRigid.AddForce(Vector3.up * 2.0f, ForceMode.VelocityChange);
            //    myAnim.SetBool("OnAir", false);
            //    Dist = 0.0f;
            //}

        }
        if (!isGround & Input.GetKeyDown(KeyCode.R))
        {

        }



        myAnim.SetBool("Run", true);

    }

}
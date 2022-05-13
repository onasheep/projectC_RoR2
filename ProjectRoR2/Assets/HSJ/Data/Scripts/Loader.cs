using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float WalkSpeed = 7.0f;
    public float RunSpeed = 10.0f;
    float ApplySpeed;
    float rx;
    float ry;
    //////////////////JumpInput//////////////////
    int JumpCount = 0;  
    public float JumpForce = 10.0f;
    int JumpItem = 1;
    //////////////////AttackInput//////////////////     
    public Transform HookStart;
    public GameObject LoaderFist;
    public Transform MyCamera;
    public Transform MySpringArm;
    public Transform LeftArm;
    public Transform RightArm;
    /// <summary>
    /// ���Ȱ��� 
    /// </summary>
    /// ��Ÿ�� �� ��ų UI ��Ÿ�� //
    public Image M2Img;
    public Image ShiftImg;
    public Image RImg;
    /// <summary>
    ///  �� ��ų ���� ��Ÿ�� 
    /// </summary>
    private float RMBCool = 5.0f;
    private float ShiftCool = 8.0f;
    private float RCool = 8.0f;
    /// <summary>
    ///  ��ųUI�� ��Ÿ���� �����Ű�� �ڷ�ƾ 
    /// </summary>
    /// <param name="_image"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    IEnumerator CoolTime(Image _image, float t)
    {
        _image.fillAmount = 0.0f;
        float speed = 1.0f / t;
        while (_image.fillAmount < 1.0f)
        {
            _image.fillAmount += Time.deltaTime * speed;
            yield return null;
        }
    }



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
                PlayerMoving();               
                Jump();
                Attack();
                break;
            case STATE.DEAD:
                break;
        }
    }
    public void Start()
    {
        ApplySpeed = WalkSpeed;
        if (myState == STATE.CREATE)
        {
            ChangeState(STATE.PLAY);
        }

        this.GetComponentInChildren<ComboEvent>().ComboCheck += (value) => Comboable = value;
        
        // ĳ���ͷ� ���� ȭ�� ���߾����� ���ư��� ��


    }

    void Update()
    {
        StateProcess();
        
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
            StartCoroutine(CoolTime(M2Img, RMBCool));
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
        if(Input.GetMouseButton(1))
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
            StartCoroutine(CoolTime(ShiftImg, ShiftCool));


            myAnim.SetBool("IsShift", true);
            myRigid.AddForce(MySpringArm.forward * 80.0f, ForceMode.Impulse);
            
        }
    }
    void R()
    {
        if (isGround & Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(CoolTime(RImg, RCool));

            Dist = Dir.magnitude;
            Dir.Normalize();
            myRigid.AddForce(Vector3.zero);
            myRigid.AddForce(Vector3.up * 40.0f, ForceMode.VelocityChange);           
            myAnim.SetTrigger("RAtk");
            
            
            myAnim.SetBool("OnAir",true);
           
            

            //if (Dist == 1.0f)      // �ٴڿ� ������� ��� ���߿� �߰� ����� �κ�;
            //{
            //    myRigid.AddForce(Vector3.up * 2.0f, ForceMode.VelocityChange);
            //    myAnim.SetBool("OnAir", false);
            //    Dist = 0.0f;
            //}

        }
        



            myAnim.SetBool("Run", true);

    }

   /// <summary>
   ///  �̵� ���� 
   /// </summary>
    public void PlayerMoving()
    {


        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirY = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirY;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * ApplySpeed;
        Vector3 dir = (_moveHorizontal + _moveVertical).normalized;
        //�̵�
        myRigid.MovePosition(transform.position + _velocity * 7f * Time.deltaTime);
        //������ �ȴ� �ִϸ��̼�
        if(isGround)
        {
            myAnim.SetFloat("x", _moveDirX, 0.1f, Time.deltaTime);
            myAnim.SetFloat("y", _moveDirY, 0.1f, Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
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
        ApplySpeed = RunSpeed;
    }

    // �޸��� ����
    public void RunningCancel()
    {
        isRun = false;
        myAnim.SetBool("isSprint", false);
        ApplySpeed = WalkSpeed;
    }
    // ����
    public void Jump()

    {
        if (isGround)
        {
            JumpCount = 1;
            if (Input.GetKeyDown("space"))
            {
                myAnim.SetTrigger("Jump");
                myAnim.SetBool("OnAir", true);
                RunningCancel();
                if (JumpCount == JumpItem)
                    myRigid.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                isGround = false;
                JumpCount = 0;
            }
        }
        else
        {

        }
    }
    //���� ������ ��������
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Ground")
        {
            Debug.Log("On");
            isGround = true;
            myAnim.SetBool("OnAir", false);
            JumpCount = 1;
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "Ground")
        {
            myAnim.SetBool("OnAir", true);
            Debug.Log("Off");
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////

    /*
    Ray ray = new Ray(this.transform.position, -this.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, CollisionOffset + myZoom, CrashMask))
        {
            myZoom = Vector3.Distance(hit.point - ray.direction * CollisionOffset, this.transform.position);
        }
        myCam.localPosition = -Vector3.forward * myZoom;
    */

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lemurian : MoveMove, BattlecombatSystem
{

    public AttackSystem myAttackSystem = null; //(�÷��̾�)���� ����
    public STATE mystate = STATE.NONE;
    [SerializeField]
    public  CharacterStatkkj LemuData; //ĳ���� ������ (���ݷ��̶���� ü�� ��) 
    public Transform firemouth;  //���̾ �߻� ��ġ 
    public GameObject FireBoom; //���̾ 
    public GameObject myperceptionlemus1;
    AIPerceptionlemu myperceptionlemus;
    public Transform HpBar; // HP������ 
    float AtkDelayCheck; //���� ������ üũ 
    public float MonsterUIBarHeight = 0.0f; // ��������  UI�� ü�� ���� 
    public MonsterUIBar kkjmonsterUIBar;


    [SerializeField]
    private LayerMask Ongound;

    Vector3 StartPos;

    public enum STATE
    {
        NONE, CREATE, RUN, BATTLE, DEAD
    }
    

    [SerializeField] Transform Target;
    public Transform myTarget
    {
        get => Target;
        set => Target = value;
    }

    Rigidbody _rigidbody = null;
    Rigidbody rigidbody
    {
        get
        {
            if (_rigidbody == null)
            {
                _rigidbody = this.GetComponent<Rigidbody>();
            }
            return _rigidbody;
        }
    }
    /*
    AIPerceptionlemu myperceptionlemus //�������� 
    {
        get
        {
            if (_aiperceptionlemu == null)
            {
                _aiperceptionlemu = GameObject.Find("AIPerception").GetComponent<AIPerceptionlemu>();

            }
            return _aiperceptionlemu;
        }
    }
    */
    public float _curHP = 0.0f;
    public float HP //ü�� ���� (UI ����?) 
    {
       
        get
        {
            return _curHP;
        }
        set
        {
            _curHP = value;
            if (_curHP <= 0.0f) _curHP = 0.0f;
            kkjmonsterUIBar.myHP.value = _curHP/LemuData.HP;
        }
    }
    
    
    void FindTargetkkj() //Ÿ�� ���� 
    {
        ChangeState(STATE.BATTLE);
    }

    public bool IsLivekkj()   
    {
        return mystate == STATE.RUN || mystate == STATE.BATTLE;
    }
    public void OnDamagekkj(float Damage) //���������� �������� ������ 
    {
        if (mystate == STATE.DEAD)
        { return; }

        HP -= Damage;
        myAnim.SetTrigger("Hit"); //�°� ������ 
         //    Invoke("HpBarSet", 0.0f);
 
        _curHP -= Damage;
        Debug.Log("���������� ���� �ް� �ֽ��ϴ�.");
        if (HP <= 0.0f)
        {
            HP = 0.0f;
            Debug.Log("���������� �׾����ϴ�. �׽�Ʈ ����");
       //     Destroy(kkjmonsterUIBar.gameObject);
            StopAllCoroutines();
            myAnim.SetTrigger("Dead");
            ChangeState(STATE.DEAD);
        }
 
       }
    
    /*
    void HpBarSet()
    {
        
            GameObject obj = Instantiate(Resources.Load("UI Prefab/MonsterHPBar"), GameObject.Find("InGameUICanvas").transform) as GameObject; //HP �� 
            kkjmonsterUIBar = obj.GetComponent<MonsterUIBar>(); //�� ��ũ��Ʈ�� �̿��Ѵ�. 
            kkjmonsterUIBar.Initialize(HpBar, 2.0f);
 
        Destroy(kkjmonsterUIBar.gameObject, 1.0f);
        
    }
    */

    void Firekkj(Transform Target, float range) //Ÿ���� ���� ���� 
    {
            myAnim.SetTrigger("Attack");
            GameObject Firebool = Instantiate(FireBoom, firemouth.position, firemouth.rotation); //�Կ��� �Ҳ��� �������� (���̾) 
            Vector3 dir = Target.position - firemouth.position;
            Firebool.GetComponent<LemurianFireball>().Shotting(dir, range);
           
    }
  

    void OnAttackkkj() //���ݽ� ó�� 
    {
        if (myperceptionlemus.Target.IsLivekkj())
            myperceptionlemus.Target.OnDamagekkj(LemuData.AP); //���� ������?
    }
    private void Awake()
    {
        
    }
    void Start()
    {
        
       GameObject obj = Instantiate(Resources.Load("UI Prefab/MonsterHPBarrr"), GameObject.Find("InGameUICanvas").transform) as GameObject; //HP �� 
       kkjmonsterUIBar = obj.GetComponent<MonsterUIBar>(); //�� ��ũ��Ʈ�� �̿��Ѵ�. 
       kkjmonsterUIBar.Initialize(HpBar, 2.0f);
        
        myperceptionlemus = myperceptionlemus1.GetComponent<AIPerceptionlemu>();
        ChangeState(STATE.CREATE);
    }



    // Update is called once per frame
    void Update()
    {
      ProcessSTATE();
     }

    IEnumerator DeadDisappeerkkj() //���Ͱ� �װ� �� ���� ó�� 
    {
        yield return new WaitForSeconds(1.0f);
        float dist = 1.0f;
        while(dist > 0.0f)
        {
            float delta = Time.deltaTime * 0.5f;
            this.transform.Translate(-Vector3.up * delta);
            dist -= delta;
            yield return null;
        }
        Destroy(this.gameObject);
     //   Destroy(kkjmonsterUIBar.gameObject);
    }

    void ChangeState(STATE s)
    {
        if (mystate == s) return;
        mystate = s;
        switch(mystate)
        {
            case STATE.CREATE:
                myperceptionlemus.FindTarget = FindTargetkkj;
                 _curHP = LemuData.HP;
                StartPos = this.transform.position;
                myAnim.SetTrigger("Spawn"); // ���� 
                LemuData.HP = 80.0f; //ü�� 
                LemuData.MoveSpeed = 5.0f; //������ 
                LemuData.AP = 12.0f;
                LemuData.AttackRange = 2.0f;
                LemuData.AttackDelay = 5.0f;
                ChangeState(STATE.RUN);
                break;
            case STATE.RUN:
                //    if ((Vector3.Distance(Target.transform.position, this.transform.position) > LemuData.AttackRange))
                //  {
                //     myperceptions.FindTarget = FindTargetkkj;
                //    }
                break;
            case STATE.BATTLE:
                StopAllCoroutines();
                //Ÿ�� �������� ����ų� ������ �ٷ� �ٵ��� 
                base.AttackTargetkkj(myperceptionlemus.Target, LemuData.AttackRange, LemuData.AttackDelay, () => ChangeState(STATE.RUN));
                Debug.Log("�÷��̾�����߽��ϴ�. ���� ���� ���ϴ�.");
                //   Firekkj(myperceptionlemus.Target.transform, LemuData.AttackRange)
   
                    InvokeRepeating("Firekkj", 3.0f ,1.1f); //���̾ ���� �ݺ� (?) 
                  if (LemuData.HP <= 0.0f)
                  {
                      ChangeState(STATE.DEAD);
                  }
                  break;
              case STATE.DEAD:
                  StopAllCoroutines();
                  myAnim.SetTrigger("Dead"); //Dead
                  CancelInvoke("Firekkj"); //���̾�� ������ �ʰ� �ϰ�
                  Debug.Log("���������� �׾����ϴ�."); 
                  StartCoroutine(DeadDisappeerkkj()); //���� ��������� 
                 break;

          }
      }

      void ProcessSTATE()
      {
          switch(mystate)
          {
              case STATE.CREATE:
                  /*
                  if(myAnim.GetBool("Spawning") && mystate != STATE.RUN)
                  {
                      ChangeState(STATE.RUN);
                  }
                  */
                break;
            case STATE.RUN:
                /*
                if (myTarget)
                {
                    if (Vector3.Distance(myTarget.position, this.transform.position) > LemuData.AttackRange)
                    {
                        myAnim.SetTrigger("Move");

                    }
                    if (Vector3.Distance(myTarget.position, this.transform.position) <= LemuData.AttackRange && !myAnim.GetBool("Attacking"))
                        ChangeState(STATE.BATTLE);
                }
                else
                    StopAllCoroutines();
                */

                    break;
            case STATE.BATTLE:
               
               AtkDelayCheck += Time.deltaTime;
                if (AtkDelayCheck >= LemuData.AttackDelay)
                {
                    Firekkj(myperceptionlemus.Target.transform, LemuData.AttackRange);
                   AtkDelayCheck = 0.0f;
                }
                Debug.Log("Attacking");
                /*
                if (LemuData.HP <= 0.0f)
                {
                    StopAllCoroutines();
                    myAnim.SetTrigger("Dead");
                    ChangeState(STATE.DEAD);
                }
                */
                break;
            case STATE.DEAD:
                break;
        }
    }



}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class Lemurian : MoveMove, BattlecombatSystem
{
    public enum STATE
    {
        NONE, CREATE, RUN, BATTLE, DEAD
    }
    AIPerceptionlemu _aiperceptionlemu = null;

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

    AIPerceptionlemu myperceptionlemus //�������� 
    {
        get
        {
            if (_aiperceptionlemu == null)
            {
                _aiperceptionlemu = this.GetComponentInChildren<AIPerceptionlemu>();

            }
            return _aiperceptionlemu;
        }
    }

    public float HPChange //ü�� ���� (UI ����?) 
    {
        get
        {
            return _curHP;
        }
        set
        {
            _curHP += value;
            if (_curHP < 0.0f) _curHP = 0.0f;
            kkjmonsterUIBar.myHP.value = _curHP / LemuData.HP;
        }
    }

    float _curHP = 0.0f; //�ʱ�ȭ?
    float MoveDelay = 0.0f;
    public AttackSystem myAttackSystem = null; //�ڸ��� (�÷��̾�)���� ����
    public STATE mystate = STATE.NONE;
    public  CharacterStatkkj LemuData; //ĳ���� ������ (���ݷ��̶���� ü�� ��) 
    public Transform firemouth;  //���̾ �߻� ��ġ 
    public GameObject FireBoom; //���̾ 
    public Transform EnemyHead; // HP������ 
    float AtkDelayCheck; //���� ������ üũ 
    public float MonsterUIBarHeight = 80.0f; // ��������  UI�� ü�� ���� 
    MonsterUIBar kkjmonsterUIBar = null;

    [SerializeField]
    private LayerMask Ongound;

    Vector3 StartPos;

    void FindTargetkkj() //Ÿ�� ���� 
    {
        ChangeState(STATE.BATTLE);
    }

    public bool IsLivekkj()   
    {
        return mystate != STATE.DEAD;
    }
    public void OnDamagekkj(float Damage) //���������� �������� ������ 
    {
       if (mystate == STATE.DEAD) 
          return;
        LemuData.HP -= Damage;
       
        Debug.Log("���������� ���� �ް� �ֽ��ϴ�.");
        if (LemuData.HP <= 0)
        {
           Debug.Log("���������� �׾����ϴ�. �׽�Ʈ ����");
            ChangeState(STATE.DEAD);
            myAnim.SetTrigger("Dead");
            CancelInvoke("Firekkj");
        }
        else
        {
            myAnim.SetTrigger("Hit"); //�°� ������ 
        }
       }

    void Firekkj(Transform Target, float range) //Ÿ���� ���� ���� 
    {
        if (mystate != STATE.DEAD)
        {
            myAnim.SetTrigger("Attack");
            GameObject Firebool = Instantiate(FireBoom, firemouth.position, firemouth.rotation); //�Կ��� �Ҳ��� �������� (���̾) 
            Vector3 dir = Target.position - firemouth.position;
            Firebool.GetComponent<LemurianFireball>().Shotting(dir, range);
        }

      
    }

    

    void OnAttackkkj() //���ݽ� ó�� 
    {
        if (myperceptionlemus.Target.IsLivekkj())
            myperceptionlemus.Target.OnDamagekkj(LemuData.AP); //���� ������?
    }
 
    void Start()
    {
        /*
       GameObject obj = Instantiate(Resources.Load("UI Prefab/MonsterHPBar"), GameObject.Find("InGameUICanvas").transform) as GameObject; //HP �� 
        kkjmonsterUIBar = obj.GetComponent<MonsterUIBar>(); //�� ��ũ��Ʈ�� �̿��Ѵ�. 
        kkjmonsterUIBar.Initialize(EnemyHead, 0.0f);
        */
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
    }

    void ChangeState(STATE s)
    {
        if (mystate == s) return;
        mystate = s;
        switch(mystate)
        {
            case STATE.CREATE:
                myperceptionlemus.FindTarget = FindTargetkkj; //ã���� �ٴ�
                myAnim.SetTrigger("Spawn"); // ���� 
                LemuData.HP = 80.0f; //ü�� 
                LemuData.MoveSpeed = 5.0f; //������ 
                LemuData.AP = 12.0f;
                LemuData.AttackRange = 20.0f;
                LemuData.AttackDelay = 3.0f;
                              ChangeState(STATE.RUN);
                break;
            case STATE.RUN:
                //    if ((Vector3.Distance(Target.transform.position, this.transform.position) > LemuData.AttackRange))
                //  {
                //     myperceptions.FindTarget = FindTargetkkj;
                //    }
            if(myTarget)
                {
                    
                }
                break;
            case STATE.BATTLE:
             StopAllCoroutines();
                //Ÿ�� �������� ����ų� ������ �ٷ� �ٵ��� 
              base.AttackTargetkkj(myperceptionlemus.Target, LemuData.AttackRange, LemuData.AttackDelay, () => ChangeState(STATE.RUN));
                Debug.Log("�÷��̾�����߽��ϴ�. ���� ���� ���ϴ�.");
              InvokeRepeating("Firekkj", 3.0f, 1.1f); //���̾ ���� �ݺ� (?) 
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
                if(myAnim.GetBool("Spawning") && mystate != STATE.RUN)
                {
                    ChangeState(STATE.RUN);
                }
                break;
            case STATE.RUN:
                //    base.StartRunkkj(Target.transform.position, LemuData.MoveSpeed, LemuData.AttackRange);

                break;
            case STATE.BATTLE:
                AtkDelayCheck += Time.deltaTime;
                if (AtkDelayCheck >= LemuData.AttackDelay)
                {
                    Firekkj(myperceptionlemus.Target.transform, LemuData.AttackRange);
                    AtkDelayCheck = 1.0f;
                }
                break;
            case STATE.DEAD:
                break;
        }
    }



}



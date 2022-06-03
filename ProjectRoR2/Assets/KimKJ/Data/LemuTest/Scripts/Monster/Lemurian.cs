using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lemurian : MoveMove, BattleSystem
{
    public enum STATE
    {
        NONE, CREATE, RUN, BATTLE, DEAD
    }
    AIPerception _aiperception = null;

    Transform Target;
    public Transform myTarget
    {
        get => Target;
        set => Target = null;
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

    AIPerception myperceptions
    {
        get
        {
            if (_aiperception == null)
            {
                _aiperception = this.GetComponentInChildren<AIPerception>();

            }
            return _aiperception;
        }
    }

    public float HPChange
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
    public AttackSystem myAttackSystem = null; //�ڸ��� (�÷��̾�)���� ���� 
    public STATE mystate = STATE.NONE;
    public  CharacterStatkkj LemuData; //ĳ���� ������ (���ݷ��̶���� ü�� ��) 
    public Transform firemouth;  //���̾ �߻� ��ġ 
    public GameObject FireBoom; //���̾ 
    public Transform EnemyHead; // HP������ 
    public float MonsterUIBarHeight = 80.0f; // ��������  UI�� ü�� ���� 
    MonsterUIBar kkjmonsterUIBar = null;

    Vector3 StartPos;

    void FindTargetkkj() //Ÿ�� ���� 
    {
        ChangeState(STATE.BATTLE);
    }

    public bool IsLivekkj()  //�ٰų� or ������ �ϰų� 
    {
        return mystate == STATE.RUN || mystate == STATE.BATTLE;
    }
    public void OnDamagekkj(float Damage) //������ �޴°� 
    {
       if (mystate == STATE.DEAD) 
          return;
        LemuData.HP -= Damage;
       
        Debug.Log("���������� ���� �ް� �ֽ��ϴ�.");
        if (LemuData.HP <= 0)
        {
                   Debug.Log("���������� �׾����ϴ�. �׽�Ʈ ����");
            ChangeState(STATE.DEAD);
            CancelInvoke("Firekkj");
            myAnim.SetTrigger("Dead");
           
        }
        else
        {
            myAnim.SetTrigger("Hit");
        }
       }

    void Firekkj() //Ÿ���� ���� ���� 
    {
        myAnim.SetTrigger("Attack");
        GameObject Firebool = Instantiate(FireBoom, firemouth.position, firemouth.rotation);
     
    }

    

    void OnAttackkkj() //���ݽ� ó�� 
    {
        if (myperceptions.Target.IsLivekkj())
            myperceptions.Target.OnDamagekkj(LemuData.AP);
    }
 
    void Start()
    {
        GameObject obj = Instantiate(Resources.Load("UI/MonsterHPBar"), GameObject.Find("Canvas").transform) as GameObject;
        kkjmonsterUIBar = obj.GetComponent<MonsterUIBar>();
        kkjmonsterUIBar.Initialize(EnemyHead, 0.0f);


        ChangeState(STATE.CREATE);
    }

   

    // Update is called once per frame
    void Update()
    {
        ProcessSTATE();
     
          }

    IEnumerator Waitting(float Mms, UnityAction done) //��� 
    {
        yield return new WaitForSeconds(Mms);
        done?.Invoke();
    }

    void RoamingToRandomPosition() //�������� �Դ� ���� �ϰ� �����ϱ� 
    {
        Vector3 pos = new Vector3();
        pos.x = StartPos.x + Random.Range(-3.0f, 3.0f);
        pos.z = StartPos.z + Random.Range(-3.0f, 3.0f);
       base.StartRunkkj(pos, 1.0f, 1.0f, () => StartCoroutine(Waitting(Random.Range(1.0f, 1.0f), RoamingToRandomPosition)));
        Debug.Log("Move");
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
         myperceptions.FindTarget = FindTargetkkj;
                myAnim.SetTrigger("Spawn");
                LemuData.HP = 80.0f;
                LemuData.MoveSpeed = 5.0f;
                LemuData.AP = 12.0f;
                LemuData.AttackRange = 6.0f;
                LemuData.AttackDelay = 3.0f;
                              ChangeState(STATE.RUN);
                break;
            case STATE.RUN:
                  //    if ((Vector3.Distance(Target.transform.position, this.transform.position) > LemuData.AttackRange))
                 //  {
                //     myperceptions.FindTarget = FindTargetkkj;
                //    }
                StartCoroutine(Waitting(Random.Range(1.0f, 1.0f), RoamingToRandomPosition)); //�÷��̾� ��ó���� ��ȯ�� �ϵ�, �̰͵� ��������
                //��ȯ�� �ϵ��� ������ �Ѵ�.
                break;
            case STATE.BATTLE:
             StopAllCoroutines();
              base.AttackTargetkkj(myperceptions.Target, LemuData.AttackRange, LemuData.AttackDelay, () => ChangeState(STATE.RUN));
             //Ÿ�� �������� ����ų� ������ �ٷ� �ٵ��� 
                Debug.Log("�÷��̾�����߽��ϴ�. ���� ���� ���ϴ�.");
              InvokeRepeating("Firekkj", 3.0f, 1.1f); //���̾ ���� �ݺ� (?) 
                break;
            case STATE.DEAD:
                StopAllCoroutines();
                CancelInvoke("Firekkj"); //���̾�� ������ �ʰ� �ϰ�
                myAnim.SetTrigger("Dead"); //Dead
                Debug.Log("LEMU is DEAD"); 
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
                break;
            case STATE.DEAD:
                break;
        }
    }



}



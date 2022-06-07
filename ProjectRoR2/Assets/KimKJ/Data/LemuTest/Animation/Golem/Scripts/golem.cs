using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


public class golem : MoveMove, BattlecombatSystem
{

    public enum STATE
    {
        NONE,CREATE,RUN,BATTLE,DEAD
    }
    Transform Target;


    public Transform myTarget
    {
        get => Target;
        set => Target = null;
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
       //     kkjmonstergolemUIBar.myHP.value = _curHP / GolemData.HP;
        }
    }

    AIPerceptionGolem _aiperception = null;
    AIPerceptionGolem myperceptions
    {
        get
        {
            if (_aiperception == null)
            {
                _aiperception = this.GetComponentInChildren<AIPerceptionGolem>();

            }
            return _aiperception;
        }
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
    float _curHP = 0.0f;
    public AttackSystem myAttackSystem = null;
    public CharacterStatkkj GolemData;
    public STATE mystate = STATE.NONE;
   public GameObject LazerBeem;
   public Transform Eye;
    public Transform EnemyHead; // HP넣을곳 
    public float GolemUIBarHeight =480.0f; // 골렘  UI와 체력 연동 
   // public BattleCombatSystem myBattleCombatSystem = null;
 //   GolemUIBar kkjmonstergolemUIBar = null;

    Vector3 StartPos;

    void FindTargetkkj()
    {
        ChangeState(STATE.BATTLE);
    }

    public bool IsLivekkj() //주석 처리해야됨.
    {
        return mystate == STATE.RUN || mystate == STATE.BATTLE;
    }
    public void OnDamagekkj(float Damage)
    {
        if (mystate == STATE.DEAD)
            return;
        GolemData.HP -= Damage;

        Debug.Log("골렘이 공격 받고 있습니다.");
        if (GolemData.HP <= 0)
        {
            Debug.Log("골렘이 죽었습니다. 테스트 종료");
            ChangeState(STATE.DEAD);
            CancelInvoke("Firekkj2");
            myAnim.SetTrigger("Dead");
        }
        else
        {
            myAnim.SetTrigger("Hit");
        }
    }


   
     void Firekkj2()
    {
        myAnim.SetTrigger("Attack");
   GameObject Firebeem = Instantiate(LazerBeem,  Eye.position,Eye.rotation);

    }

    void OnAttackkkj2()
    {
        if (myperceptions.Target.IsLivekkj())
            myperceptions.Target.OnDamagekkj(GolemData.AP);
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        GameObject obj = Instantiate(Resources.Load("UI/GolemUIBar"), GameObject.Find("Canvas").transform) as GameObject;
        kkjmonstergolemUIBar = obj.GetComponent<GolemUIBar>();
        kkjmonstergolemUIBar.Initialize(EnemyHead, 0.0f);
        */
        ChangeState(STATE.CREATE);
    //   InvokeRepeating("Firekkj2", 0.0f, 3.0f);
              
    }



    // Update is called once per frame
    void Update()
    {
   
        ProcessSTATE();
    }
    IEnumerator DeadDisappeerkkj()
    {
        yield return new WaitForSeconds(5.0f);
        float dist = 1.0f;
        while (dist > 0.0f)
        {
            float delta = Time.deltaTime * 1.0f;
            this.transform.Translate(-Vector3.up * delta);
            dist -= delta;
            yield return null;
        }
        Destroy(this.gameObject);
    }

    IEnumerator Waitting(float Mms, UnityAction done) //대기 
    {
        yield return new WaitForSeconds(Mms);
        done?.Invoke();
    }

    void RoamingToRandomPosition() //랜덤으로 왔다 갔다 하게 설정하기 
    {
        Vector3 pos = new Vector3();
        pos.x = StartPos.x + Random.Range(-3.0f, 3.0f);
        pos.z = StartPos.z + Random.Range(-3.0f, 3.0f);
        base.StartRunkkj(pos, 1.0f, 1.0f, () => StartCoroutine(Waitting(Random.Range(1.0f, 1.0f), RoamingToRandomPosition)));
        Debug.Log("Move");
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
               GolemData.HP = 480.0f;
                GolemData.AP = 20.0f;
                GolemData.AttackRange = 12.0f;
                GolemData.AttackDelay = 5.0f;
                ChangeState(STATE.RUN);
                break;
            case STATE.RUN:
                //   if ((Vector3.Distance(Target.transform.position, this.transform.position) >= GolemData.AttackRange))
                //    {
                //     myperceptions.FindTarget = FindTargetkkj;
                //       }
                StartCoroutine(Waitting(Random.Range(1.0f, 1.0f), RoamingToRandomPosition));
                break;
            case STATE.BATTLE:
                StopAllCoroutines();
                base.AttackTargetkkj(myperceptions.Target, GolemData.AttackRange, GolemData.AttackDelay, () => ChangeState(STATE.RUN));
                   InvokeRepeating("Firekkj2", 0.0f, 3.0f);
                break;
            case STATE.DEAD:
                StopAllCoroutines();
                CancelInvoke("Firekkj2");
                myAnim.SetTrigger("Dead");
                StartCoroutine(DeadDisappeerkkj());
                break;
        }
    }

    void ProcessSTATE()
    {
        switch(mystate)
        {
            case STATE.CREATE:
                if (myAnim.GetBool("Spawning") && mystate != STATE.RUN)
                {
                    ChangeState(STATE.RUN);
                }
                break;
            case STATE.RUN:
          //      base.StartRunkkj(Target.transform.position, GolemData.MoveSpeed, GolemData.AttackRange);
                break;
            case STATE.BATTLE:
                break;
            case STATE.DEAD:
                break;
         
        }    
    }
 
}

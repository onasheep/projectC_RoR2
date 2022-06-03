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

    float _curHP = 0.0f; //초기화?
    public AttackSystem myAttackSystem = null; //코만도 (플레이어)와의 연동 
    public STATE mystate = STATE.NONE;
    public  CharacterStatkkj LemuData; //캐릭터 데이터 (공격력이라던가 체력 등) 
    public Transform firemouth;  //파이어볼 발사 위치 
    public GameObject FireBoom; //파이어볼 
    public Transform EnemyHead; // HP넣을곳 
    public float MonsterUIBarHeight = 80.0f; // 레무리안  UI와 체력 연동 
    MonsterUIBar kkjmonsterUIBar = null;

    Vector3 StartPos;

    void FindTargetkkj() //타겟 추적 
    {
        ChangeState(STATE.BATTLE);
    }

    public bool IsLivekkj()  //뛰거나 or 공격을 하거나 
    {
        return mystate == STATE.RUN || mystate == STATE.BATTLE;
    }
    public void OnDamagekkj(float Damage) //데미지 받는것 
    {
       if (mystate == STATE.DEAD) 
          return;
        LemuData.HP -= Damage;
       
        Debug.Log("레무리안이 공격 받고 있습니다.");
        if (LemuData.HP <= 0)
        {
                   Debug.Log("레무리안이 죽었습니다. 테스트 종료");
            ChangeState(STATE.DEAD);
            CancelInvoke("Firekkj");
            myAnim.SetTrigger("Dead");
           
        }
        else
        {
            myAnim.SetTrigger("Hit");
        }
       }

    void Firekkj() //타겟을 향해 공격 
    {
        myAnim.SetTrigger("Attack");
        GameObject Firebool = Instantiate(FireBoom, firemouth.position, firemouth.rotation);
     
    }

    

    void OnAttackkkj() //공격시 처리 
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


    IEnumerator DeadDisappeerkkj() //몬스터가 죽고 난 이후 처리 
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
                StartCoroutine(Waitting(Random.Range(1.0f, 1.0f), RoamingToRandomPosition)); //플레이어 근처에서 소환을 하되, 이것도 랜덤으로
                //소환을 하도록 설정을 한다.
                break;
            case STATE.BATTLE:
             StopAllCoroutines();
              base.AttackTargetkkj(myperceptions.Target, LemuData.AttackRange, LemuData.AttackDelay, () => ChangeState(STATE.RUN));
             //타겟 감지에서 벗어나거나 죽으면 바로 뛰도록 
                Debug.Log("플레이어를감지했습니다. 공격 모드로 들어갑니다.");
              InvokeRepeating("Firekkj", 3.0f, 1.1f); //파이어볼 공격 반복 (?) 
                break;
            case STATE.DEAD:
                StopAllCoroutines();
                CancelInvoke("Firekkj"); //파이어볼은 나가지 않게 하고
                myAnim.SetTrigger("Dead"); //Dead
                Debug.Log("LEMU is DEAD"); 
                StartCoroutine(DeadDisappeerkkj()); //이제 사라지도록 
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



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lemurian : MoveMove, BattlecombatSystem
{

    public AttackSystem myAttackSystem = null; //(플레이어)와의 연동
    public STATE mystate = STATE.NONE;
    [SerializeField]
    public  CharacterStatkkj LemuData; //캐릭터 데이터 (공격력이라던가 체력 등) 
    public Transform firemouth;  //파이어볼 발사 위치 
    public GameObject FireBoom; //파이어볼 
    public GameObject myperceptionlemus1;
    AIPerceptionlemu myperceptionlemus;
    public Transform HpBar; // HP넣을곳 
    float AtkDelayCheck; //공격 딜레이 체크 
    public float MonsterUIBarHeight = 0.0f; // 레무리안  UI와 체력 연동 
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
    AIPerceptionlemu myperceptionlemus //감지범위 
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
    public float HP //체력 설정 (UI 연동?) 
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
    
    
    void FindTargetkkj() //타겟 추적 
    {
        ChangeState(STATE.BATTLE);
    }

    public bool IsLivekkj()   
    {
        return mystate == STATE.RUN || mystate == STATE.BATTLE;
    }
    public void OnDamagekkj(float Damage) //레무리안이 데미지를 받을때 
    {
        if (mystate == STATE.DEAD)
        { return; }

        HP -= Damage;
        myAnim.SetTrigger("Hit"); //맞고 있을때 
         //    Invoke("HpBarSet", 0.0f);
 
        _curHP -= Damage;
        Debug.Log("레무리안이 공격 받고 있습니다.");
        if (HP <= 0.0f)
        {
            HP = 0.0f;
            Debug.Log("레무리안이 죽었습니다. 테스트 종료");
       //     Destroy(kkjmonsterUIBar.gameObject);
            StopAllCoroutines();
            myAnim.SetTrigger("Dead");
            ChangeState(STATE.DEAD);
        }
 
       }
    
    /*
    void HpBarSet()
    {
        
            GameObject obj = Instantiate(Resources.Load("UI Prefab/MonsterHPBar"), GameObject.Find("InGameUICanvas").transform) as GameObject; //HP 빠 
            kkjmonsterUIBar = obj.GetComponent<MonsterUIBar>(); //이 스크립트를 이용한다. 
            kkjmonsterUIBar.Initialize(HpBar, 2.0f);
 
        Destroy(kkjmonsterUIBar.gameObject, 1.0f);
        
    }
    */

    void Firekkj(Transform Target, float range) //타겟을 향해 공격 
    {
            myAnim.SetTrigger("Attack");
            GameObject Firebool = Instantiate(FireBoom, firemouth.position, firemouth.rotation); //입에서 불꽃이 나가도록 (파이어볼) 
            Vector3 dir = Target.position - firemouth.position;
            Firebool.GetComponent<LemurianFireball>().Shotting(dir, range);
           
    }
  

    void OnAttackkkj() //공격시 처리 
    {
        if (myperceptionlemus.Target.IsLivekkj())
            myperceptionlemus.Target.OnDamagekkj(LemuData.AP); //공격 데미지?
    }
    private void Awake()
    {
        
    }
    void Start()
    {
        
       GameObject obj = Instantiate(Resources.Load("UI Prefab/MonsterHPBarrr"), GameObject.Find("InGameUICanvas").transform) as GameObject; //HP 빠 
       kkjmonsterUIBar = obj.GetComponent<MonsterUIBar>(); //이 스크립트를 이용한다. 
       kkjmonsterUIBar.Initialize(HpBar, 2.0f);
        
        myperceptionlemus = myperceptionlemus1.GetComponent<AIPerceptionlemu>();
        ChangeState(STATE.CREATE);
    }



    // Update is called once per frame
    void Update()
    {
      ProcessSTATE();
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
                myAnim.SetTrigger("Spawn"); // 스폰 
                LemuData.HP = 80.0f; //체력 
                LemuData.MoveSpeed = 5.0f; //움직임 
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
                //타겟 감지에서 벗어나거나 죽으면 바로 뛰도록 
                base.AttackTargetkkj(myperceptionlemus.Target, LemuData.AttackRange, LemuData.AttackDelay, () => ChangeState(STATE.RUN));
                Debug.Log("플레이어를감지했습니다. 공격 모드로 들어갑니다.");
                //   Firekkj(myperceptionlemus.Target.transform, LemuData.AttackRange)
   
                    InvokeRepeating("Firekkj", 3.0f ,1.1f); //파이어볼 공격 반복 (?) 
                  if (LemuData.HP <= 0.0f)
                  {
                      ChangeState(STATE.DEAD);
                  }
                  break;
              case STATE.DEAD:
                  StopAllCoroutines();
                  myAnim.SetTrigger("Dead"); //Dead
                  CancelInvoke("Firekkj"); //파이어볼은 나가지 않게 하고
                  Debug.Log("레무리안이 죽었습니다."); 
                  StartCoroutine(DeadDisappeerkkj()); //이제 사라지도록 
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



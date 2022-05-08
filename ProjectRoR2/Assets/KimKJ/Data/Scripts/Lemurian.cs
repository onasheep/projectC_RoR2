//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.Events;

//public class Lemurian : MoveMove , BattleSystem
//{
//    public enum STATE
//    {
//        NONE, CREATE, RUN, BATTLE, DEAD
//    }
//    AIPerception _aiperception = null;
//    AIPerception myperceptions
//    {
//        get
//        {
//            if (_aiperception == null)
//            {
//                _aiperception = this.GetComponentInChildren<AIPerception>();

//            }
//            return _aiperception;
//        }
//    }

//    float _curHP = 0.0f;
//    public float HPChange
//    {
//        get
//        {
//            return _curHP;
//        }
//        set
//        {
//            _curHP += value;
//            if (_curHP < 0.0f) _curHP = 0.0f;
//         //   myStarBar.myHP.value = _curHP / myStat.HP;
//        }
//    }

//    public STATE mystate = STATE.NONE;
//    public GameObject Target;
//  public  CharacterStat LemuData;
// // public CharacterStat myStat;
//    public Transform firemouth; //입에서 파이리 마냥 불덩이가 나옴 (5개나!) 
//    public GameObject FireBoom;

//    Vector3 StartPos;

//    void FindTarget()
//    {
//        ChangeState(STATE.BATTLE);
//    }

//    public bool IsLive()
//    {
//        return mystate == STATE.RUN || mystate == STATE.BATTLE;
//    }
//    public void OnDamage(float Damage)
//    {
//       if (mystate == STATE.DEAD) 
//          return;
//        LemuData.HP -= Damage;
       
//        Debug.Log("레무리안이 공격 받고 있습니다.");
//        if (LemuData.HP <= 0)
//        {
//                   Debug.Log("레무리안이 죽었습니다. 테스트 종료");
//            ChangeState(STATE.DEAD);
//         //   myAnim.SetTrigger("Dead");
           
//        }
//        else
//        {
//            myAnim.SetTrigger("Hit");
//        }
//       }

//    void Fire()
//    {
//        myAnim.SetTrigger("Attack");
//        GameObject Firebool = Instantiate(FireBoom, firemouth.position, firemouth.rotation);
     
//    }

//    void OnAttack()
//    {
//        if (myperceptions.Target.IsLive())
//            myperceptions.Target.OnDamage(LemuData.AP);
//    }
 
//    void Start()
//    {
//    //   InvokeRepeating("Fire", 3.0f, 2.0f);
//            ChangeState(STATE.CREATE);
//    }

   

//    // Update is called once per frame
//    void Update()
//    {
//        ProcessSTATE();
//       }

//    IEnumerator DeadDisappeer()
//    {
//        yield return new WaitForSeconds(1.0f);
//        float dist = 1.0f;
//        while(dist > 0.0f)
//        {
//            float delta = Time.deltaTime * 0.5f;
//            this.transform.Translate(-Vector3.up * delta);
//            dist -= delta;
//            yield return null;
//        }
//        Destroy(this.gameObject);
//    }

//    void ChangeState(STATE s)
//    {
//        if (mystate == s) return;
//        mystate = s;
//        switch(mystate)
//        {
//            case STATE.CREATE:
//            myperceptions.FindTarget = FindTarget;
//                myAnim.SetTrigger("Spawn");
//                LemuData.HP = 80.0f;
//                LemuData.AP = 12.0f;
//                LemuData.AttackRange = 1.0f;
//                    LemuData.AttackDelay = 3.0f;
//                              ChangeState(STATE.RUN);
//                break;
//            case STATE.RUN:
//                if ((Vector3.Distance(Target.transform.position, this.transform.position) > LemuData.AttackRange))
//                   {
//                    //      myperceptions.FindTarget = FindTarget;
//                  // base.StartRun(Target.transform.position, LemuData.MoveSpeed, LemuData.AttackRange);
//                }
//          //    base.StartRun(Target.transform.position, LemuData.MoveSpeed, LemuData.AttackRange);
//                break;
//            case STATE.BATTLE:
//             StopAllCoroutines();
//              base.AttackTarget(myperceptions.Target, LemuData.AttackRange, LemuData.AttackDelay, () => ChangeState(STATE.RUN));
                         
//               Debug.Log("플레이어를감지했습니다. 공격 모드로 들어갑니다.");
//              InvokeRepeating("Fire", 3.0f, 1.5f);
//                break;
//            case STATE.DEAD:
//                StopAllCoroutines();
//                CancelInvoke("Fire");
//                myAnim.SetTrigger("Dead");
//                Debug.Log("LEMU is DEAD");
       
//                    StartCoroutine(DeadDisappeer());
//               break;

//        }
//    }

//    void ProcessSTATE()
//    {
//        switch(mystate)
//        {
//            case STATE.CREATE:
//                if(myAnim.GetBool("Spawning") && mystate != STATE.RUN)
//                {
//                    ChangeState(STATE.RUN);
//                }
//                break;
//            case STATE.RUN:
//                base.StartRun(Target.transform.position, LemuData.MoveSpeed, LemuData.AttackRange);
//                break;
//            case STATE.BATTLE:
//                                    break;
//            case STATE.DEAD:
//                break;
//        }
//    }

//}



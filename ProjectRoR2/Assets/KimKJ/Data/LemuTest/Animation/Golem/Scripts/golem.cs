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
    public Transform HpBar; // HP³ÖÀ»°÷ 
    public float GolemUIBarHeight =480.0f; // °ñ·½  UI¿Í Ã¼·Â ¿¬µ¿ 
    float AtkDelayCheck;
    GolemUIBar kkjmonstergolemUIBar = null;

    Vector3 StartPos;

    void FindTargetkkj()
    {
        ChangeState(STATE.BATTLE);
    }

    public bool IsLivekkj() //ÁÖ¼® Ã³¸®ÇØ¾ßµÊ.
    {
        return mystate == STATE.RUN || mystate == STATE.BATTLE;
    }
    public void OnDamagekkj(float Damage)
    {
        if (mystate == STATE.DEAD)
        { return; }
        GolemData.HP -= Damage;
        myAnim.SetTrigger("Hit");
        Debug.Log("°ñ·½ÀÌ °ø°Ý ¹Þ°í ÀÖ½À´Ï´Ù.");
        if (GolemData.HP <= 0)
        {
            Debug.Log("°ñ·½ÀÌ Á×¾ú½À´Ï´Ù. Å×½ºÆ® Á¾·á");
            ChangeState(STATE.DEAD);
            StopAllCoroutines();
            myAnim.SetTrigger("Dead");
        }

    }


   
     void Firekkj2(Transform target, float range)
    {
      
            myAnim.SetTrigger("Attack");
            GameObject Firebeem = Instantiate(LazerBeem, Eye.position, Eye.rotation);
            Vector3 dir =  target.position - Eye.position;
            Firebeem.GetComponent<GolemFireball>().Shotting(dir, range);

    }

    void OnAttackkkj2()
    {
        if (myperceptions.Target.IsLivekkj())
            myperceptions.Target.OnDamagekkj(GolemData.AP);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        GameObject obj = Instantiate(Resources.Load("UI Prefab/GolemUIBar"), GameObject.Find("InGameUICanvas").transform) as GameObject;
        kkjmonstergolemUIBar = obj.GetComponent<GolemUIBar>();
        kkjmonstergolemUIBar.Initialize(HpBar, 0.0f);
        
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
                GolemData. AttackDelay= 2.0f;
                ChangeState(STATE.RUN);
                break;
            case STATE.RUN:
                //   if ((Vector3.Distance(Target.transform.position, this.transform.position) >= GolemData.AttackRange))
                //    {
                //     myperceptions.FindTarget = FindTargetkkj;
                //       }
                if(myTarget)
                {

                }
                break;
            case STATE.BATTLE:
                StopAllCoroutines();
                base.AttackTargetkkj(myperceptions.Target, GolemData.AttackRange, GolemData.AttackDelay, () => ChangeState(STATE.RUN));
                
                break;
            case STATE.DEAD:
                StopAllCoroutines();
                myAnim.SetTrigger("Dead");
                CancelInvoke("Firekkj2");
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
                AtkDelayCheck += Time.deltaTime;
                if (AtkDelayCheck >= GolemData.AttackDelay)
                {
                    Firekkj2(myperceptions.Target.transform, GolemData.AttackRange);
                    AtkDelayCheck = 0.0f;
                }
                if (GolemData.HP <= 0.0f)
                {
                    StopAllCoroutines();
                    myAnim.SetTrigger("Dead");
                    ChangeState(STATE.DEAD);
                }
                break;
            case STATE.DEAD:
                break;
         
        }    
    }
 
}

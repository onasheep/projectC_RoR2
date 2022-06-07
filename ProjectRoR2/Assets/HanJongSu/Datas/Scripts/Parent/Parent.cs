using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : HJSMonster, HJSCombatSystem
{
    public enum STATE
    {
        EMPTY, CREATE, MOVE, BATTLE, DIE
    }

    public STATE myState = STATE.EMPTY;
    [SerializeField] Transform Target;
    public Transform myTarget
    {
        get => Target;
        set => Target = value;
    }
    public Transform Hand;
    float IsAir = 0.0f;
    float MoveDelay = 10.0f;
    public HJSMonsterData ParentData;

    public void HJSGetDamage(float Damage)
    {
        if (myState == STATE.DIE) return;
        ParentData.HP -= Damage;
        if (ParentData.HP <= 0) ChangeState(STATE.DIE);

    }

    public bool HJSIsAlive()
    {
        return myState != STATE.DIE;
    }

    void Start()
    {
        ChangeState(STATE.CREATE);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessState();
    }

    protected void FixedUpdate()
    {
        if (Mathf.Approximately(myRigidBody.velocity.y, 0.0f))
        {
            if (IsAir != 0.0f)
            {
                IsAir = 0.0f;
            }
        }
        else
        {
            IsAir += Time.fixedDeltaTime;
            if (IsAir >= 10.0f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator Slap()
    {
        myAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(ParentData.AttackSpeed);
        ChangeState(STATE.MOVE);
    }

    IEnumerator Waiting10Sec()
    {
        while(!myTarget)
        {
            yield return new WaitForSeconds(10.0f);  
        }
        ChangeState(STATE.BATTLE);
    }

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.CREATE:
                ParentData.HP = 585;
                ParentData.MaxHP = ParentData.HP;
                ParentData.AD = 16.0f;
                ParentData.MoveSpeed = 5.0f;
                ParentData.AttackRange = 5.0f;
                ParentData.AttackSpeed = 3.0f;
                break;
            case STATE.MOVE:
                if (myTarget)
                {
                    base.StartMoveTr(myTarget, ParentData.MoveSpeed, ParentData.AttackRange, () => ChangeState(STATE.BATTLE));
                    
                }
                else
                {
                    StopAllCoroutines();
                    if (myAnim.GetBool("IsMoving")) myAnim.SetBool("IsMoving", false);
                }   
                
                break;
            case STATE.BATTLE:
                StopAllCoroutines();
                StartCoroutine(Slap());
              

                break;
            case STATE.DIE:
                StopAllCoroutines();
                myAnim.SetTrigger("Die");
                Destroy(this.gameObject, 3.0f);
                break;

        }
    }

    void ProcessState()
    {
        switch (myState)
        {
            case STATE.CREATE:
                if (myAnim.GetBool("IsCreate") && myState != STATE.MOVE)
                {
                    ChangeState(STATE.MOVE);
                }
                break;
            case STATE.MOVE:
                
                if (myTarget)
                {
                    if(MoveDelay >= 0.5f)
                    {
                        base.StartRot(myTarget);
                        MoveDelay = 0.0f;
                    }
                    MoveDelay += Time.deltaTime;
                }
                else if(!myTarget)
                {
                    StartCoroutine(Waiting10Sec());  
                }
                
                break;
            case STATE.BATTLE:
                break;
            case STATE.DIE:
                break;
        }
    }

   
}

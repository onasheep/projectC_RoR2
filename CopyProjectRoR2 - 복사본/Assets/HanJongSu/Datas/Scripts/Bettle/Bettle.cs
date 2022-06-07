using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bettle : Monster, CombatSystem
{
    public enum STATE
    {
        EMPTY, CREATE, MOVE, BATTLE, DIE
    }

    public STATE myState = STATE.EMPTY;
    public Transform Target;
    
    MonsterData BettleData;


    public void GetDamage(float Damage)
    {
        if (myState == STATE.DIE) return;
        BettleData.HP -= Damage;
        if (BettleData.HP <= 0) ChangeState(STATE.DIE);
        else
        {
            myAnim.SetTrigger("Damage");
        }

    }

    public bool IsAlive()
    {
        return myState != STATE.DIE;
    }
    void Start()
    {
        ChangeState(STATE.CREATE);
        
    }

    void Update()
    {
        ProcessState();
    }

    void Attacking()
    {
        myPlayerDr.myTarget.GetDamage(BettleData.AD);
    }

    IEnumerator HeadBute()
    {
        myAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(1.0f);
        ChangeState(STATE.MOVE);
    }
    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case STATE.CREATE:
                BettleData.HP = 80.0f;
                BettleData.AD = 12.0f;
                BettleData.MoveSpeed = 2.0f;
                BettleData.AttackRange = 3.0f;
                myAnimEvent.Attack += Attacking;
                break;
            case STATE.MOVE:
                base.StartMove(Target, BettleData.MoveSpeed, BettleData.AttackRange);
                if(Vector3.Distance(Target.position, this.transform.position) <= BettleData.AttackRange)
                {
                    ChangeState(STATE.BATTLE);
                }
                break;
            case STATE.BATTLE:
                StopAllCoroutines();
                StartCoroutine(HeadBute());
                

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
        switch(myState)
        {
            case STATE.CREATE:
                if(myAnim.GetBool("IsCreate") && myState != STATE.MOVE)
                {
                    ChangeState(STATE.MOVE);
                }
                break;
            case STATE.MOVE:
                base.StartRot(Target);
                break;
            case STATE.BATTLE:
                break;
            case STATE.DIE:
                break;
        }
    }
}
 
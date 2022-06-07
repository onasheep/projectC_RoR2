using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettleQueen : HJSMonster
{
    public enum STATE
    {
        EMPTY, CREATE, MOVE, BATTLE, DIE
    }

    public STATE myState = STATE.EMPTY;
    public GameObject Target;
    HJSMonsterData QueenData;

    void Start()
    {
        ChangeState(STATE.CREATE);
    }

    void Update()
    {
        ProcessState();
    }

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.CREATE:
                QueenData.HP = 2100.0f;
                QueenData.AD = 25.0f;
                QueenData.MoveSpeed = 6.0f;
                QueenData.AttackRange = 3.0f;

                break;
            case STATE.MOVE:
                //base.StartMove(Target.transform.position, QueenData.MoveSpeed, QueenData.AttackRange);
                
                break;
            case STATE.BATTLE:
                break;
            case STATE.DIE:
                break;
        }
    }

    void ProcessState()
    {
        switch (myState)
        {
            case STATE.CREATE:
                if (myAnim.GetBool("IsCreate"))
                {
                    ChangeState(STATE.MOVE);
                }
                break;
            case STATE.MOVE:
                if(Target.gameObject.transform.position != this.transform.position)
                {
                    myAnim.SetTrigger("Moving");
                }
                break;
            case STATE.BATTLE:
                break;
            case STATE.DIE:
                break;
        }
    }
}


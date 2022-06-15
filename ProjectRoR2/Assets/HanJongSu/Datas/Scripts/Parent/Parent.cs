using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    float MoveDelay = 0.0f;
    public Transform Hand;
    private AudioSource audioSource;
    SphereCollider Dr = null;
    public GameObject DrObject;
    UnityAction DieAction = null;

    public HJSMonsterData ParentData;

    public void HJSGetDamage(float Damage, UnityAction dieAction = null)
    {
        if (myState == STATE.DIE)
        {
            Debug.Log("이미 죽어 리턴");
            return;
        }
        if(DieAction == null)
        {

        }
        ParentData.HP -= Damage;
        Instantiate(GetDamageEffect, this.transform.position, Quaternion.identity, this.transform);
        Debug.Log("부모몬스터가 " + Damage + "만큼의 데미지를 입어 현재 체력은 " + ParentData.HP);
        if (ParentData.HP <= 0)
        {
            ParentData.HP = 0;
            Debug.Log("부모 몬스터가 죽음 상태로 돌입");
            ChangeState(STATE.DIE);
        }

    }

    public bool HJSIsAlive()
    {
        return myState != STATE.DIE;
    }


    void ColliderSizeUp(float size, bool wait = false)
    {
        Dr = this.GetComponentInChildren<SphereCollider>();
        Dr.isTrigger = true;
        if (wait) StartCoroutine(WaitingSecP(size));
        else Dr.radius = size;  
        /*
        if (wait) StartCoroutine(WaitingSecP(size));
        else DrObject.GetComponent<SphereCollider>().radius = size; 
        */
    }

    public void SoundPlay(AudioClip clip)
    {
        audioSource.volume = 1.0f;
        audioSource.PlayOneShot(clip);
    }

    void Start()
    {
        //ColliderSizeUp(0.1f);
        audioSource = GetComponent<AudioSource>();
        ChangeState(STATE.CREATE);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessState();
    }


    IEnumerator Slap()
    {
        if (Target != null)
        {
            myAnim.SetTrigger("Attack");
            SoundPlay(AtkSound);
        }

        yield return new WaitForSeconds(ParentData.AttackSpeed);
        ChangeState(STATE.MOVE);
    }

    IEnumerator WaitingSecP(float size)
    {
        yield return new WaitForSeconds(4.0f);
        DrObject.GetComponent<SphereCollider>().radius = size;
    }

    IEnumerator WaitingSecForBattle()
    {
        while(!myTarget)
        {
            yield return new WaitForSeconds(1.0f);  
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
                SoundPlay(SpawnSound);
                ParentData.HP = 585;
                ParentData.MaxHP = ParentData.HP;
                ParentData.AD = 16.0f;
                ParentData.MoveSpeed = 6.0f;
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
                SoundPlay(DieSound);
                myAnim.SetTrigger("Die");
                StartCoroutine(Disapearing());
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
                    DrObject.SetActive(true);
                    //ColliderSizeUp(28.0f);
                    ChangeState(STATE.MOVE);
                }
                break;
            case STATE.MOVE:
                if (myTarget)
                {
                    if(MoveDelay >= 0.3f)
                    {
                        base.StartRot(myTarget);
                        MoveDelay = 0.0f;
                    }
                    MoveDelay += Time.deltaTime;
                }
                else if(!myTarget)
                {
                    StartCoroutine(WaitingSecForBattle());  
                }
                
                break;
            case STATE.BATTLE:
                break;
            case STATE.DIE:
                break;
        }
    }

   
}

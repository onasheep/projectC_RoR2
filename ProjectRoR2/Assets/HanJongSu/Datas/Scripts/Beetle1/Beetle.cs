using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Beetle : HJSMonster, HJSCombatSystem
{
    public enum HJSSTATE
    {
        EMPTY, CREATE, MOVE, BATTLE, DIE
    }

    public HJSSTATE myState = HJSSTATE.EMPTY;
    [SerializeField] Transform Target;
    public Transform myTarget
    {
        get => Target;
        set => Target = value;
    }

    [SerializeField]
    public HJSMonsterData BettleData;
    private AudioSource audioSource;
    public GameObject DrObject;
    SphereCollider Dr = null;
    float MoveDelay = 0.0f;
    float myTime = 0.0f;

    UnityAction DieAction = null;

    public void HJSGetDamage(float Damage, UnityAction dieAction = null)
    {
        if (myState == HJSSTATE.DIE)
        {
            Debug.Log("이미 죽어 리턴");
            return;
        }
        if (DieAction == null)
        {

        }
        myAnim.SetTrigger("Damage");
        Instantiate(GetDamageEffect, this.transform.position ,Quaternion.identity, this.transform);
        BettleData.HP -= Damage;
        //Instantiate()
        Debug.Log("딱정벌래가 " + Damage + "만큼의 데미지를 입어 현재 체력은 " + BettleData.HP);

        if (BettleData.HP <= 0)
        {
            BettleData.HP = 0;
            Debug.Log("딱정벌레가 죽음 상태로 돌입");
            ChangeState(HJSSTATE.DIE);
        }
    }

    void ColliderSizeUp(float size, bool wait = false)
    {
        Dr = this.GetComponentInChildren<SphereCollider>();
        Dr.isTrigger = true;
        if (wait) StartCoroutine(WaitingSec(size));
        else Dr.radius = size;
        /*if(wait)StartCoroutine(WaitingSec(size)); 
        else DrObject.GetComponent<SphereCollider>().radius = size;*/
    }

    public bool HJSIsAlive()
    {
        return myState != HJSSTATE.DIE;
    }
    void Start()
    {
        
        audioSource = this.GetComponent<AudioSource>();
        //ColliderSizeUp(0.1f);
        ChangeState(HJSSTATE.CREATE);  
    }

    void Update()
    { 
        ProcessState();   
    }

    
    public void SoundPlay(AudioClip clip)
    {
        audioSource.volume = 0.10f;
        audioSource.PlayOneShot(clip);
    }
    IEnumerator HeadBute()
    {
        SoundPlay(AtkSound);
        myAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(BettleData.AttackSpeed);
        ChangeState(HJSSTATE.MOVE);
    }

    IEnumerator WaitingSec(float size)
    {

       yield return new WaitForSeconds(4.0f);
       DrObject.GetComponent<SphereCollider>().radius = size;
    }

    void ChangeState(HJSSTATE s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case HJSSTATE.CREATE:
                SoundPlay(SpawnSound);
                BettleData.HP = 80.0f;
                BettleData.MaxHP = BettleData.HP;
                BettleData.AD = 12.0f;
                BettleData.MoveSpeed = 1.0f;
                BettleData.AttackSpeed = 1.5f;
                BettleData.AttackRange = 3.0f;
                BettleData.GainGold = 12.0f;
                BettleData.GainExp = 10.0f;
                ColliderSizeUp(28.0f, true);
                break;
            case HJSSTATE.MOVE:
                
                if (myTarget)
                {
                    
                }
                break;
            case HJSSTATE.BATTLE:
                StopAllCoroutines();
                StartCoroutine(HeadBute());
                
                break;
            case HJSSTATE.DIE:
                StopAllCoroutines();
                SoundPlay(DieSound);
                myAnim.SetTrigger("Die");
                DieAction?.Invoke();
                StartCoroutine(Disapearing());
                break;
                
        }
    }

    void ProcessState()
    {
        switch(myState)
        {
            case HJSSTATE.CREATE:
                if(myAnim.GetBool("IsCreate") && myState != HJSSTATE.MOVE)
                {
                    //ColliderSizeUp(28.0f, true);
                    DrObject.SetActive(true);
                    ChangeState(HJSSTATE.MOVE);
                }
                break;
            case HJSSTATE.MOVE:
                if (myTarget)
                {
                    if ((Vector3.Distance(myTarget.position, this.transform.position) > BettleData.AttackRange) && (MoveDelay >= BettleData.MoveSpeed))
                    {
                        myAnim.SetTrigger("Move");
                        base.StartRot(myTarget);
                        MoveDelay = 0.0f;
                    }
                    MoveDelay += Time.deltaTime;
                                     
                    if (Vector3.Distance(myTarget.position, this.transform.position) <= BettleData.AttackRange && !myAnim.GetBool("IsAttacking"))
                        ChangeState(HJSSTATE.BATTLE);
                }
                else
                    StopAllCoroutines();
                   
                break;
            case HJSSTATE.BATTLE:
                break;
            case HJSSTATE.DIE:
                break;
        }
    }
}
 
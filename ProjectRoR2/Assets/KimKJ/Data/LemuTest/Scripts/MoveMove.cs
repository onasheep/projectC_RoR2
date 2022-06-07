using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveMove : MonoBehaviour
{
  
    Vector3 Dir = Vector3.zero;
    Animator _anim = null;
    protected Animator myAnim
    {
        get
        {
            if (_anim == null) _anim = this.GetComponentInChildren<Animator>();
            return _anim;
        }
    }


  //  Rigidbody.velocity = Vector3.zero;
  Rigidbody  rigidbody;
    public  float RotSpeed = 360.0f;
    public float MoveSpeed = 3.0f;
    Coroutine moveRoutine = null;
    Coroutine rotRoutine = null;
    Vector3 TargetPos = Vector3.zero;

    protected void StartRunkkj(Vector3 pos, float Mms = 0.0f, float Mar = 0.0f, UnityAction done = null) //RUN 
    {
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(Movingkkj(pos, Mms, Mar, done));
        if (rotRoutine != null) StopCoroutine(rotRoutine);
       rotRoutine = StartCoroutine(Rotatingkkj(pos));
    }

    protected void AttackTargetkkj(BattlecombatSystem Target, float AttackRange, float AttackDelay, UnityAction EndAttack) //Ÿ���� ã������ 
    {
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(Attackingkkj(Target, AttackRange, AttackDelay, EndAttack));
       if (rotRoutine != null) StopCoroutine(rotRoutine);
       rotRoutine = StartCoroutine(LookingAtTargetkkj(Target));
    }



    IEnumerator Movingkkj(Vector3 pos, float Mms, float Mar, UnityAction done) //�����ϋ� 
    {

        float Dist = Vector3.Distance(pos, this.transform.position) - Mar;
        Vector3 oldPos = this.transform.position;
         Debug.Log("���Ͱ� �̵��մϴ�.");
        while (!myAnim.GetBool("Running") && Dist > Mathf.Epsilon)
        {
            myAnim.SetTrigger("Run");

            Dist -= Vector3.Distance(oldPos, this.transform.position);
            Debug.Log(Dist);
            oldPos = this.transform.position;
            //Dist = Vector3.Distance(pos, this.transform.position) - Mar;
          yield return null;
        }
            myAnim.SetBool("Running", true);
          //   myAnim.SetBool("Running", false);

    }

    IEnumerator Rotatingkkj(Vector3 pos) //ȸ�� 
    {
        Vector3 dir = (pos - this.transform.position).normalized;
        Gameutilkkj.CalAngle(myAnim.transform.position, dir, myAnim.transform.right, out ROTATEDATA data);
        float r = Mathf.Acos(Vector3.Dot(this.transform.forward, dir));
        float Rot = 360.0f * (r / Mathf.PI);
        float rotDir = 1.0f;
     //  rigidbody.velocity = Vector3.zero;
        if (Vector3.Dot(this.transform.right, dir) < -Mathf.Epsilon)
        {
            rotDir = -1.0f;
        }
 

        while (Rot > Mathf.Epsilon)
        {
            float delta = 360.0f * Time.smoothDeltaTime;
            if (Rot < delta)
            {
                delta = Rot;
            }
            this.transform.Rotate(Vector3.up * delta * rotDir);
            Rot -= delta;
            yield return null;
        }
        //  rigidbody.velocity = Vector3.zero; // �̰� �־�� �ϳ� ������ 

    }
    IEnumerator LookingAtTargetkkj(BattlecombatSystem Target)
    {
        while (true)
        {
            Gameutilkkj.CalAngle(myAnim.transform.forward, (Target.transform.position - this.transform.position).normalized, 
                myAnim.transform.right, out ROTATEDATA data);
            if (data.Angle > Mathf.Epsilon)
            {
                float delta = Time.smoothDeltaTime * RotSpeed;
                delta = delta > data.Angle ? data.Angle : delta;
               myAnim.transform.Rotate(Vector3.up * delta * data.Dir , Space.World);
            }
            yield return null;
        }
     
    }
  


    IEnumerator Attackingkkj(BattlecombatSystem Target, float AttackRange, float AttackDelay, UnityAction EndAttack = null)
    {
       
        float playTime = AttackDelay;
        while (true)
        {
            if (Target.IsLivekkj() == false) break;
            Vector3 dir = Target.transform.position - this.transform.position;
         //   Debug.Log("�÷��̾� ����");
            float dist = dir.magnitude;
            if (dist > AttackRange)
            {
                myAnim.SetTrigger("Run");
                dir.Normalize();

                float delta = Time.smoothDeltaTime * MoveSpeed;
              delta = delta > dist ? dist : delta;
               
                //    this.transform.Translate(dir * delta, Space.World);

            }
            else
            {
               myAnim.SetBool("Running", false);
                if (myAnim.GetBool("Attacking") == false)
               {
                   playTime += Time.deltaTime;
                   
                    //���ݴ��
                    if (playTime >= AttackDelay)
                   {
                       //����
                       myAnim.SetTrigger("Attack");
                       playTime = 0.0f;
                      
                    }
                }
           }
          
            yield return null;
        }
    }

 
}

 


   
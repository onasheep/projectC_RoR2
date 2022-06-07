using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Monster : Property 
{
    public struct MonsterData
    {
        public float HP;
        public float AD;
        public float MoveSpeed;
        public float AttackSpeed;
        public float AttackRange;
    }

    Coroutine moveRoutine = null;
    Coroutine rotRoutine = null;
    protected void StartMove(Transform Target, float Mms = 0.0f, float Mar = 0.0f)
    {
       
        if( (Vector3.Distance(Target.position, this.transform.position) > Mar) )
        {
            if (moveRoutine != null) StopCoroutine(moveRoutine);
            moveRoutine = StartCoroutine(Moving(Target, Mms, Mar));
        }
    }
    protected void StartRot(Transform Target)
    {
        if (rotRoutine != null) StopCoroutine(rotRoutine);
        rotRoutine = StartCoroutine(Rotating(Target));
    }
    /*IEnumerator Following(Transform target, )
    {
        
        Vector3 dir = target.position - this.transform.position;
    }*/

    IEnumerator Moving(Transform Target, float Mms, float Mar)
    {
        
        float Dist = Vector3.Distance(Target.position, this.transform.position) - Mar;
        while (!myAnim.GetBool("IsMoving") && Dist > Mathf.Epsilon)
        {
            myAnim.SetTrigger("Move");
            Dist = Vector3.Distance(Target.position, this.transform.position) - Mar;
            yield return new WaitForSeconds(Mms);
        }
    }

    IEnumerator Rotating(Transform Target)
    {
        Vector3 dir = (Target.position - this.transform.position).normalized;
        float r = Mathf.Acos(Vector3.Dot(this.transform.forward, dir));
        float Rot = 180 * (r / Mathf.PI);
        float rotDir = 1.0f;
        if (Vector3.Dot(this.transform.right, dir) < -Mathf.Epsilon)
        {
            rotDir = -1.0f;
        }

        while (Rot > Mathf.Epsilon)
        {
            float delta = 360.0f * Time.deltaTime;
            if (Rot < delta)
            {
                delta = Rot;
            }
            this.transform.Rotate(Vector3.up * delta * rotDir, Space.World);
            Rot -= delta;
            yield return null;

            dir = (Target.position - this.transform.position).normalized;
            r = Mathf.Acos(Vector3.Dot(this.transform.forward, dir));
            Rot = 180 * (r / Mathf.PI);
            rotDir = 1.0f;
            if (Vector3.Dot(this.transform.right, dir) < -Mathf.Epsilon)
            {
                rotDir = -1.0f;
            }
        }

    }
}

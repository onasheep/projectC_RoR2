using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class HJSMonster : HJSProperty 
{
    [Serializable]
    public struct HJSMonsterData
    {
        public float MaxHP;
        public float HP;
        public float AD;
        public float MoveSpeed;
        public float AttackSpeed;
        public float AttackRange;
        public float GainGold;
        public float GainExp;
    }
    [SerializeField]protected AudioClip SpawnSound;
    [SerializeField] protected AudioClip AtkSound;
    [SerializeField] protected AudioClip DieSound;

    Coroutine moveRoutine = null;
    Coroutine rotRoutine = null;
    protected void StartMoveTr(Transform Target, float Mms, float Mar, UnityAction done)
    {
        
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MovingTr(Target, Mms, Mar, done));
        
    }
    protected void StartRot(Transform Target)
    {
        if (rotRoutine != null) StopCoroutine(rotRoutine);
        rotRoutine = StartCoroutine(Rotating(Target));
    }
    /*IEnumerator Following(Transform target )
    {
        
        Vector3 dir = target.position - this.transform.position;
    }*/

    IEnumerator MovingTr(Transform Target, float Mms, float Mar, UnityAction done)
    {
        Vector3 dir = Target.position - this.transform.position;
        Vector3 unitVec = dir.normalized;
        float Dist = dir.magnitude - Mar;
        
        while (Dist > Mathf.Epsilon)
        {
            if(!myAnim.GetBool("IsMoving")) myAnim.SetBool("IsMoving",true);
            float delta = Time.deltaTime * Mms;
            if(Dist < delta)
            {
                delta = Dist;
            }
            this.transform.Translate(unitVec * delta, Space.World);
            dir = Target.position - this.transform.position;
            unitVec = dir.normalized;
            Dist = dir.magnitude - Mar;
            yield return null;
        }
        myAnim.SetBool("IsMoving", false);
        done?.Invoke();
    }

    protected IEnumerator Disapearing()
    {
        yield return new WaitForSeconds(3.0f);
        float dist = 1.0f;
        while (dist > 0.0f)
        {
            float delta = Time.deltaTime * 0.5f;
            this.transform.Translate(-Vector3.up * delta);
            dist -= delta;
            yield return null;
        }

        Destroy(this.gameObject);
    }

    IEnumerator Rotating(Transform Target)
    {
        Vector3 dir = (Target.position - this.transform.position).normalized;
        float r = Mathf.Acos(Vector3.Dot(this.transform.forward, dir));
        float Rot = 180 * (r / Mathf.PI);
        float rotDir = Vector3.Dot(this.transform.right, dir) < -Mathf.Epsilon ? -1.0f : 1.0f;
        /*
        if (Vector3.Dot(this.transform.right, dir) < -Mathf.Epsilon)
        {
            rotDir = -1.0f;
        }*/

        /*float Rot = 180 * (Mathf.Acos(Vector3.Dot(this.transform.forward, (Target.position - this.transform.position).normalized)) / Mathf.PI);
        float rotDir = 1.0f;
        if (Vector3.Dot(this.transform.right, (Target.position - this.transform.position).normalized) < -Mathf.Epsilon)
        {
            rotDir = -1.0f;
        }*/
        
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
        }
        rotRoutine = null;
    }
}

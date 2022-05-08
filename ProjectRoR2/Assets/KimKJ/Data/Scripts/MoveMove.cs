//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class MoveMove : MonoBehaviour
//{
  
//    Vector3 Dir = Vector3.zero;
//    Animator _anim = null;
//    protected Animator myAnim
//    {
//        get
//        {
//            if (_anim == null) _anim = this.GetComponentInChildren<Animator>();
//            return _anim;
//        }
//    }

//    TesterPlayer playertar = null;
//    protected TesterPlayer myPlayerTester
//    {
//        get
//        {
//            if (playertar == null)
//            {
//                playertar = this.GetComponentInChildren<TesterPlayer>(); //플레이어가 존재시 나중에 끼워맞출때 수정하면 될듯하다.
//            }
//            return playertar;
//        }
//    }

  

//    float RotSpeed = 360.0f;
//    public float MoveSpeed = 3.0f;
//    Coroutine moveRoutine = null;
//    Coroutine rotRoutine = null;

//    protected void StartRun(Vector3 pos, float Mms = 0.0f, float Mar = 0.0f)
//    {
//        if (rotRoutine != null) StopCoroutine(rotRoutine);
//        rotRoutine = StartCoroutine(Rotating(pos));
//     if (moveRoutine != null) StopCoroutine(moveRoutine);
//       moveRoutine = StartCoroutine(Moving(pos, Mms, Mar));
//    }

//    protected void AttackTarget(BattleSystem Target, float AttackRange, float AttackDelay, UnityAction EndAttack)
//    {
//        if (moveRoutine != null) StopCoroutine(moveRoutine);
//        moveRoutine = StartCoroutine(Attacking(Target, AttackRange, AttackDelay, EndAttack));
//       if (rotRoutine != null) StopCoroutine(rotRoutine);
//        rotRoutine = StartCoroutine(LookingAtTarget(Target));
//    }

//    protected void MoveToPosition(Vector3 pos, float Mms = 0.0f, float Mar = 0.0f)
//    {
//        if (moveRoutine != null) StopCoroutine(moveRoutine);
//        moveRoutine = StartCoroutine(Moving(pos, Mms, Mar));
//        if (rotRoutine != null) StopCoroutine(rotRoutine);
//        rotRoutine = StartCoroutine(Rotating(pos));
//    }

//    IEnumerator Moving(Vector3 pos, float Mms, float Mar)
//    {

//        float Dist = Vector3.Distance(pos, this.transform.position) - Mar;
//         Debug.Log("레무리안이 달린다!!!!");
//        while (!myAnim.GetBool("Running") && Dist > Mathf.Epsilon)

//        {
//            myAnim.SetTrigger("Run");
//            Dist = Vector3.Distance(pos, this.transform.position) - Mar;
//            yield return new WaitForSeconds(Mms);

//        }


    
//    }

//    IEnumerator Rotating(Vector3 pos)
//    {
//        Vector3 dir = (pos - this.transform.position).normalized;
//        float r = Mathf.Acos(Vector3.Dot(this.transform.forward, dir));
//        float Rot = 180.0f * (r / Mathf.PI);
//        float rotDir = 1.0f;
//        if (Vector3.Dot(this.transform.right, dir) < -Mathf.Epsilon)
//        {
//            rotDir = -1.0f;
//        }

//        while (Rot > Mathf.Epsilon)
//        {
//            float delta = 360.0f * Time.deltaTime;
//            if (Rot < delta)
//            {
//                delta = Rot;
//            }
//            this.transform.Rotate(Vector3.up * delta * rotDir, Space.World);
//            Rot -= delta;
//            yield return null;
//        }
//    }
//    IEnumerator LookingAtTarget(BattleSystem Target)
//    {
//        while (true)
//        {
//            Gameutil.CalAngle(myAnim.transform.forward, (Target.transform.position - this.transform.position).normalized, myAnim.transform.right, out ROTATEDATA data);
//            if (data.Angle > Mathf.Epsilon)
//            {
//                float delta = Time.deltaTime * RotSpeed;
//                delta = delta > data.Angle ? data.Angle : delta;
//                myAnim.transform.Rotate(Vector3.up * delta * data.Dir, Space.World);
//            }
//            yield return null;
//        }
//    }


//    IEnumerator Attacking(BattleSystem Target, float AttackRange, float AttackDelay, UnityAction EndAttack = null)
//    {
//        float playTime = AttackDelay;
//        while (true)
//        {
//            if (Target.IsLive() == false) break;
//            Vector3 dir = Target.transform.position - this.transform.position;
//         //   Debug.Log("플레이어 공격");
//            float dist = dir.magnitude;
//            if (dist > AttackRange)
//            {
//                myAnim.SetTrigger("Run");
//                dir.Normalize();

//                float delta = Time.deltaTime * MoveSpeed;
//                delta = delta > dist ? dist : delta;
//            //    this.transform.Translate(dir * delta, Space.World);
//            }
//            else
//            {
//               myAnim.SetBool("Running", false);
//                if (myAnim.GetBool("Attacking") == false)
//               {
//                   playTime += Time.deltaTime;
//                    //공격대기
//                    if (playTime >= AttackDelay)
//                   {
//                       //공격
//                       myAnim.SetTrigger("Attack");
//                       playTime = 0.0f;
//                   }
//                }
//           }
//            yield return null;
//        }
//    }
//}

 


   
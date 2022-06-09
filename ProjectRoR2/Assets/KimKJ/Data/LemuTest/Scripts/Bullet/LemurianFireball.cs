using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemurianFireball : MonoBehaviour
{
   public float damage;
    Rigidbody rigid;
    public float speed = 12.0f;
    LayerMask Layer;
    public GameObject lemu = null;
    Lemurian _lemuS = null;
    Lemurian mygolem
    {
        get
        {
            if (_lemuS == null)
            {
                _lemuS = lemu.GetComponent<Lemurian>();

            }
            return _lemuS;
        }
    }
    AIPerceptionlemu _aiperceptionlemu = null;
    AIPerceptionlemu myperceptionlemus //감지범위 
    {
        get
        {
            if (_aiperceptionlemu == null)
            {
                _aiperceptionlemu = GameObject.Find("AIPerception").GetComponent<AIPerceptionlemu>();

            }
            return _aiperceptionlemu;
        }
    }
    void Start()
    {
     //   LemuFireball.AP = 3.0f;
      
        //  damage = 3.0f;
    }


    void Update()
    {
 

    }

    IEnumerator Moving(Vector3 dir, float myBulletRange)
    {
        float dist = 0.0f;
        while (dist < myBulletRange)
        {
            float delta = speed * Time.deltaTime;
            dist += delta;
            this.transform.Translate(dir * delta, Space.World);
            yield return null;
        }
        Destroy(this.gameObject);
    }

    public void Shotting(Vector3 dir, float myBulletRange)
    {
        StartCoroutine(Moving(dir, myBulletRange));
    }



    void DestroyFireball()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
   if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
          other.gameObject.GetComponent<KJH_Player>()?.TakeDamage(damage); //코만도의 경우 여기서 데미지 주는걸로 설정 
   //       other.gameObject.GetComponent<Loader>()?.OnDamagekkj(damage); //로더의 경우 여기서 데미지 주는걸로 설정하면됨.
            Destroy(this.gameObject);
            Debug.Log("공격중입니다.");
        }
      
    }


}

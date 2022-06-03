using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemurianFireball : MonoBehaviour
{
   public float damage;
    Rigidbody rigid;
    public float speed = 2.0f;
    LayerMask Layer;

    void Start()
    {
     //   LemuFireball.AP = 3.0f;
        Invoke("DestroyFireball", 3);
        //  damage = 3.0f;
    }


    void Update()
    {
         Vector3 dir = transform.forward;
       transform.position += dir * speed * Time.deltaTime;

    }


    void DestroyFireball()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
   if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
     //     other.gameObject.GetComponent<KJH_Player>()?.TakeDamage(damage); //코만도의 경우 여기서 데미지 주는걸로 설정 
   //       other.gameObject.GetComponent<Loader>()?.OnDamagekkj(damage); //로더의 경우 여기서 데미지 주는걸로 설정하면됨.
            Destroy(this.gameObject);
            Debug.Log("공격중입니다.");
        }
      
    }


}

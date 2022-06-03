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
     //     other.gameObject.GetComponent<KJH_Player>()?.TakeDamage(damage); //�ڸ����� ��� ���⼭ ������ �ִ°ɷ� ���� 
   //       other.gameObject.GetComponent<Loader>()?.OnDamagekkj(damage); //�δ��� ��� ���⼭ ������ �ִ°ɷ� �����ϸ��.
            Destroy(this.gameObject);
            Debug.Log("�������Դϴ�.");
        }
      
    }


}

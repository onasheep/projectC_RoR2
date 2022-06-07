using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemFireball : MonoBehaviour
{

    public float damage;
    Rigidbody rigid;
    public float Speed = 4.0f;
    LayerMask Layer;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyGolemFireballkkj", 2);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.forward;
        transform.position += dir * Speed * Time.deltaTime;
    }

    void DestroyGolemFireballkkj()
    {
        Destroy(this.gameObject);
    }

  

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<KJH_Player>()?.TakeDamage(damage);
       //     other.gameObject.GetComponent<Loader>()?.OnDamagekkj(damage);
            Destroy(this.gameObject);
            Debug.Log("공격중입니다.");
        }
    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemFireball : MonoBehaviour
{
    public GameObject Golem = null;
    golem _golemS = null;
    golem mygolem
    {
        get
        {
            if (_golemS == null)
            {
                _golemS = Golem.GetComponent<golem>();

            }
            return _golemS;
        }
    }
    AIPerceptionGolem _aiperception = null;
    AIPerceptionGolem myperceptions
    {
        get
        {
            if (_aiperception == null)
            {
                _aiperception = Golem.GetComponentInChildren<AIPerceptionGolem>();

            }
            return _aiperception;
        }
    }
    public float damage;
    Rigidbody rigid;
    public float Speed = 4.0f;
    LayerMask Layer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Moving(Vector3 dir, float myBulletRange)
    {
        float dist = 0.0f;
        while (dist < myBulletRange)
        {
            float delta = Speed * Time.deltaTime;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed;
    public LayerMask CrushMask;
    Vector3 Checkdir = Vector3.zero;
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if ((CrushMask & 1 << collision.transform.gameObject.layer) != 0)
        {
            Destroy(gameObject);
        }
    }
    */
    private void Update()
    {
        
    }
    void CheckCrush(Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, 1f, CrushMask))
        {
            Debug.Log("Hit");
            Destroy(this.gameObject);
        }
    }
    IEnumerator Moving(Vector3 dir, float myBulletRange)
    {
        float dist = 0.0f;
        while (dist < myBulletRange)
        {
            float delta = BulletSpeed * Time.deltaTime;
            dist += delta;
            CheckCrush(dir);
            this.transform.Translate(dir * delta, Space.World);
            yield return null;
        }
        Destroy(this.gameObject);
    }

    public void Shotting(Vector3 dir, float myBulletRange)
    {
        StartCoroutine(Moving(dir, myBulletRange));        
    } 
}
/*
Ray ray = new Ray(bulletSpawnPoint.position, dir);
if (Physics.Raycast(ray, out RaycastHit hit, myBulletRange))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                Debug.Log("Monster");
            }
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                Debug.Log("Wall");
            }
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Debug.Log("Ground");
            }
        }   
 */
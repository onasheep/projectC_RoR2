using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed;
    bool Move = false;
    public LayerMask CrushMask;
    private void OnCollisionEnter(Collision collision)
    {
        if ((CrushMask & 1 << collision.transform.gameObject.layer) != 0)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Moving(Vector3 dir, float myBulletRange)
    {
        float dist = 0.0f;
        while (dist < myBulletRange)
        {
            float delta = BulletSpeed * Time.deltaTime;
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
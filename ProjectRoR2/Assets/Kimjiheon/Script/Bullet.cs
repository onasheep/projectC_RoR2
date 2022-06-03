using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed;
    public LayerMask CrushMask;
    Vector3 Checkdir = Vector3.zero;
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
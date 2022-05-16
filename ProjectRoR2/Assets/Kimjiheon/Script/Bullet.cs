using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public LayerMask CrushMask;
    private void OnCollisionEnter(Collision collision)
    {
        if ((CrushMask & 1 << collision.transform.gameObject.layer)!=0)
        {
            Destroy(gameObject);
        }
    }
}
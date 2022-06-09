using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootAnikkj : MoveMove
{
    Vector3 Dir2 = Vector3.zero;

    private void FixedUpdate()
    {
       this.transform.Translate(Dir2, Space.World);
        Dir2 = Vector3.zero;
    }

    private  void OnAnimatorMove()
    {
        Dir2 += myAnim.deltaPosition;
    }
}

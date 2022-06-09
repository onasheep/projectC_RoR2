using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HJSRootAnimation : HJSProperty
{
    Vector3 Dir = Vector3.zero;
    //float dist = 0.0f;
    private void FixedUpdate()
    {
        this.transform.parent.Translate(Dir, Space.World);
        //dist += Dir.magnitude;
        Dir = Vector3.zero;
    }
    private void OnAnimatorMove()
    {
        Dir += myAnim.deltaPosition;
    }

}

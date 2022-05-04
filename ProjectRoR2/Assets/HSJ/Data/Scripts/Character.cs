using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
   
    Animator _anim = null;
    protected Animator myAnim
    {
        get
        {
            if (_anim == null) _anim = this.GetComponent<Animator>();
            if (_anim == null) _anim = this.GetComponentInChildren<Animator>();
            return _anim;
        }
    }
    Rigidbody _Rigid = null;
    protected Rigidbody myRigid
    {
        get
        {
            if (_Rigid == null)
            {
                _Rigid = this.GetComponent<Rigidbody>();
            }
            return _Rigid;
        }
    }
  
}

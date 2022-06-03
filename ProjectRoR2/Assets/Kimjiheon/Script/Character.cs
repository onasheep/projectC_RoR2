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
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
                if (_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
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
                if (_Rigid == null)
                {
                    _Rigid = GetComponentInChildren<Rigidbody>();
                }
            }
            return _Rigid;
        }
    }

}

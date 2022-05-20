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
    CharacterController _CC = null;
    protected CharacterController myCc
    {
        get
        {
            if (_CC == null)
            {
                _CC = this.GetComponent<CharacterController>();
                if (_CC == null)
                {
                    _CC = GetComponentInChildren<CharacterController>();
                }
            }
            return _CC;
        }
    }
    CapsuleCollider _CapsuleCol = null;
    protected CapsuleCollider myCapsuleCol
    {
        get
        {
            if (_CapsuleCol == null)
            {
                _CapsuleCol = this.GetComponent<CapsuleCollider>();
                if (_CapsuleCol == null)
                {
                    _CapsuleCol = GetComponentInChildren<CapsuleCollider>();
                }
            }
            return _CapsuleCol;
        }
    }
    #region 스프링조인트
    SpringJoint _mySpring = null;
    protected SpringJoint _SJoint
    {
        get
        {
            if( _mySpring == null)
            {
                _mySpring = this.GetComponent<SpringJoint>();
                
            }
            return _mySpring;
        }
    }
    #endregion

}

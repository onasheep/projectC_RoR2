using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // 로더 스탯
    protected float playTime = 0.0f;
    protected float M1Cool = 0.5f;
    protected float M2Cool = 4.0f;
    protected float ShiftCool = 6.0f;
    protected float ChargingTime = 0.0f;
    protected float RCool = 4.0f;

    // 로더 쿨타임 체크용
    protected float M1checkT;
    protected float M2checkT;
    protected float ShiftcheckT;
    protected float RcheckT;

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
    #region 스프링조인트
    SpringJoint _mySpring = null;
    protected SpringJoint _SJoint
    {
        get
        {
            if (_mySpring == null)
            {
                _mySpring = this.GetComponent<SpringJoint>();

            }
            return _mySpring;
        }
    }
    #endregion
}

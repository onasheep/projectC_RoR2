using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class MonsterDB : MonoBehaviour
{
    public Bettle.MonsterData BettleData;
}*/

public class HJSProperty : MonoBehaviour
{
    Animator _anim = null;
    protected Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = this.GetComponentInChildren<Animator>();
            }
            return _anim;
        }
    }
    /*
    PlayerDR _playerDr = null;
    protected PlayerDR myPlayerDr
    {
        get
        {
            if (_playerDr == null)
            {
                _playerDr = this.GetComponentInChildren<PlayerDR>();
            }
            return _playerDr;
        }
    }
    */
    /*
    HJSBeetleBattleAnimEvents _animEvent = null;
    protected HJSBeetleBattleAnimEvents myAnimEvent
    {
        get
        {
            if (_animEvent == null)
            {
                _animEvent = this.GetComponentInChildren<HJSBeetleBattleAnimEvents>();
            }
            return _animEvent;
        }
    }
    */
    Rigidbody _rigidBody = null;
    protected Rigidbody myRigidBody
    {
        get
        {
            if (_rigidBody == null)
            {
                _rigidBody = this.GetComponent<Rigidbody>();
            }
            return _rigidBody;
        }
    }
}

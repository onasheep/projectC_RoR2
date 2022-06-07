using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour
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
    BattleAnimEvents _animEvent = null;
    protected BattleAnimEvents myAnimEvent
    {
        get
        {
            if (_animEvent == null)
            {
                _animEvent = this.GetComponentInChildren<BattleAnimEvents>();
            }
            return _animEvent;
        }
    }
}

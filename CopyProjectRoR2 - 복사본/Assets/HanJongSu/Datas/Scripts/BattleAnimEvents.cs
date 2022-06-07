using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleAnimEvents : MonoBehaviour
{
    public event UnityAction Attack = null;
    public void OnAttack()
    {
        Debug.Log("BattleEventsOnAttack");
        Attack?.Invoke();
    }
}

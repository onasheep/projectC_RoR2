using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HJSBeetleBattleAnimEvents : MonoBehaviour
{

    //public event UnityAction Attack = null;
    public Transform Head;
    /*public void OnAttack()
    {
        Debug.Log("BattleEventsOnAttack");
        Attack?.Invoke();
    }*/
    public void HeadButColider()
    {
        Collider[] Enemy = Physics.OverlapSphere(Head.position, 3.0f, 1 << LayerMask.NameToLayer("Player"));
        if (Enemy != null)
        {
            foreach (Collider enm in Enemy)
            {
                enm.GetComponent<KJH_Player>()?.TakeDamage(this.GetComponentInParent<Beetle>().BettleData.AD);
            }
        }

    }
}

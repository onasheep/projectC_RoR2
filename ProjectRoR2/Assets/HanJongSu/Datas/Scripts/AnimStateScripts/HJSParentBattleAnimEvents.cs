using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HJSParentBattleAnimEvents : MonoBehaviour
{

    //public event UnityAction Attack = null;
    public Transform Hand;
    /*public void OnAttack()
    {
        Debug.Log("BattleEventsOnAttack");
        Attack?.Invoke();
    }*/
    public void SlapColider()
    {
        Collider[] Enemy = Physics.OverlapSphere(Hand.position, 6.0f, 1 << LayerMask.NameToLayer("Player"));
        if (Enemy != null)
        {
            foreach (Collider enm in Enemy)
            {
                enm.GetComponent<KJH_Player>()?.TakeDamage(this.GetComponentInParent<Parent>().ParentData.AD);
            }
        }

    }
}

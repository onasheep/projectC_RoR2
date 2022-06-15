using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HJSParentBattleAnimEvents : MonoBehaviour
{
    public Transform Hand;
    public GameObject CrashGround;
    public AudioClip CrashSound;

    public void SlapColider()
    {
        Collider[] Enemy = Physics.OverlapSphere(Hand.position, 6.0f, 1 << LayerMask.NameToLayer("Player"));
        GetComponentInParent<Parent>().SoundPlay(CrashSound);
        Instantiate(CrashGround, Hand.position, Quaternion.identity);
        if (Enemy != null)
        {
            foreach (Collider enm in Enemy)
            {
                //enm.GetComponent<KJH_Player>()?.TakeDamage(this.GetComponentInParent<Parent>().ParentData.AD);
                if (DontDestroyobject.instance.CharSelected == 1)
                {
                    enm.GetComponent<KJH_Player>()?.TakeDamage(this.GetComponentInParent<Parent>().ParentData.AD);       
                }
                if (DontDestroyobject.instance.CharSelected == 2)
                {
                    //nm.GetComponent<Loader>()?.TakeDamage(this.GetComponentInParent<Beetle>().BettleData.AD);
                }
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HJSBeetleBattleAnimEvents : MonoBehaviour
{
    public Transform Head;
    public GameObject CrashEffect;
    public AudioClip CrashSound;

    public void HeadButColider()
    {
        Collider[] Enemy = Physics.OverlapSphere(Head.position, 3.0f, 1 << LayerMask.NameToLayer("Player"));
        if (Enemy != null)
        {
            foreach (Collider enm in Enemy)
            {
                //enm.GetComponent<KJH_Player>()?.TakeDamage(this.GetComponentInParent<Beetle>().BettleData.AD);
                if (DontDestroyobject.instance.CharSelected == 1)
                {
                    enm.GetComponent<KJH_Player>()?.TakeDamage(this.GetComponentInParent<Beetle>().BettleData.AD);
                    GetComponentInParent<Beetle>().SoundPlay(CrashSound);
                    Instantiate(CrashEffect, Head.position + new Vector3(0.0f, -1.0f, 0.0f), Quaternion.identity);
                }
                if (DontDestroyobject.instance.CharSelected == 2)
                {
                    //nm.GetComponent<Loader>()?.TakeDamage(this.GetComponentInParent<Beetle>().BettleData.AD);
                }
            }
        }

    }
}

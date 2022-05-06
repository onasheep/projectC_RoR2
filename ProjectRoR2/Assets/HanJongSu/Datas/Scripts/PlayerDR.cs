using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDR : MonoBehaviour
{
    public UnityAction FindTarget = null;
    public LayerMask myEnemyMask;
    public CombatSystem myTarget = null;
    public List<GameObject> DetectionPlayerList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if( (myEnemyMask & (1<< other.gameObject.layer) ) != 0)
        {            
            DetectionPlayerList.Add(other.gameObject);
            if(myTarget == null)
            {
                myTarget = other.gameObject.GetComponent<CombatSystem>();
                FindTarget?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //DetectionPlayerList.Remove(other.gameObject);
    }
}

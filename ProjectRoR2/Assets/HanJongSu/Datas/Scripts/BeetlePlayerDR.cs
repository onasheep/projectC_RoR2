using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeetlePlayerDR : MonoBehaviour
{
    public LayerMask myEnemyMask; 
    public List<GameObject> DetectionPlayerList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if( (myEnemyMask & (1 << other.gameObject.layer) ) != 0)
        {            
            DetectionPlayerList.Add(other.gameObject);
            if(this.transform.parent.GetComponent<Beetle>().myTarget == null)
            {
                this.transform.parent.GetComponent<Beetle>().myTarget = DetectionPlayerList[0].transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DetectionPlayerList.Remove(other.gameObject);
        if (this.transform.parent.GetComponent<Beetle>().myTarget == other.gameObject.transform)
        {
            if (DetectionPlayerList.Count == 0)
            {
                this.transform.parent.GetComponent<Beetle>().myTarget = null;
            }
            else
                this.transform.parent.GetComponent<Beetle>().myTarget = DetectionPlayerList[0].transform;
        }
    }
}

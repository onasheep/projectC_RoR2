using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{

    public MJ_Map1 Map1; 
    private void OnTriggerEnter(Collider other)
    {
        if(DontDestroyobject.instance.CharSelected == 1)
        {
            other.gameObject.GetComponent<KJH_Player>().transform.position = Map1.GetComponent<MJ_Map1>().StartPos;
        }

        if(DontDestroyobject.instance.CharSelected == 2)
        {
            other.gameObject.GetComponent<Loader>().transform.position = Map1.GetComponent<MJ_Map1>().StartPos;
            
        }
    }

}

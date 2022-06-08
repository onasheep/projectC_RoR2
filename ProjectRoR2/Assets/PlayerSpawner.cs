using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject commando = null;
    public GameObject loader= null;
    void Awake()
    {
        if(DontDestroyobject.instance.CharSelected == 1)
        {
            commando = Instantiate(Resources.Load("Prefeb/Commando")) as GameObject;
        }
        if (DontDestroyobject.instance.CharSelected == 2)
        {
            loader = Instantiate(Resources.Load("Prefabs/Loader/Loader")) as GameObject;
        }
        else return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

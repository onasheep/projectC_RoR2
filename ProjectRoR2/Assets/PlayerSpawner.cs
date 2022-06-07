using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    void Awake()
    {
        if(DontDestroyobject.instance.CharSelected == 1)
        {
            Instantiate(Resources.Load("Prefeb/Commando"));
        }
        if (DontDestroyobject.instance.CharSelected == 2)
        {
            Instantiate(Resources.Load("Prefabs/Loader/Loader"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

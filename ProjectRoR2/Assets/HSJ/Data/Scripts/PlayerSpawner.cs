using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject player;
    void Awake()
    {
        if(DontDestroyobject.instance.CharSelected == 1)
        {
            player = Instantiate(Resources.Load("Prefeb/Commando")) as GameObject;
            player.transform.GetComponentInChildren<AttackSystem>().SetPlayer(player);
        }
        if (DontDestroyobject.instance.CharSelected == 2)
        {
            player = Instantiate(Resources.Load("Prefabs/Loader/Loader")) as GameObject;
            player.transform.GetComponentInChildren<AttackSystem>().SetPlayer(player);
        }
        else return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

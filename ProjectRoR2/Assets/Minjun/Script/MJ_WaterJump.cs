using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJ_WaterJump : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.Find("mdlCommandoDualies (merge)");
        if (player == null)
        {
            player = GameObject.Find("mdlLoader (merge)");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        player.GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
    }
}

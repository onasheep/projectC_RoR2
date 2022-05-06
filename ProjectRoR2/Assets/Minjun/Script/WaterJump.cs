using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJump : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject waterJumpEff = Instantiate(Resources.Load("Effect/SmallSplash"), this.transform.position, Quaternion.identity) as GameObject;
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

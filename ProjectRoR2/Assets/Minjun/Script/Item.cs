using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{ 
    public Transform Player;
    public LayerMask myPlayer;
    public Transform myRoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (myRoot.transform.parent != Player.transform)
        {
            this.transform.Rotate(Vector3.up * 180.0f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((myPlayer & (1 << other.gameObject.layer)) != 0)
        {
            myRoot.transform.SetParent(Player.transform);
            myRoot.transform.localScale -= new Vector3(0.9f, 0.9f, 0.9f);
            myRoot.transform.localPosition = new Vector3(0.0f, 0.01f, -0.002f);
        }
    }
}

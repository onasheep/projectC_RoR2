using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSJItem : MonoBehaviour
{
    public HsjInventory inventory;
    public GameObject itemobject;

    private void Awake()
    {
        inventory = GameObject.FindObjectOfType<HsjInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           
                    Debug.Log("1");
                    
                    
                    Destroy(gameObject);
                 
        }
        

    }
}

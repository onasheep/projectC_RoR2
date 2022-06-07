using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSJItemSlot : MonoBehaviour
{
    public int i;
    private HsjInventory inventory;

    private void Awake()
    {
        inventory = GameObject.FindObjectOfType<HsjInventory>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}

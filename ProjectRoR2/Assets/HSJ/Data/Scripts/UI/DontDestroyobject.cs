using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyobject : MonoBehaviour
{
    public static DontDestroyobject instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance == this)
            {
                Destroy(gameObject);
            }
        }
    }

        public int CharSelected = 0;
    
}


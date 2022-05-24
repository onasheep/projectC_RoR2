using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComboEvent : MonoBehaviour
{
    public event UnityAction<bool> ComboCheck = null;
   

    public void ComboCheckStart()
    {
        ComboCheck?.Invoke(true);
    }

    public void ComboCheckEnd()
    {
        ComboCheck?.Invoke(false);
    }
    
}

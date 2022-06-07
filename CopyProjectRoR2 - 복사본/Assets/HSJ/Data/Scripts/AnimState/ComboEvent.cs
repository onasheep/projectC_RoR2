using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComboEvent : MonoBehaviour
{
    public event UnityAction<bool> ComboCheck = null;
    public GameObject Effect;
    public Transform RArm;
    public Transform LArm;
    public SoundManager mySpeaker;



    public void ComboCheckStart()
    {
        ComboCheck?.Invoke(true);
    }

    public void ComboCheckEnd()
    {
        ComboCheck?.Invoke(false);
    }
    
    public void LArmeffect()
    {
        Instantiate(Effect,LArm);
    }
    public void RArmeffect()
    {
        Instantiate(Effect, RArm);

    }
    public void REffectSound()
    {
        mySpeaker.PlaySound("R");
        mySpeaker.GetComponent<AudioSource>().Play();
    }

}

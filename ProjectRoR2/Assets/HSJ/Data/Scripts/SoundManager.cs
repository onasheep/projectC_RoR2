using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SoundManagerMachine = null;

    public AudioClip audioLMB;
    public AudioClip audioRMB;
    public AudioClip audioShift;
    public AudioClip audioR;
    AudioSource audioSource;

    private void Awake()
    {
        SoundManagerMachine = this;
        this.audioSource = GetComponent<AudioSource>();
    }
    
    public void PlaySound(string action)
    {
        switch (action)
        {
            case "LMB":
                audioSource.volume = 0.1f;
                audioSource.clip = audioLMB;    
                break;
            case "RMB":
                audioSource.volume = 0.1f;
                audioSource.clip = audioRMB;
                break;
            case "Shift":
                audioSource.volume = 0.05f;
                audioSource.clip = audioShift;
                break;
            case "R":
                audioSource.volume = 0.05f;
                audioSource.clip = audioR;      
                break;
        }
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

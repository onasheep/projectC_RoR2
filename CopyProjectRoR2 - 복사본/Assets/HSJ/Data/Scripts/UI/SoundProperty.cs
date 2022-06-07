using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundProperty : MonoBehaviour
{
    AudioSource _source = null;
    protected AudioSource MySpeaker
    {
        get
        {
            if (_source == null)
            {
                _source = this.GetComponent<AudioSource>();
                Sound.Ins.AddEffectSources(_source);
            }
            return _source;
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    const string EffectVolumeKey = "EffectVolume";
    const string BGMVolumeKey = "BGMVolumeKey";
    static Sound instance = null;

    public static Sound Ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(Sound)) as Sound;
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "SoundManager";
                    instance = obj.AddComponent<Sound>();

                    //instance.EffectVolume = 1.0f - PlayerPrefs.GetFloat(EffectVolumeKey);
                    //instance.BGmVolume = 1.0f - PlayerPrefs.GetFloat(BGMVolumeKey);

                    DontDestroyOnLoad(obj);
                }
            }
            return instance;

        }
    }

    List<AudioSource> EffectSources = new List<AudioSource>();
    AudioSource _bgmSource = null;

    AudioSource BGMSource
    {
        get
        {
            if (_bgmSource == null)
            {
                _bgmSource = Camera.main.GetComponent<AudioSource>();
            }
            return _bgmSource;
        }
    }

    float _effectVolume = 1.0f;   // 1.0f 이 가장 큰소리 0.0이 가장 작은 소리
    float _bgmVolume = 1.0f;
    public bool IsPauseBGM = false;

    public float EffectVolume
    {
        get
        {
            return _effectVolume;
        }
        set
        {
            _effectVolume = Mathf.Clamp(_effectVolume, 0.0f, 1.0f);
            PlayerPrefs.SetFloat(EffectVolumeKey, 1.0f - _effectVolume);
            //_effectVolume = value > 1.0f ? 1.0f : value < 0.0f ? 0.0f: value; // 3항식 확장 
            SetEffectVolume(_effectVolume);
        }
    }

    public float BGmVolume
    {
        get
        {
            return _bgmVolume;
        }
        set
        {
            PlayerPrefs.SetFloat(BGMVolumeKey, 1.0f - _effectVolume);  // 레지스트리에 값을 저장

            _bgmVolume = Mathf.Clamp(_bgmVolume, 0.0f, 1.0f);

            //_bgmVolume = value > 1.0f ? 1.0f : value < 0.0f ? 0.0f : value;
            BGMSource.volume = _bgmVolume;
        }
    }




    public void AddEffectSources(AudioSource source)
    {
        EffectSources.Add(source);
    }

    void SetEffectVolume(float volume)
    {
        foreach (AudioSource source in EffectSources)
        {
            source.volume = volume;
        }
    }

    public void PlayBGM(AudioClip bgm = null, bool loop = true)
    {
        if (IsPauseBGM)
        {
            IsPauseBGM = false;
            BGMSource.Play();
            return;

        }
        BGMSource.clip = bgm;
        BGMSource.loop = loop;
        BGMSource.Play();
    }

    public void PauseBGM()
    {
        IsPauseBGM = true;
        BGMSource.Pause();
    }
}

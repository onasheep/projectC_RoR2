using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyInputControl : MonoBehaviour
{
    [SerializeField] Image LM2Img;
    [SerializeField] Image LShiftImg;
    [SerializeField] Image LRShiftImg;

    
    public void LM2CoolTime(float cooltime)
    {
        StartCoroutine(CoolTime(LM2Img, cooltime));
    }

    public void LShiftCoolTime(float cooltime)
    {
        StartCoroutine(CoolTime(LShiftImg, cooltime));
    }
    public void LRCoolTime(float cooltime)
    {
        StartCoroutine(CoolTime(LRShiftImg, cooltime));
    }

    IEnumerator CoolTime(Image _image, float t)
    {
        _image.fillAmount = 0.0f;
        float speed = 1.0f / t;
        while (_image.fillAmount < 1.0f)
        {
            _image.fillAmount += Time.deltaTime * speed;
            yield return null;
        }
    }
}

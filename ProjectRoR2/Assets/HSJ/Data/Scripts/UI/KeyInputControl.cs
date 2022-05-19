using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyInputControl : MonoBehaviour
{
    [SerializeField] Image LM2Img;
    [SerializeField] Image LShiftImg;
    [SerializeField] Image LRShiftImg;

    
    public void LM2CoolTime()
    {
        StartCoroutine(CoolTime(LM2Img, 6.0f));
    }

    public void LShiftCoolTime()
    {
        StartCoroutine(CoolTime(LShiftImg, 6.0f));
    }
    public void LRCoolTime()
    {
        StartCoroutine(CoolTime(LRShiftImg, 6.0f));
    }
    
    IEnumerator CoolTime(Image skillimg,float cool)
    {
        while(cool > 0.0f)
        {
            cool -= Time.deltaTime;
            skillimg.fillAmount = (1.0f/cool);
            yield return new WaitForSeconds(cool);
        }
    }
}

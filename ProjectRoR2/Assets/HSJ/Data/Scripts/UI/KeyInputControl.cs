using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyInputControl : MonoBehaviour
{
    public static KeyInputControl KeyInputMachine = null;
    [SerializeField] Image LM1Img = null;
    [SerializeField] Image LM2Img = null;
    [SerializeField] Image LShiftImg = null;
    [SerializeField] Image LRShiftImg = null;    

    private void Awake()
    {
        if (DontDestroyobject.instance.CharSelected == 1)
        {
            LM1Img.sprite = Resources.Load<Sprite>("Prefabs/SkillImage/CSkill/LMBIcon");
            LM2Img.sprite = Resources.Load<Sprite>("Prefabs/SkillImage/CSkill/RLBIcon");
            LShiftImg.sprite = Resources.Load<Sprite>("Prefabs/SkillImage/CSkill/ShiftIcon");
            LRShiftImg.sprite = Resources.Load<Sprite>("Prefabs/SkillImage/CSkill/RIcon");
        }
        else if ( DontDestroyobject.instance.CharSelected == 2)
        {
            LM2Img.sprite = Resources.Load<Sprite>("Prefabs/SkillImage/LSkill/LMBIcon");
            LM2Img.sprite = Resources.Load<Sprite>("Prefabs/SkillImage/LSkill/RMBIcon");
            LShiftImg.sprite = Resources.Load<Sprite>("Prefabs/SkillImage/LSkill/ShiftIcon");
            LRShiftImg.sprite = Resources.Load<Sprite>("Prefabs/SkillImage/LSkill/RIcon");
        }
        KeyInputMachine = this;
    }
    public void M2CoolTime(float cooltime)
    {
        StartCoroutine(CoolTime(LM2Img, cooltime));
    }

    public void ShiftCoolTime(float cooltime)
    {
        StartCoroutine(CoolTime(LShiftImg, cooltime));
    }
    public void RCoolTime(float cooltime)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSkillPanel : MonoBehaviour
{
   
    public TMPro.TMP_Text Passive;
    public TMPro.TMP_Text LMB;
    public TMPro.TMP_Text RMB;
    public TMPro.TMP_Text Shift;
    public TMPro.TMP_Text R;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CommandoSkillText();
    }
    
    public  void CommandoSkillText()
    
    {
        Passive.text = "";
        LMB.text = "<color=#FAA325>2연사</color>\n적 하나를 빠르게 쏘아 <color=#FDC804>100 % </color>의 피해를 입힙니다.";
        RMB.text = "<color=#FAA325>위상조정탄</color>\n<color=#FDC804>관통력이 있는</color> 총알을 발사하여 <color=#FDC804>300 % 피해</color>를 입힙니다.적을 관통할 때마다 피해가 <color=#FDC804>40 %</color> 증가합니다.";
        Shift.text = "<color=#FAA325>전술 구르기</color>\n짧은 거리를 <color=#6EAAFB>몸을 굴려</color> 이동합니다.";
        R.text = "<color=#FAA325>제압 사격\n</color><color=#FDC804>충격</color>.연속 사격하여 탄환당 <color=#FDC804>100 % 피해</color>를 줍니다.공격 속도에 따라 발사 횟수가 증가합니다.";
    
    }
   
}

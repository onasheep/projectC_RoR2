using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LSkillPanel : MonoBehaviour
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
        LoaderSkillText();
    }

    public void LoaderSkillText()
    {
        Passive.text = "<color=#4751EF>고철 장벽</color>\n로더는 모든 낙하 피해에 <color=#6EAAFB>면역</color>입니다. 로더의 건틀릿으로 적을 공격하면 <color=#6EAAFB>임시 장벽</color>을 얻습니다.";
        LMB.text = "<color=#4751EF>너클붐</color>\n근처의 적들에게 휘둘러서 <color=#FDC804>320%의 피해</color>를 입힙니다. ";
        RMB.text = "<color=#4751EF>갈고리 주먹</color>\n건틀릿을 앞으로 발사해서 대상을 향해 <color=#6EAAFB>이동</color>합니다.";
        Shift.text = "<color=#4751EF>충전 건틀릿</color>\n<color=#6EAAFB>묵직</color>. 날카로운 주먹을 충전하여 <color=#FDC804>600%-2700% 피해</color>를 줍니다.";
        R.text = "<color=#4751EF>썬더슬램</color>\n</color><color=#FDC804>마비</color>.주먹으로 내려쳐, 충격 시 <color=#FDC804>2000% 피해</color>를 입힙니다.";
    }
}

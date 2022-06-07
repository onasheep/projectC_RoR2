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
        Passive.text = "<color=#4751EF>��ö �庮</color>\n�δ��� ��� ���� ���ؿ� <color=#6EAAFB>�鿪</color>�Դϴ�. �δ��� ��Ʋ������ ���� �����ϸ� <color=#6EAAFB>�ӽ� �庮</color>�� ����ϴ�.";
        LMB.text = "<color=#4751EF>��Ŭ��</color>\n��ó�� ���鿡�� �ֵѷ��� <color=#FDC804>320%�� ����</color>�� �����ϴ�. ";
        RMB.text = "<color=#4751EF>���� �ָ�</color>\n��Ʋ���� ������ �߻��ؼ� ����� ���� <color=#6EAAFB>�̵�</color>�մϴ�.";
        Shift.text = "<color=#4751EF>���� ��Ʋ��</color>\n<color=#6EAAFB>����</color>. ��ī�ο� �ָ��� �����Ͽ� <color=#FDC804>600%-2700% ����</color>�� �ݴϴ�.";
        R.text = "<color=#4751EF>�������</color>\n</color><color=#FDC804>����</color>.�ָ����� ������, ��� �� <color=#FDC804>2000% ����</color>�� �����ϴ�.";
    }
}

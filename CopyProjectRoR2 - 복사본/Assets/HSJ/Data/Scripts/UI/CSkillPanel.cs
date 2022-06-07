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
        LMB.text = "<color=#FAA325>2����</color>\n�� �ϳ��� ������ ��� <color=#FDC804>100 % </color>�� ���ظ� �����ϴ�.";
        RMB.text = "<color=#FAA325>��������ź</color>\n<color=#FDC804>������� �ִ�</color> �Ѿ��� �߻��Ͽ� <color=#FDC804>300 % ����</color>�� �����ϴ�.���� ������ ������ ���ذ� <color=#FDC804>40 %</color> �����մϴ�.";
        Shift.text = "<color=#FAA325>���� ������</color>\nª�� �Ÿ��� <color=#6EAAFB>���� ����</color> �̵��մϴ�.";
        R.text = "<color=#FAA325>���� ���\n</color><color=#FDC804>���</color>.���� ����Ͽ� źȯ�� <color=#FDC804>100 % ����</color>�� �ݴϴ�.���� �ӵ��� ���� �߻� Ƚ���� �����մϴ�.";
    
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class KJH_PlayerHUD : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private KJH_CharacterStat myStat; //�÷��̾� ü��
    [SerializeField]
    private AttackSystem myAttackSys; //�÷��̾� ü���̺�Ʈ
    [Header("HP/Damage UI")]
    [SerializeField]
    private TextMeshProUGUI myHPtext; //�÷��̾� ü�� ���
    [SerializeField]
    private Image damageScreen;
    [SerializeField]
    private AnimationCurve curvedamageScreen;

    private void Awake()
    {
        myAttackSys.onHPEvent.AddListener(UpdateHPHUD);
    }

    private void UpdateHPHUD(float previous, float current)
    {
        myHPtext.text = "HP" + current;

        if (previous - current > 0)
        {
            StopCoroutine("OnDamageScreen");
            StartCoroutine("OnDamageScreen");
        }
    }

    private IEnumerator OnDamageScreen()
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime;
            Color color = damageScreen.color;
            color.a = Mathf.Lerp(1, 0, curvedamageScreen.Evaluate(percent));
            damageScreen.color = color;
            yield return null;
        }
    }
}

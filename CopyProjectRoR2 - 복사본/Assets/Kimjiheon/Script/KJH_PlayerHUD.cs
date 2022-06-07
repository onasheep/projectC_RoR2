using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class KJH_PlayerHUD : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private KJH_CharacterStat myStat; //플레이어 체력
    [SerializeField]
    private AttackSystem myAttackSys; //플레이어 체력이벤트
    [Header("HP/Damage UI")]
    [SerializeField]
    private TextMeshProUGUI myHPtext; //플레이어 체력 출력
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

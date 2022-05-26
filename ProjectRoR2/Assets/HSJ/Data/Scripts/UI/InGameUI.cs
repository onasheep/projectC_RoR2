using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    Vector3 ScreenCenter;
    public Transform Canvas;
    public Camera Aimcamera;
    // timer
    public TMPro.TMP_Text TimeText;
    float time;

    // hpbar
    public KJH_CharacterStat myStat;
    [SerializeField] private Slider myHpbar;
    float maxHp = 100;  // �ִ�ü�°� ����ü���� ���߿� ���ȿ��� �������� ��ü�� �� ���� 
    float curHP = 100; // �۵������� ����� �а� 
    public TMPro.TMP_Text HpbarText;

    // expbar
    [SerializeField] private Slider myExpbar;
    float maxExp = 100;
    float curExp = 0.0f;
    public TMPro.TMP_Text LevelText;
    int Level = 1;

    // Gold
    public TMPro.TMP_Text GoldText;
    int Gold = 0;

    // Start is called before the first frame update
    void Start()
    {
        myHpbar.value = (float)curHP / (float)maxHp;
        myExpbar.value = (float)curExp / (float)maxExp;
        LevelText.text = "���� : " + Level;

        MakeAim();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        HpBarController();
        ExpBarController();
        GainGold();
    }
    public void MakeAim()
    {
        ScreenCenter = new Vector3(Aimcamera.pixelWidth / 2, Aimcamera.pixelHeight / 2);
        Ray ray = Aimcamera.ScreenPointToRay(ScreenCenter);
        GameObject Crosshair = Instantiate(Resources.Load("Prefabs/CrossHair"), Canvas) as GameObject;
    }

    private void Timer()
    {
        time += Time.deltaTime;
        TimeText.text = string.Format("{0:N1}",time);
    }

    // �� �Ʒ���
    // ������ �߰��Ǹ� ���� �� ��
    private void HpBarController() // damage�� ������ �޾ƿͼ� ��� 
    {
        if(Input.GetKeyDown(KeyCode.M)) // ���� ���߿� �ٲ�
        {
            if(curHP > 0)
            {
                curHP -= 10; // ���߿� �������� �ٲ� 

            }
            else
            {
                curHP = 0;
            }
        }
        myHpbar.value = Mathf.Lerp(myHpbar.value, (float)curHP / (float)maxHp, Time.deltaTime * 10.0f); // hp ���� Lerp�� �ε巴�� 
        HpbarText.text = curHP + "/" + maxHp; // hpbar �ؽ�Ʈ ���� 
    }

    private void ExpBarController()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            if(curExp < maxExp) // ������������ 
            {
                curExp += 10.0f;
            }
            if(curExp == maxExp)
            {
                Level++;
                LevelText.text = "���� : " + Level; // ���������� ������ ��ġ �ݿ� 
                maxExp += 10.0f; // �������ϸ� �䱸 ����ġ ��� ���� ��ġ 10.0f;
                curExp = 0.0f;
            }      
        }
        myExpbar.value = Mathf.Lerp(myExpbar.value, (float)curExp / (float)maxExp, Time.deltaTime * 10.0f);
    }

    private void GainGold()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Gold += 10;

        }
        //if(Gold < Price)
        //{

        //}
        //if( �������� ��� ) 
        //{
        //    Gold -= Price;
        //}
        GoldText.text = Gold.ToString();
    }
}

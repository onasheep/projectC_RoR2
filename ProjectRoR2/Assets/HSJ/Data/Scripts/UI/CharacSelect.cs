using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;



public class CharacSelect : SoundProperty
{
    // ĳ���� ���� �Ҹ�
    public AudioClip CSound;
    public AudioClip LSound;
    
    // ���±�� 
    public enum STATE
    {
        LOADER, COMMANDO, CLICK, NONE
    }
    // �ڽ��� �ڽ����� ���̾ �ΰ� 
    public STATE myChar = STATE.NONE;    
    public GameObject C;
    public GameObject L;
    public Button CB;
    public Button LB;
    public TMPro.TMP_Text CharName;
    public Button Ability;
    public Button Skill;
    public GameObject CSkill;
    public GameObject LSkill;
    public TMPro.TMP_Text Abilitytxt;    
    void Start()
    {
        ChangeState(STATE.COMMANDO);
        Sound.Ins.AddEffectSources(MySpeaker);



    }

    // Update is called once per frame
    void Update()
    {
        StatProceses();
        
    }

    public void CommandoSel()
    {
        if (myChar == STATE.COMMANDO)
        {
            
            ChangeState(STATE.CLICK);
        }
        if (!(myChar == STATE.COMMANDO))
        {
            ChangeState(STATE.COMMANDO);
        }
    }
   
    public void LoaderSel()
    {
        if (myChar == STATE.LOADER)
        {
            ChangeState(STATE.CLICK);
        }
        if (!(myChar == STATE.LOADER))
        {
            ChangeState(STATE.LOADER);
        }
    }
  
    void ChangeState(STATE c)
    {
        if (myChar == c) return;
        myChar = c;
        switch (myChar)
        {
            case STATE.COMMANDO:
                //MySpeaker.PlayOneShot(CSound);
                // �ڸ��� �𵨸� ��Ƽ��,  �δ� �𵨸� ��Ƽ��
                //MySpeaker.
                C.SetActive(true);
                L.SetActive(false);
                // �̸� ����
                CharName.text = "�ڸ���";
                // ���� �ؽ�Ʈ ��ü
                Abilitytxt.text = "�ڸ����� � ��Ȳ������ ���� �� �ִ� ���� ĳ�����Դϴ�.\n\n"+
                "<!> 2����� �ܰŸ������� ��Ÿ������� �����ϸ�, ��� �ӵ��� ������ �پ��� ������ ȿ���� �ߵ��� �� �ֽ��ϴ�.\n\n"+
                "<!> ���� �����⸦ ȿ�������� ����ϸ� ����� ������ ���� �� �ֽ��ϴ�\n\n" +
                "<!> ���� ����� �ϳ��� ������ �ݺ��Ͽ� ���� �ɰų� ���� ���� �����Ű�� �� ����� �� �ֽ��ϴ�.\n\n" +
                "<!> ��������ź�� ���� ����� �� �ֽ��ϴ�. �� �̿��ϼ���!\n\n";
                //  �ڸ��� ���¿��� �δ� ��ųâ�� ���������� �ڸ��� ��ųâ���� ��ü
                if (LSkill.activeInHierarchy)
                {
                    CSkill.SetActive(true);
                    LSkill.SetActive(false);
                }
                // ĳ���� Ŭ���� ����, ��ųâ �� ����
                Ability.gameObject.GetComponent<Image>().color = new Color32(250, 163, 37, 246);
                Skill.gameObject.GetComponent<Image>().color = new Color32(250, 163, 37, 246);
                ChangeState(STATE.CLICK);
                break;
            case STATE.LOADER:
                //MySpeaker.PlayOneShot(LSound);

                // �δ� �𵨸� ��Ƽ��, �ڸ��� �𵨸� ��Ƽ��
                L.SetActive(true);
                C.SetActive(false);
                // �̸� ����
                CharName.text = "�δ�";
                // ���� �ؽ�Ʈ ��ü
                Abilitytxt.text = "�δ��� ������ ����ؼ� ������ ������� ȯ���� Ž���� �� �ִ� �������� ������ �����Դϴ�.\n\n" +
                    "<!> ��ö �庮�� ����ϸ� ���� ���¸� ������ �� �ֽ��ϴ�.�ָ����� ������ �ʴ´ٸ��.\n\n" +
                    "<!> ���� �ָ��� ����ϸ� ������ �̵��ϰų�, ���� ������ ���ϰų�, ��û�� ���� ��Ʋ�� ���ݷ� ��ȭ�� ���� �ӵ��� �����մϴ�!\n\n" +
                    "<!> ���� ��Ʋ���� �Ÿ��� �����ų� ��ó�� ���� �������ϱ� ���� ����� �� �ֽ��ϴ�.\n\n";
               //  �δ� ���¿��� �ڸ��� ��ųâ�� ���������� �δ� ��ųâ���� ��ü
                if (CSkill.activeInHierarchy)
                {
                    LSkill.SetActive(true);
                    CSkill.SetActive(false);
                }
                // ĳ���� Ŭ���� ����, ��ųâ �� ����
                Ability.gameObject.GetComponent<Image>().color = new Color32(71, 81, 239, 255);
                Skill.gameObject.GetComponent<Image>().color = new Color32(71, 81, 239, 255);

                ChangeState(STATE.CLICK);
                break;
            case STATE.CLICK:
                break;
            case STATE.NONE:            
                break;
                
        }
    }
    void StatProceses()
    {
        switch (myChar)
        {
            case STATE.COMMANDO:
                
                break;
            case STATE.LOADER:
                break;
            case STATE.CLICK:
                // Ŭ�����¿��� ĳ���� ������ Ŭ���� ���º��� 
                CB.onClick.AddListener(CommandoSel);                
                LB.onClick.AddListener(LoaderSel);               
                break;
            case STATE.NONE:
                break;
        }
    }
    
    
   
}

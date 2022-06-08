using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;



public class CharacSelect : SoundProperty
{
    // 캐릭터 등장 소리
    public AudioClip CSound;
    public AudioClip LSound;
    
    // 상태기계 
    public enum STATE
    {
        LOADER, COMMANDO, CLICK, NONE
    }
    // 자식의 자식으로 레이어를 두고 
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
        Cursor.visible = true;


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
                // 코만도 모델링 엑티브,  로더 모델링 디엑티브
                //MySpeaker.
                C.SetActive(true);
                L.SetActive(false);
                // 이름 변경
                CharName.text = "코만도";
                // 개요 텍스트 교체
                Abilitytxt.text = "코만도는 어떤 상황에서나 믿을 수 있는 만능 캐릭터입니다.\n\n"+
                "<!> 2연사는 단거리에서나 장거리에서나 강력하며, 사격 속도가 빠르고 다양한 아이템 효과를 발동할 수 있습니다.\n\n"+
                "<!> 전술 구르기를 효율적으로 사용하면 예고된 공격을 피할 수 있습니다\n\n" +
                "<!> 제압 사격은 하나의 적에게 반복하여 마비를 걸거나 여러 적을 마비시키는 데 사용할 수 있습니다.\n\n" +
                "<!> 위상조정탄은 벽을 통과할 수 있습니다. 잘 이용하세요!\n\n";
                //  코만도 상태에서 로더 스킬창이 켜져있으면 코만도 스킬창으로 교체
                if (LSkill.activeInHierarchy)
                {
                    CSkill.SetActive(true);
                    LSkill.SetActive(false);
                }
                // 캐릭터 클릭시 개요, 스킬창 색 변경
                Ability.gameObject.GetComponent<Image>().color = new Color32(250, 163, 37, 246);
                Skill.gameObject.GetComponent<Image>().color = new Color32(250, 163, 37, 246);
                ChangeState(STATE.CLICK);
                DontDestroyobject.instance.CharSelected = 1;
                break;
            case STATE.LOADER:
                //MySpeaker.PlayOneShot(LSound);

                // 로더 모델링 엑티브, 코만도 모델링 디엑티브
                L.SetActive(true);
                C.SetActive(false);
                // 이름 변경
                CharName.text = "로더";
                // 개요 텍스트 교체
                Abilitytxt.text = "로더는 갈고리를 사용해서 고유한 방법으로 환경을 탐색할 수 있는 느리지만 강력한 전사입니다.\n\n" +
                    "<!> 고철 장벽을 사용하면 전투 상태를 유지할 수 있습니다.주먹질을 멈추지 않는다면요.\n\n" +
                    "<!> 갈고리 주먹을 사용하면 빠르게 이동하거나, 적의 공격을 피하거나, 엄청난 충전 건틀릿 공격력 강화를 통해 속도가 증가합니다!\n\n" +
                    "<!> 충전 건틀릿은 거리를 좁히거나 근처의 적을 마무리하기 위해 사용할 수 있습니다.\n\n";
               //  로더 상태에서 코만도 스킬창이 켜져있으면 로더 스킬창으로 교체
                if (CSkill.activeInHierarchy)
                {
                    LSkill.SetActive(true);
                    CSkill.SetActive(false);
                }
                // 캐릭터 클릭시 개요, 스킬창 색 변경
                Ability.gameObject.GetComponent<Image>().color = new Color32(71, 81, 239, 255);
                Skill.gameObject.GetComponent<Image>().color = new Color32(71, 81, 239, 255);

                ChangeState(STATE.CLICK);
                DontDestroyobject.instance.CharSelected = 2;

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
                // 클릭상태에서 캐릭터 아이콘 클릭시 상태변경 
                CB.onClick.AddListener(CommandoSel);                
                LB.onClick.AddListener(LoaderSel);               
                break;
            case STATE.NONE:
                break;
        }
    }
    
    
   
}

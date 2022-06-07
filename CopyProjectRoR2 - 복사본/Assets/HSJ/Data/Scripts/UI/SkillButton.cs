using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject Abilitytxt;
    public GameObject CSkillPanel;
    public GameObject LSkillPanel;
    public GameObject LChar;
    public GameObject CChar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!LChar.activeInHierarchy && CChar.activeInHierarchy)
        {
            CSkillPanel.SetActive(true);
            LSkillPanel.SetActive(false);
            Abilitytxt.SetActive(false);
        }

        if (!CChar.activeInHierarchy && LChar.activeInHierarchy)
        {
            LSkillPanel.SetActive(true);
            CSkillPanel.SetActive(false);
            Abilitytxt.SetActive(false);
        }
        

    }


}

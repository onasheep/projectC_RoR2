using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject Abilitytxt;
    public GameObject CSkillPanel;
    public GameObject LSkillPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Abilitytxt.SetActive(true);
        CSkillPanel.SetActive(false);
        LSkillPanel.SetActive(false);
    }

}

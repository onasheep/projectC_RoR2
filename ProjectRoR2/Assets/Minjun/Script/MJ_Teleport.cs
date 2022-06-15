using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MJ_Teleport : MonoBehaviour
{
    public float Charge = 0;
    bool engage = false;
    bool _Charging = false;
    BoxCollider BoxCol;
    SphereCollider SpherCol;
    GameObject Range;
    GameObject RedF;

    public GameObject Boss;
    public GameObject obj;
    int Boss_Cnt = 0;
    bool Boss_Die = false;

    public TMPro.TMP_Text ChargeUI;

    public GameOverCanvas myGameOver = null;

    public Parent.STATE state;

    // Start is called before the first frame update
    void Start()
    {       
        Range = GameObject.Find("TelRange");
        BoxCol = GetComponent<BoxCollider>();
        SpherCol = GetComponent<SphereCollider>();
        SpherCol.enabled = false;
        Range.SetActive(false);
        RedF = Instantiate(Resources.Load("Effect/Red_Floating"), this.transform.position, Quaternion.identity,this.transform) as GameObject;

        myGameOver = GameOverCanvas.GameOverMachine;

    }

    // Update is called once per frame
    void Update()
    {
        Charging();
        if (Boss_Cnt == 1 && !Boss_Die)
        {
            state = obj.GetComponent<Parent>().myState;
            if (state == Parent.STATE.DIE)
            {
                Boss_Die = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) engage = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) engage = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) engage = false;
    }

   
    void Charging()
    {     
        if (engage)
        {
            if (Input.GetKey(KeyCode.F))
            {
                RedF.SetActive(false);
                _Charging = true;
                BoxCol.enabled = false;
                SpherCol.enabled = true;
                Range.SetActive(true);
                if (Boss_Cnt == 0)
                {
                    obj = Instantiate(Boss,this.transform.position, Quaternion.identity);
                                      
                    Boss_Cnt++;
                }
            }
            if(_Charging)
            {
                if(Charge!=100) Charge += Time.deltaTime;
                ChargeUI.text = "<color=#ff0000>" + Charge.ToString("F0")  + "%" + "</color>";
                
                if (Charge >= 100 )
                {
                    BoxCol.enabled = true;
                    SpherCol.enabled = false;
                    Range.SetActive(false);
                    Charge = 100;
                    if (engage && Input.GetKeyDown(KeyCode.F))
                    {
                        myGameOver.GameOver();
                    }
                }
            }
        }
        if (state == Parent.STATE.DIE)
        {
            Charge = 100;
            if (Charge >= 100)
            {
                BoxCol.enabled = true;
                SpherCol.enabled = false;
                Range.SetActive(false);
                Charge = 100;
                if (engage && Input.GetKeyDown(KeyCode.F))
                {
                    myGameOver.GameOver();
                }
            }
            ChargeUI.text = "<color=#ff0000>" + Charge.ToString("F0") + "%" + "</color>";
        }

    }
}

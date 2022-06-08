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

    public TMPro.TMP_Text ChargeUI;

    // Start is called before the first frame update
    void Start()
    {       
        Range = GameObject.Find("TelRange");
        BoxCol = GetComponent<BoxCollider>();
        SpherCol = GetComponent<SphereCollider>();
        SpherCol.enabled = false;
        Range.SetActive(false);
        RedF = Instantiate(Resources.Load("Effect/Red_Floating"), this.transform.position, Quaternion.identity,this.transform) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Charging();
    }

    private void OnTriggerEnter(Collider other)
    {
        engage = true;
    }
    private void OnTriggerStay(Collider other)
    {
        engage = true;
    }
    private void OnTriggerExit(Collider other)
    {
        engage = false;
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

            }
            if(_Charging)
            {
                if(Charge!=100) Charge += Time.deltaTime;
                ChargeUI.text = "<color=#ff0000>" + Charge.ToString("F0")  + "%" + "</color>";
                
                if (Charge >= 100)
                {
                    BoxCol.enabled = true;
                    SpherCol.enabled = false;
                    Range.SetActive(false);
                    Charge = 100;
                    if (engage && Input.GetKeyDown(KeyCode.F))
                    {
                        MJ_SceneLoder.inst.LoadScene(1);                       
                    }
                }
            }
        }
    }
}

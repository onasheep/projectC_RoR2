using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public float Charge = 0;
    bool engage = false;
    bool _Charging = false;
    BoxCollider BoxCol;
    SphereCollider SpherCol;
    GameObject Range;
    
    // Start is called before the first frame update
    void Start()
    {
        Range = GameObject.Find("TelRange");
        BoxCol = GetComponent<BoxCollider>();
        SpherCol = GetComponent<SphereCollider>();
        SpherCol.enabled = false;
        Range.SetActive(false);
        GameObject obj = Instantiate(Resources.Load("Effect/Red_Floating"), this.transform.position, Quaternion.identity) as GameObject;
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
            if (Input.GetKey(KeyCode.Space))
            {
                _Charging = true;
                BoxCol.enabled = false;
                SpherCol.enabled = true;
                Range.SetActive(true);

            }
            if(_Charging)
            {
                Charge += Time.deltaTime;
                if (Charge >= 100)
                {
                    BoxCol.enabled = true;
                    SpherCol.enabled = false;
                    Range.SetActive(false);
                    Charge = 100;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneLoder.inst.LoadScene(1);                       
                    }
                }
            }
        }
    }
}

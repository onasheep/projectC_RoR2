using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    bool BoxEngage = false;
    bool BoxEmpty = false;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (BoxEngage && !BoxEmpty)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.enabled = true;
                GameObject respawn = Instantiate(Resources.Load("Effect/RespawnLight1"), this.transform.localPosition, Quaternion.Euler(new Vector3(-90.0f,0f,0f))) as GameObject;
                StartCoroutine(ItemRespawn());
                
                BoxEmpty = true;
                BoxCollider Box = GetComponent<BoxCollider>();
                Destroy(Box);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BoxEngage = true;
    }
    private void OnTriggerStay(Collider other)
    {
        BoxEngage = true;
    }
    private void OnTriggerExit(Collider other)
    {
        BoxEngage = false;
    }

    IEnumerator ItemRespawn()
    {
        yield return new WaitForSeconds(3.0f);
        GameObject obj = Instantiate(Resources.Load("Item/CrowBar"), this.transform.localPosition + new Vector3(0f, 2f, 0f), Quaternion.identity) as GameObject;
        obj.transform.localScale += new Vector3(50f, 50f, 50f);
    }
}

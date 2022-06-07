using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJ_ItemBox2T : MonoBehaviour
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
                GameObject respawn = Instantiate(Resources.Load("Effect/RespawnLight1"), this.transform.localPosition, Quaternion.Euler(new Vector3(-90.0f, 0f, 0f))) as GameObject;
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
        int Rnd2T = Random.Range(0, 5);
        switch (Rnd2T)
        {
            case 0:
                yield return new WaitForSeconds(3.0f);
                GameObject obj = Instantiate(Resources.Load("NewPrefab/Item/Guillotine"), this.transform.localPosition + new Vector3(0f, 2f, 0f), Quaternion.identity) as GameObject;
                obj.transform.localScale += new Vector3(50f, 50f, 50f);
                break;
            case 1:
                yield return new WaitForSeconds(3.0f);
                GameObject obj1 = Instantiate(Resources.Load("NewPrefab/Item/Hopoo_Feather"), this.transform.localPosition + new Vector3(0f, 2f, 0f), Quaternion.identity) as GameObject;
                obj1.transform.localScale += new Vector3(50f, 50f, 50f);
                break;
            case 2:
                yield return new WaitForSeconds(3.0f);
                GameObject obj2 = Instantiate(Resources.Load("NewPrefab/Item/MissileLauncher"), this.transform.localPosition + new Vector3(0f, 2f, 0f), Quaternion.identity) as GameObject;
                obj2.transform.localScale += new Vector3(50f, 50f, 50f);
                break;
            case 3:
                yield return new WaitForSeconds(3.0f);
                GameObject obj3 = Instantiate(Resources.Load("NewPrefab/Item/Seed"), this.transform.localPosition + new Vector3(0f, 2f, 0f), Quaternion.identity) as GameObject;
                obj3.transform.localScale += new Vector3(50f, 50f, 50f);
                break;
            case 4:
                yield return new WaitForSeconds(3.0f);
                GameObject obj4 = Instantiate(Resources.Load("NewPrefab/Item/Time_Bubble"), this.transform.localPosition + new Vector3(0f, 2f, 0f), Quaternion.identity) as GameObject;
                obj4.transform.localScale += new Vector3(50f, 50f, 50f);
                break;
        }
    }
}

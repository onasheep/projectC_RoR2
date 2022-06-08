using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJ_ItemBox1T : MonoBehaviour
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                anim.enabled = true;
                
                StartCoroutine(ItemRespawn());

                BoxEmpty = true;
                BoxCollider Box = GetComponent<BoxCollider>();
                Destroy(Box);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) BoxEngage = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) BoxEngage = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) BoxEngage = false;
    }

    IEnumerator ItemRespawn()
    {
        int Respawn = Random.Range(0, 10);
        if (Respawn <= 7) 
        {
            GameObject respawn = Instantiate(Resources.Load("Effect/RespawnLight1"), this.transform.localPosition, Quaternion.Euler(new Vector3(-90.0f, 0f, 0f))) as GameObject;
            int Rnd1T = Random.Range(0, 6); // 아이템 크기(CrowBar(70),Glasses(200),Goat_Hoof(70),Medkit(100),Steak(30),SyringeCluster(50))
            switch (Rnd1T)
            {
                case 0:
                    yield return new WaitForSeconds(3.0f);
                    GameObject obj1 = Instantiate(Resources.Load("NewPrefab/Item/CrowBar"), this.transform.localPosition + new Vector3(0f, 1f, 1f), Quaternion.identity) as GameObject;
                    obj1.transform.localScale = new Vector3(70f, 70f, 70f); ;
                    break;
                case 1:
                    yield return new WaitForSeconds(3.0f);
                    GameObject obj2 = Instantiate(Resources.Load("NewPrefab/Item/Glasses"), this.transform.localPosition + new Vector3(0f, 1f, 1f), Quaternion.identity) as GameObject;
                    obj2.transform.localScale = new Vector3(200f, 200f, 200f); ;
                    break;
                case 2:
                    yield return new WaitForSeconds(3.0f);
                    GameObject obj3 = Instantiate(Resources.Load("NewPrefab/Item/Goat_Hoof"), this.transform.localPosition + new Vector3(0f, 1f, 1f), Quaternion.Euler(0f,0f,180f)) as GameObject;
                    obj3.transform.localScale = new Vector3(70f, 70f, 70f); ;
                    break;
                case 3:
                    yield return new WaitForSeconds(3.0f);
                    GameObject obj4 = Instantiate(Resources.Load("NewPrefab/Item/Medkit"), this.transform.localPosition + new Vector3(0f, 1f, 1f), Quaternion.Euler(180f, 0f, 0f)) as GameObject;
                    obj4.transform.localScale = new Vector3(100f, 100f, 100f); ;
                    break;
                case 4:
                    yield return new WaitForSeconds(3.0f);
                    GameObject obj5 = Instantiate(Resources.Load("NewPrefab/Item/Steak"), this.transform.localPosition + new Vector3(0f, 1f, 1f), Quaternion.identity) as GameObject;
                    obj5.transform.localScale = new Vector3(30f, 30f, 30f); ;
                    break;
                case 5:
                    yield return new WaitForSeconds(3.0f);
                    GameObject obj6 = Instantiate(Resources.Load("NewPrefab/Item/SyringeCluster"), this.transform.localPosition + new Vector3(0f, 1f, 1f), Quaternion.Euler(0f, -90f, 0f)) as GameObject;
                    obj6.transform.localScale = new Vector3(50f, 50f, 50f); ;
                    break;
            }
        }
        else
        {
            GameObject respawn = Instantiate(Resources.Load("Effect/RespawnLight2"), this.transform.localPosition, Quaternion.Euler(new Vector3(-90.0f, 0f, 0f))) as GameObject;
            int Rnd2T = Random.Range(0, 2);
            switch (Rnd2T)
            {                
                case 0:
                    yield return new WaitForSeconds(3.0f);
                    GameObject obj1 = Instantiate(Resources.Load("NewPrefab/Item/Hopoo_Feather"), this.transform.localPosition + new Vector3(0f, 1f, 1f), Quaternion.identity) as GameObject;
                    obj1.transform.localScale = new Vector3(50f, 50f, 50f);
                    break;                
                case 1:
                    yield return new WaitForSeconds(3.0f);
                    GameObject obj3 = Instantiate(Resources.Load("NewPrefab/Item/Seed"), this.transform.localPosition + new Vector3(0f, 1f, 1f), Quaternion.identity) as GameObject;
                    obj3.transform.localScale = new Vector3(15f, 15f, 15f);
                    break;
            }
        }
        
    }
}

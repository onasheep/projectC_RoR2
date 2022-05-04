using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Transform myRoot;
    // Start is called before the first frame update
    void Start()
    {
        //float RespawnPos_x = Random.Range(-10, 10);
        //float RespawnPos_z = Random.Range(-10, 10);
        //myRoot.transform.position = new Vector3(RespawnPos_x, 0.0f, RespawnPos_z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject obj = Instantiate(Resources.Load("Item/CrowBar"),this.transform.localPosition + new Vector3(0f,2f,0f), Quaternion.identity) as GameObject;
            obj.transform.localScale += new Vector3(50f, 50f, 50f);
            Destroy(this.transform.parent.gameObject);
        }
    }
}

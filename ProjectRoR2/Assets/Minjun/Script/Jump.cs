using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public GameObject player;
    bool Jumpstart = false;
    GameObject jumpeff;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Jumpstart)
        {
            if (Input.GetKeyDown(KeyCode.Space))//µ· Ã¼Å© Ãß°¡
            {
                GetComponent<Animator>().SetBool("Done", false);
                GetComponent<Animator>().SetTrigger("Start");
                
                StartCoroutine("Jumping");
                StartCoroutine("JumpDone");

            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        Jumpstart = true;
    }
    private void OnTriggerExit(Collider other)
    {
        Jumpstart = false;
    }

    IEnumerator Jumping()
    {
        yield return new WaitForSeconds(1.5f);
        jumpeff = Instantiate(Resources.Load("Effect/Heat Distortion"), this.transform.position, Quaternion.identity) as GameObject;
        jumpeff.transform.localScale = new Vector3(0.6f, 1.5f, 0.6f);
        player.GetComponent<Rigidbody>().AddForce(Vector3.up * 1000);
    }
    IEnumerator JumpDone()
    {
        yield return new WaitForSeconds(2.5f);
        GetComponent<Animator>().SetBool("Done", true);
        Destroy(jumpeff);
    }
}

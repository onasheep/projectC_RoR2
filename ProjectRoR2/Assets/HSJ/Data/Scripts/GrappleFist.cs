using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleFist : Character
{
    public LayerMask Crashmask;
    public float Speed = 20.0f;
    float FistCounter = 0.0f;
    LineRenderer lr;
    bool FistCheck;
    void Start()
    {
        lr = this.GetComponent<LineRenderer>();
        lr.startWidth = 0.03f;
        lr.endWidth = 0.03f;
        FistCheck = false;

    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, this.transform.localPosition );
        lr.SetPosition(1, GameObject.Find("mech.hand.end.l").GetComponent<Transform>().position);

        FistCounter += Time.deltaTime;
        Debug.Log(FistCounter);
        if (FistCounter > 1.2f && FistCheck == false)
        {
            Destroy(this.gameObject);
            FistCounter = 0.0f;
        }
    }

    


    IEnumerator FistStuck(Vector3 hit)
    {
        while(!Input.GetMouseButtonUp(1))
        {
            this.transform.position = hit;
            yield return null;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if ((Crashmask & (1 << other.gameObject.layer)) > 0)
        {
            FistCheck = true;
            Debug.Log("!");
            StartCoroutine(FistStuck(this.transform.position));

        }
        

    }

}

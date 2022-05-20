using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleFist : Character
{
    
    public LayerMask Crashmask;
    public float Speed = 20.0f;
    LineRenderer lr;
    bool FistStay = false;

    void Start()
    {
        lr = this.GetComponent<LineRenderer>();
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;


    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, this.transform.localPosition );
        lr.SetPosition(1, GameObject.Find("mech.hand.end.l").GetComponent<Transform>().position);
        

    }

    
    private void OnTriggerEnter(Collider other)
    {
        if ((Crashmask & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("!");
            Ray ray = new Ray();
            ray.origin = this.transform.position;
            ray.direction = this.transform.forward;
            if (Physics.Raycast(ray,out RaycastHit hit, 1.0f, Crashmask))
            {
                this.transform.position = hit.point;
            }

            FistStay = true;
        }
        

    }

   
    private void OnTriggerStay(Collider other)
    {

        

    }

    private void OnTriggerExit(Collider other)
    {
        if ((Crashmask & (1 << other.gameObject.layer)) > 0)
        {

            Debug.Log("?");
            //Destroy(this.gameObject);
        }
    }
    //IEnumerator FistStuck()
    //{
    //    while(!FistStay )
    //    {
    //        Ray();
    //        if(Input.GetMouseButtonUp(1))
    //        {
    //            FistStay = false;
    //        }
    //        yield return null;
    //    }
        
    //}
}

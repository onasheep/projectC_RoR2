using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GrappleFist : Character
{
    public LayerMask Crashmask;
    public float Speed = 20.0f;
    float FistCounter = 0.0f;
    LineRenderer lr;
    bool FistCheck = false;
    public Vector3 Dir = Vector3.zero;
    public SpringJoint Lsjoint = null;
    public GameObject Effect;
    public GameObject Aim;

    void Start()
    {

        Lsjoint.maxDistance = 100.0f;
        lr = this.GetComponent<LineRenderer>();
        lr.startWidth = 0.03f;
        lr.endWidth = 0.03f;
        FistCheck = false;
    }

    void Update()
    {
        lr.SetPosition(0, this.transform.localPosition );
        lr.SetPosition(1, GameObject.Find("mech.hand.end.l").GetComponent<Transform>().position);


        FistCounter += Time.deltaTime;
        if (FistCounter > 1.0f && FistCheck == false)
        {
            Destroy(this.gameObject);
            FistCounter = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        if (FistCheck) return;
        float Speed = 50.0f;
        float dist = Time.deltaTime * Speed;
        this.transform.Translate(Dir * dist, Space.World);


        Ray ray = new Ray();
        ray.origin = this.transform.position;
        ray.direction = Dir;
        

        if (Physics.Raycast(ray, out RaycastHit hit, dist, Crashmask))
        {
            Lsjoint.maxDistance = 3.0f;
            FistCheck = true;
            this.transform.position = hit.point;
            Instantiate(Effect, this.transform);
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
      
        
    }


}

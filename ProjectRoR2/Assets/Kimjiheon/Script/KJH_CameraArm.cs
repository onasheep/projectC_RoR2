using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJH_CameraArm : MonoBehaviour
{
    float myZoom;
    Vector3 StartCam;
    public float CollisionOffset = 1.0f;
    public float MouseSpeed = 200.0f;   
    [SerializeField]
    private Transform myCamera;
    [SerializeField]
    private Transform CamFollow;
    public LayerMask ZoomInMask;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCam = myCamera.localPosition;
        myZoom = -myCamera.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        CamCollision();
        FollowCam();
    }
    
    public void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = this.transform.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;               
        if (x < 180.0f)
        {
            x = Mathf.Clamp(x, -1.0f, 70.0f);
        }
        else
        {
            x = Mathf.Clamp(x, 295.0f, 361f);
        }
        this.transform.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);

    }

    public void FollowCam()
    {
        this.transform.position = CamFollow.position;
    }

    public void CamCollision()
    {       
        Debug.DrawRay(transform.position, myCamera.transform.position - transform.position, Color.red);
        Ray ray = new Ray(this.transform.position, myCamera.transform.position - this.transform.position);
        if (Physics.Raycast(ray, out RaycastHit hit, myZoom, ZoomInMask))
        {
            if ((ZoomInMask & 1 << hit.transform.gameObject.layer) != 0)
            {                
                myCamera.transform.localPosition = Vector3.Lerp(myCamera.transform.localPosition, hit.point, Time.deltaTime * 2.0f);
                myCamera.transform.position = hit.point;
            }
        }
        else 
        {
            myCamera.transform.localPosition = Vector3.Lerp(myCamera.transform.localPosition, StartCam, Time.deltaTime * 5.0f);
        }
    }
}

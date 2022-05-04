using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour
{
    float rx;
    float Animrx;
    float ry;
    float xMouse;
    float yMouse;
    float myZoom;
    Vector3 StartCam;
    public float CollisionOffset = 1.0f;
    public float MouseSpeed = 200.0f;
    public Transform myPlayer;
    public Transform myCamera;
    public LayerMask ZoomInMask;
    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCam = myCamera.localPosition;
        myZoom = -myCamera.localPosition.z;
        myAnim = myPlayer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        CamCollision();
    }

    public void LookAround()
    {
        xMouse = Input.GetAxis("Mouse X");
        yMouse = Input.GetAxis("Mouse Y");

        rx += yMouse * MouseSpeed * Time.deltaTime;
        ry += xMouse * MouseSpeed * Time.deltaTime;
        rx = Mathf.Clamp(rx, -70.0f, 70.0f);
        this.transform.eulerAngles = new Vector3(-rx, ry, 0);
        myPlayer.transform.eulerAngles = new Vector3(0, ry, 0);

    }
    public void CamCollision()
    {
        Debug.DrawRay(transform.position, myCamera.transform.position - transform.position, Color.red);
        Ray ray = new Ray(this.transform.position, myCamera.transform.position - this.transform.position);
        float Dist = (myPlayer.transform.position - transform.position).magnitude;
        if (Physics.Raycast(ray, out RaycastHit hit, myZoom, ZoomInMask))
        {
            if ((ZoomInMask & 1 << hit.transform.gameObject.layer) != 0  )
            {
                myCamera.transform.localPosition = Vector3.Lerp(myCamera.transform.localPosition, hit.point, Time.deltaTime * 2.0f);
                myCamera.transform.position = hit.point;
            }
           

        }
        else
        {
            myCamera.transform.localPosition = Vector3.Lerp(myCamera.transform.localPosition, StartCam, Time.deltaTime * 10.0f);
        }
    }
}

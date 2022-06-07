using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    Vector3 ScreenCenter;
    public Transform Canvas;
    public Camera Aimcamera;
    // Start is called before the first frame update
    void Start()
    {
        MakeAim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakeAim()
    {
        ScreenCenter = new Vector3(Aimcamera.pixelWidth / 2, Aimcamera.pixelHeight / 2);
        Ray ray = Aimcamera.ScreenPointToRay(ScreenCenter);
        GameObject Crosshair = Instantiate(Resources.Load("Prefeb/Crosshair"), Canvas) as GameObject;
    }
}

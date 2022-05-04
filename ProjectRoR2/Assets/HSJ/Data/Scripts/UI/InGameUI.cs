using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    Vector3 ScreenCenter;
    public Transform Canvas;
    public Camera Aimcamera;
    public TMPro.TMP_Text TimeText;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        MakeAim();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }
    public void MakeAim()
    {
        ScreenCenter = new Vector3(Aimcamera.pixelWidth / 2, Aimcamera.pixelHeight / 2);
        Ray ray = Aimcamera.ScreenPointToRay(ScreenCenter);
        GameObject Crosshair = Instantiate(Resources.Load("Prefabs/CrossHair"), Canvas) as GameObject;
    }

    void Timer()
    {
        time += Time.deltaTime;
        string hour = ((int)time / 3600).ToString();
        string min = ((int)time / 60).ToString();
        string sec = ((int)time).ToString();
        TimeText.text = string.Format("{0:N1}",time);
    }
}

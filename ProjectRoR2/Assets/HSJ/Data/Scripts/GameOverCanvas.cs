using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{
    public static GameOverCanvas GameOverMachine = null;
    public GameObject Tab_InvenBase;
    public Transform InvenPos;
    public GameObject Canvas;
    public GameObject Panel;
    public AudioListener mySound;
    public KJH_CameraArm myCamera;
    public Camera Aimcamera;
    public TMPro.TMP_Text Timer;
    float time;
    private void Awake()
    {
        

        GameOverMachine = this;
        if (DontDestroyobject.instance.CharSelected == 1)
        {
            //myCommando = GameObject.Find("mdlCommandoDualies (merge)").GetComponent<KJH_Player>();
            Aimcamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            mySound = GameObject.Find("Main Camera").GetComponent<AudioListener>();
            myCamera = GameObject.Find("CamArm").GetComponent<KJH_CameraArm>();

        }

        else if (DontDestroyobject.instance.CharSelected == 2)
        {
            //myLoader = GameObject.Find("mdlLoader (merge)").GetComponent<Loader>();
            Aimcamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            mySound = GameObject.Find("Main Camera").GetComponent<AudioListener>();
            myCamera = GameObject.Find("SpringArm").GetComponent<KJH_CameraArm>();

        }
    }

    void Update()
    {
        time += Time.deltaTime;
        Timer.text = string.Format("{0:N1}초 동안 게임을 플레이 했습니다", time);
    }

    public void GameOver()
    {
        Canvas.SetActive(false);
        Panel.gameObject.SetActive(true);
        Tab_InvenBase.transform.SetParent(InvenPos);
        Tab_InvenBase.SetActive(true);

        Time.timeScale = 0.0f;
        Time.fixedDeltaTime = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mySound.enabled = false;
        myCamera.enabled = false;
    }
    public void QuitGame()
    {
        Application.Quit();
        
    }

    public void KeepGame()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        SceneManager.LoadScene(1);
    }

}

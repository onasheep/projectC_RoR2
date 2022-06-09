using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    // 에임 
    Vector3 ScreenCenter;
    public Transform Canvas;
    public Camera Aimcamera;


    public Loader myLoader;
    public KJH_Player myCommando;
    public KJH_CameraArm myCamera;
    public AudioListener mySound;
    // timer
    public TMPro.TMP_Text TimeText;
    float time;

    // hpbar
    public KJH_CharacterStat myStat;
    [SerializeField] private Slider myHpbar;
    float maxHp = 100;  // 최대체력과 현재체력은 나중에 스탯에서 가져오면 대체할 수 있음 
    float curHP = 100; // 작동용으로 만들어 둔것 
    public TMPro.TMP_Text HpbarText;

    // expbar
    [SerializeField] private Slider myExpbar;
    float maxExp = 100;
    float curExp = 0.0f;
    public TMPro.TMP_Text LevelText;
    int Level = 1;

    // Gold
    public TMPro.TMP_Text GoldText;
    int Gold = 0;

    // Esc 
    public GameObject Esc = null;
    bool EscActive = false;

    private void Awake()
    {
        if (DontDestroyobject.instance.CharSelected == 1)
        {
            myCommando = GameObject.Find("mdlCommandoDualies (merge)").GetComponent<KJH_Player>();
            Aimcamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            mySound = GameObject.Find("Main Camera").GetComponent<AudioListener>();
            myCamera = GameObject.Find("CamArm").GetComponent<KJH_CameraArm>();

        }

        else if (DontDestroyobject.instance.CharSelected == 2)
        {
            myLoader = GameObject.Find("mdlLoader (merge)").GetComponent<Loader>();
            Aimcamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            mySound = GameObject.Find("Main Camera").GetComponent<AudioListener>();
            myCamera = GameObject.Find("SpringArm").GetComponent<KJH_CameraArm>();

        }

    }

    void Start()
    {
      


     
        

        myHpbar.value = (float)curHP / (float)maxHp;
        myExpbar.value = (float)curExp / (float)maxExp;
        LevelText.text = "레벨 : " + Level;
        MakeAim();
    }

    // Update is called once per frame
    void Update()
    {

        Timer();
        HpBarController();
        ExpBarController();
        GainGold();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscMenu();
        }
     
    }
    public void MakeAim()
    {
        ScreenCenter = new Vector3(Aimcamera.pixelWidth / 2, Aimcamera.pixelHeight / 2);
        Ray ray = Aimcamera.ScreenPointToRay(ScreenCenter);
        if (DontDestroyobject.instance.CharSelected == 1)
        {
            GameObject Crosshair = Instantiate(Resources.Load("Prefeb/Crosshair"), Canvas) as GameObject;

        }
        if (DontDestroyobject.instance.CharSelected == 2)
        {
            GameObject Crosshair = Instantiate(Resources.Load("Prefabs/UI/CrossHair"), Canvas) as GameObject;
        }

    }

    private void Timer()
    {
        time += Time.deltaTime;
        TimeText.text = string.Format("{0:N1}",time);
    }

    // 이 아래로
    // 데이터 추가되면 수정 할 것
    private void HpBarController() // damage를 변수로 받아와서 사용 
    {
        if(Input.GetKeyDown(KeyCode.M)) // 조건 나중에 바꿈
        {
            if(curHP > 0)
            {
                curHP -= 10; // 나중에 데미지로 바꿈 

            }
            else if(curHP <= 0)
            {
                if(DontDestroyobject.instance.CharSelected == 1)
                {
                    myCommando.ChangeState(KJH_Player.STATE.DEAD);
                }
                if (DontDestroyobject.instance.CharSelected == 2)
                {
                    myLoader.ChangeState(Loader.STATE.DEAD);
                }
                curHP = 0;
            }
        }
        myHpbar.value = Mathf.Lerp(myHpbar.value, (float)curHP / (float)maxHp, Time.deltaTime * 10.0f); // hp 감소 Lerp로 부드럽게 
        HpbarText.text = curHP + "/" + maxHp; // hpbar 텍스트 변경 
    }

    private void ExpBarController()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            if(curExp < maxExp) // 레벨업했을때 
            {
                curExp += 10.0f;
            }
            if(curExp == maxExp)
            {
                Level++;
                LevelText.text = "레벨 : " + Level; // 레벨업마다 레벨업 수치 반영 
                maxExp += 10.0f; // 레벨업하면 요구 경험치 상승 임의 수치 10.0f;
                curExp = 0.0f;
            }      
        }
        myExpbar.value = Mathf.Lerp(myExpbar.value, (float)curExp / (float)maxExp, Time.deltaTime * 10.0f);
    }

    private void GainGold()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Gold += 10;

        }
        //if(Gold < Price)
        //{

        //}
        //if( 아이템을 샀다 ) 
        //{
        //    Gold -= Price;
        //}
        GoldText.text = Gold.ToString();
    }

    public void EscMenu()
    {       
        if(EscActive == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Esc.SetActive(true);
            Time.timeScale = 0.0f;
            Time.fixedDeltaTime = 0.0f;
            EscActive = true;
            if (DontDestroyobject.instance.CharSelected == 1)
            {
                myCommando.ChangeState(KJH_Player.STATE.PAUSE);
            }
            else if(DontDestroyobject.instance.CharSelected == 2)
            {
                myLoader.ChangeState(Loader.STATE.PAUSE);

            }
            mySound.GetComponent<AudioListener>().enabled = false;
            myCamera.GetComponent<KJH_CameraArm>().enabled = false;
        }
        else if(EscActive == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Esc.SetActive(false);
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale ;
            EscActive = false;
            if (DontDestroyobject.instance.CharSelected == 1)
            {
                myCommando.ChangeState(KJH_Player.STATE.PLAY);
            }
            else if (DontDestroyobject.instance.CharSelected == 2)
            {
                myLoader.ChangeState(Loader.STATE.PLAY);

            }
            mySound.GetComponent<AudioListener>().enabled = true;
            myCamera.GetComponent<KJH_CameraArm>().enabled = true;
        }

    }
}

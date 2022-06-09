using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    // ���� 
    Vector3 ScreenCenter;
    public Transform Canvas;
    public Camera Aimcamera;
    public AttackSystem myAttackSystem;

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
    float maxHp = 100;  // �ִ�ü�°� ����ü���� ���߿� ���ȿ��� �������� ��ü�� �� ���� 
    float curHP = 100; // �۵������� ����� �а� 
    public TMPro.TMP_Text HpbarText = null;

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
            myAttackSystem = GameObject.Find("AttackSystem").GetComponent<AttackSystem>();
            Aimcamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            mySound = GameObject.Find("Main Camera").GetComponent<AudioListener>();
            myCamera = GameObject.Find("CamArm").GetComponent<KJH_CameraArm>();

        }

        else if (DontDestroyobject.instance.CharSelected == 2)
        {
            myLoader = GameObject.Find("mdlLoader (merge)").GetComponent<Loader>();
            myAttackSystem = GameObject.Find("AttackSystem").GetComponent<AttackSystem>();
            Aimcamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            mySound = GameObject.Find("Main Camera").GetComponent<AudioListener>();
            myCamera = GameObject.Find("SpringArm").GetComponent<KJH_CameraArm>();

        }

    }

    void Start()
    {


     
        

        myHpbar.value = (float)myAttackSystem.myCharacterStat.curHP / (float)myAttackSystem.myCharacterStat.maxHP;
        myExpbar.value = (float)curExp / (float)maxExp;
        LevelText.text = "���� : " + Level;
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

    // �� �Ʒ���
    // ������ �߰��Ǹ� ���� �� ��
    private void HpBarController() // damage�� ������ �޾ƿͼ� ��� 
    {
        
       
        myHpbar.value = Mathf.Lerp(myHpbar.value, (float)myAttackSystem.myCharacterStat.curHP / (float)myAttackSystem.myCharacterStat.maxHP, Time.deltaTime * 10.0f); // hp ���� Lerp�� �ε巴�� 
        HpbarText.text = myAttackSystem.myCharacterStat.curHP + "/" + myAttackSystem.myCharacterStat.maxHP; // hpbar �ؽ�Ʈ ���� 
    }

    private void ExpBarController()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            if(curExp < maxExp) // ������������ 
            {
                curExp += 10.0f;
            }
            if(curExp == maxExp)
            {
                Level++;
                LevelText.text = "���� : " + Level; // ���������� ������ ��ġ �ݿ� 
                maxExp += 10.0f; // �������ϸ� �䱸 ����ġ ��� ���� ��ġ 10.0f;
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
        //if( �������� ��� ) 
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

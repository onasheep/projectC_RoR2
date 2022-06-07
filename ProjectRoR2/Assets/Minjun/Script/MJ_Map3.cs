using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJ_Map2 : MonoBehaviour
{
    public Transform Player;
    public Transform _BoxPoint1;
    public Transform _BoxPoint2;
    public Transform _BoxPoint3;
    public Transform _BoxPoint4;
    public Transform _BoxPoint5;
    public Transform _BoxPoint6;
    public Transform _BoxPoint7;
    public Transform _BoxPoint8;
    public Transform _BoxPoint9;
    public Transform _BoxPoint10;
    public Transform _BoxPoint11;
    public Transform _BoxPoint12;
    public Transform _BoxPoint13;
    public Transform _BoxPoint14;
    public Transform _BoxPoint15;
    public Transform _BoxPoint16;
    public Transform _BoxPoint17;
    public Transform _BoxPoint18;
    public Transform _BoxPoint19;
    public Transform _BoxPoint20;
    public Transform _BoxPoint21;
    public Transform _BoxPoint22;
    public Transform _BoxPoint23;
    public Transform _BoxPoint24;

    public Transform _StartPoint1;
    public Transform _StartPoint2;
    public Transform _StartPoint3;

    public Transform _TelPoint1;
    public Transform _TelPoint2;
    public Transform _TelPoint3;

    // Start is called before the first frame update
    void Start()
    {
        List<int> BoxRespawnList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 ,20,21,22,23};
        for (int i = 0; i < 12; i++)
        {
            int rand = Random.Range(0, BoxRespawnList.Count);
            switch (BoxRespawnList[rand])
            {
                case 0:
                    GameObject Box20 = Instantiate(Resources.Load("NewPrefab/mdlChest2"), _BoxPoint20.position, Quaternion.identity) as GameObject;
                    //Box20.transform.localScale = new Vector3(7468f, 7468f, 7468f);
                    break;
                case 1:
                    GameObject Box1 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint1.position, Quaternion.identity) as GameObject;
                    //Box1.transform.localScale = new Vector3(7468f, 7468f, 7468f);
                    break;
                case 2:
                    GameObject Box2 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint2.position, Quaternion.identity) as GameObject;
                    //Box2.transform.localScale = new Vector3(7468f, 7468f, 7468f);
                    break;
                case 3:
                    GameObject Box3 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint3.position, Quaternion.identity) as GameObject;
                    //Box3.transform.localScale = new Vector3(7468f, 7468f, 7468f);
                    break;
                case 4:
                    GameObject Box4 = Instantiate(Resources.Load("NewPrefab/mdlChest2"), _BoxPoint4.position, Quaternion.identity) as GameObject;
                    //Box4.transform.localScale = new Vector3(7468f, 7468f, 7468f);
                    break;
                case 5:
                    GameObject Box5 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint5.position, Quaternion.identity) as GameObject;
                    //Box5.transform.localScale = new Vector3(7468f, 7468f, 7468f);
                    break;
                case 6:
                    GameObject Box6 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint6.position, Quaternion.identity) as GameObject;
                    //Box6.transform.localScale = new Vector3(7468f, 7468f, 7468f);
                    break;
                case 7:
                    GameObject Box7 = Instantiate(Resources.Load("NewPrefab/mdlChest2"), _BoxPoint7.position, Quaternion.identity) as GameObject;
                    break;
                case 8:
                    GameObject Box8 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint8.position, Quaternion.identity) as GameObject;
                    break;
                case 9:
                    GameObject Box9 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint9.position, Quaternion.identity) as GameObject;
                    break;
                case 10:
                    GameObject Box10 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint10.position, Quaternion.identity) as GameObject;
                    break;
                case 11:
                    GameObject Box11 = Instantiate(Resources.Load("NewPrefab/mdlChest2"), _BoxPoint11.position, Quaternion.identity) as GameObject;
                    break;
                case 12:
                    GameObject Box12 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint12.position, Quaternion.identity) as GameObject;
                    break;
                case 13:
                    GameObject Box13 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint13.position, Quaternion.identity) as GameObject;
                    break;
                case 14:
                    GameObject Box14 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint14.position, Quaternion.identity) as GameObject;
                    break;
                case 15:
                    GameObject Box15 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint15.position, Quaternion.identity) as GameObject;
                    break;
                case 16:
                    GameObject Box16 = Instantiate(Resources.Load("NewPrefab/mdlChest2"), _BoxPoint16.position, Quaternion.identity) as GameObject;
                    break;
                case 17:
                    GameObject Box17 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint17.position, Quaternion.identity) as GameObject;
                    break;
                case 18:
                    GameObject Box18 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint18.position, Quaternion.identity) as GameObject;
                    break;
                case 19:
                    GameObject Box19 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint19.position, Quaternion.identity) as GameObject;
                    break;
                case 21:
                    GameObject Box21 = Instantiate(Resources.Load("NewPrefab/mdlChest2"), _BoxPoint19.position, Quaternion.identity) as GameObject;
                    break;
                case 22:
                    GameObject Box22 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint19.position, Quaternion.identity) as GameObject;
                    break;
                case 23:
                    GameObject Box23 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint19.position, Quaternion.identity) as GameObject;
                    break;


            }
            BoxRespawnList.RemoveAt(rand);
        }

        List<int> TelRespawnList = new List<int>() { 0, 1, 2 };
        int Trand = Random.Range(0, TelRespawnList.Count);
        switch (TelRespawnList[Trand])
        {
            case 0:
                GameObject Tel1 = Instantiate(Resources.Load("NewPrefab/mdlTeleporter1"), _TelPoint1.position, Quaternion.identity) as GameObject;
                break;
            case 1:
                GameObject Tel2 = Instantiate(Resources.Load("NewPrefab/mdlTeleporter1"), _TelPoint2.position, Quaternion.identity) as GameObject;
                break;
            case 2:
                GameObject Tel3 = Instantiate(Resources.Load("NewPrefab/mdlTeleporter1"), _TelPoint3.position, Quaternion.identity) as GameObject;
                break;
        }

        List<int> PlayerRespawnList = new List<int>() { 0, 1, 2 };
        int Prand = Random.Range(0, PlayerRespawnList.Count);
        switch (PlayerRespawnList[Prand])
        {
            case 0:
            //Player.transform.position = _StartPoint1.transform.position;
            //break;
            case 1:
                Player.transform.position = _StartPoint2.transform.position;
                break;
            case 2:
                Player.transform.position = _StartPoint2.transform.position; // 3���� ����
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJ_Map1 : MonoBehaviour
{
    public GameObject Player;
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
    public Transform _StartPoint1;
    public Transform _StartPoint2;
    public Transform _StartPoint3;
    public Transform _TelPoint1;
    public Transform _TelPoint2;
    public Transform _TelPoint3;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("mdlCommandoDualies (merge)");
        if (Player == null)
        {
            Player = GameObject.Find("mdlLoader (merge)");
        }

        List<int> BoxRespawnList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
        for (int i = 0; i< 10; i++)
        {
            int rand = Random.Range(0, BoxRespawnList.Count);
            switch (BoxRespawnList[rand])
            {
                case 0:
                    GameObject Box20 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint20.position - new Vector3(0f,0.7f,0f), Quaternion.identity) as GameObject;
                    break;
                case 1:
                    GameObject Box1 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint1.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 2:
                    GameObject Box2 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint2.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 3:
                    GameObject Box3 = Instantiate(Resources.Load("NewPrefab/mdlChest2"), _BoxPoint3.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 4:
                    GameObject Box4 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint4.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 5:
                    GameObject Box5 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint5.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 6:
                    GameObject Box6 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint6.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 7:
                    GameObject Box7 = Instantiate(Resources.Load("NewPrefab/mdlChest2"), _BoxPoint7.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 8:
                    GameObject Box8 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint8.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 9:
                    GameObject Box9 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint9.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 10:
                    GameObject Box10 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint10.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 11:
                    GameObject Box11 = Instantiate(Resources.Load("NewPrefab/mdlChest2"), _BoxPoint11.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 12:
                    GameObject Box12 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint12.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 13:
                    GameObject Box13 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint13.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 14:
                    GameObject Box14 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint14.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 15:
                    GameObject Box15 = Instantiate(Resources.Load("NewPrefab/mdlChest2"), _BoxPoint15.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 16:
                    GameObject Box16 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint16.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 17:
                    GameObject Box17 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint17.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 18:
                    GameObject Box18 = Instantiate(Resources.Load("NewPrefab/mdlChest1"), _BoxPoint18.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;
                case 19:
                    GameObject Box19 = Instantiate(Resources.Load("NewPrefab/mdlChest2"), _BoxPoint19.position - new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    break;

            }
            BoxRespawnList.RemoveAt(rand);
        }

        List<int> TelRespawnList = new List<int>() { 0, 1, 2 };
        int Trand = Random.Range(0, TelRespawnList.Count);
        switch (TelRespawnList[Trand])
        {
            case 0:
                GameObject Tel1 = Instantiate(Resources.Load("NewPrefab/mdlTeleporter1"), _TelPoint1.position - new Vector3(0f,1.5f,0f), Quaternion.identity) as GameObject;
                break;
            case 1:
                GameObject Tel2 = Instantiate(Resources.Load("NewPrefab/mdlTeleporter1"), _TelPoint2.position - new Vector3(0f, 1.5f, 0f), Quaternion.identity) as GameObject;
                break;
            case 2:
                GameObject Tel3 = Instantiate(Resources.Load("NewPrefab/mdlTeleporter1"), _TelPoint3.position - new Vector3(0f, 1.5f, 0f), Quaternion.identity) as GameObject;
                break;
        }

        List<int> PlayerRespawnList = new List<int>() { 0, 1, 2 };
        int Prand = Random.Range(0, PlayerRespawnList.Count);
        switch (PlayerRespawnList[Prand])
        {
            case 0:
                Player.transform.position = _StartPoint1.transform.position;
                break;
            case 1:
                Player.transform.position = _StartPoint2.transform.position;
                break;
            case 2:
                Player.transform.position = _StartPoint3.transform.position;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }    
}



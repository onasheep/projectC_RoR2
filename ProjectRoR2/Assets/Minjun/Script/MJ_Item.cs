using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MJ_Item : MonoBehaviour
{
    public LayerMask playerLayer; //아이템과 충돌처리해주기위한 레이어마스크

    KJH_Player C_playerStat; //코만도 스텟 받아오기 
    Loader L_playerStat;    //로더 스텟 받아오기
    public GameObject stat; //플레이어 존재 여부 체크

    public MJ_Inventory myinven;
    public MJ_ItemData itemdata;
    public Transform ItemContent;


    public GameObject C_Head;   //아이템이 붙을 위치 
    public GameObject C_RArm;
    public GameObject C_LArm;
    public GameObject C_RLeg;
    public GameObject C_LLeg;
    public GameObject C_Chest;
    public GameObject C_Foot;
    public GameObject C_Plevis;

    public string player_Type;  //플레이어가 로더인지 코만도인지 확인

    void Start()
    {
        myinven = GameObject.Find("InGameUICanvas").GetComponent<MJ_Inventory>();
        itemdata = GameObject.Find("ItemDataBase").GetComponent<MJ_ItemData>();
        

        ItemContent = GameObject.Find("InGameUICanvas").transform.Find("Tab_InvenBase").transform.Find("Bottom").transform.Find("Content_Item").transform;

        stat = GameObject.Find("mdlCommandoDualies (merge)");
        if (stat != null)
        {
            C_playerStat = stat.GetComponent<KJH_Player>();
            player_Type = "Commando";
        }
        if (stat == null)
        {
            stat = GameObject.Find("mdlLoader (merge)");
            L_playerStat = stat.GetComponent<Loader>();
            player_Type = "Loader";
        }

        C_Head = GameObject.Find("head_end");
        C_RArm = GameObject.Find("upper_arm.r");
        C_LArm = GameObject.Find("upper_arm.l");
        C_RLeg = GameObject.Find("thigh.r");
        C_LLeg = GameObject.Find("thigh.l");
        C_Chest = GameObject.Find("chest");
        C_Foot = GameObject.Find("foot.r");
        C_Plevis = GameObject.Find("pelvis");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * 180.0f * Time.deltaTime);
    }


    void C_Equip()
    {
        
        if (this.gameObject.name.Contains("Crowbar"))
        {            
            if (itemdata.equip[0] == 0)
            {
                itemdata.equip[0]++;
                myinven.AddItem(0, "Crowbar");
                myinven.invenitemname.Add("Crowbar");

                this.transform.parent = C_Chest.transform;
                this.transform.rotation = Quaternion.Euler(C_Chest.transform.rotation.eulerAngles.x, C_Chest.transform.rotation.eulerAngles.y, C_Chest.transform.rotation.eulerAngles.z);
                this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                this.transform.position = C_Chest.transform.position + new Vector3(0.15f, 0.15f, -0.2f); //위치 세부 조정


                this.gameObject.GetComponent<MJ_Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
            }
            else
            {
                itemdata.equip[0]++;
                for(int i=0;i< myinven.invenitemname.Count;i++)
                {
                    if (myinven.invenitemname[i] == "Crowbar")
                    {
                        ItemContent.transform.Find("Crowbar").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[0];
                    }
                }
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Glasses"))
        {            
            //playerStat.myCharacterStat.critical += 0.1f; 

            if (itemdata.equip[1] >= 0)
            {
                itemdata.equip[1]++;
                myinven.AddItem(1, "Glasses");
                myinven.invenitemname.Add("Glasses");
                this.transform.rotation = Quaternion.Euler(C_Head.transform.rotation.eulerAngles.x, C_Head.transform.rotation.eulerAngles.y , C_Head.transform.rotation.eulerAngles.z); // 10,118,-10
                Debug.Log(C_Head.transform.rotation.eulerAngles +""+ this.transform.rotation.eulerAngles);
                this.transform.parent = C_Head.transform;
                this.transform.localScale = new Vector3(1f, 1f, 1f);
                this.gameObject.transform.position = C_Head.transform.position;


                this.gameObject.GetComponent<MJ_Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");

            }
            else
            {
                itemdata.equip[1]++;
                for (int i = 0; i < myinven.invenitemname.Count; i++)
                {
                    if (myinven.invenitemname[i] == "Glasses")
                    {
                        ItemContent.transform.Find("Glasses").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[1];
                    }
                }
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Goat_Hoof"))
        {
            C_playerStat.myCharacterStat.WalkSpeed += 0.5f;
            if (itemdata.equip[2] == 0)
            {
                itemdata.equip[2]++;
                myinven.AddItem(2, "Goat_Hoof");
                myinven.invenitemname.Add("Goat_Hoof");
                this.transform.rotation = Quaternion.Euler(C_Foot.transform.rotation.eulerAngles.x, C_Foot.transform.rotation.eulerAngles.y, C_Foot.transform.rotation.eulerAngles.z); // 각도 조정
                this.transform.parent = C_Foot.transform;
                this.transform.localScale = new Vector3(1f, 1f, 1f);
                this.gameObject.transform.position = C_Foot.transform.position;

                this.gameObject.GetComponent<MJ_Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
            }
            else
            {
                itemdata.equip[2]++;
                for (int i = 0; i < myinven.invenitemname.Count; i++)
                {
                    if (myinven.invenitemname[i] == "Goat_Hoof")
                    {
                        ItemContent.transform.Find("Goat_Hoof").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[2];
                    }
                }
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Medkit"))
        {            
            if (itemdata.equip[3] == 0)
            {
                itemdata.equip[3]++;
                myinven.AddItem(3, "Medkit");
                myinven.invenitemname.Add("Medkit");
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.transform.parent = C_RLeg.transform;
                this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                this.gameObject.transform.position = C_RLeg.transform.position + new Vector3(0.1f, 0f, 0f);

                this.gameObject.GetComponent<MJ_Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");

            }
            else
            {
                itemdata.equip[3]++;
                for (int i = 0; i < myinven.invenitemname.Count; i++)
                {
                    if (myinven.invenitemname[i] == "Medkit")
                    {
                        ItemContent.transform.Find("Medkit").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[3];
                    }
                }
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Steak"))
        {

            C_playerStat.myCharacterStat.maxHP += 25.0f;
            if (itemdata.equip[4] == 0)
            {
                itemdata.equip[4]++;
                myinven.AddItem(4, "Steak");
                myinven.invenitemname.Add("Steak");
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.transform.parent = C_Chest.transform;
                this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                this.gameObject.transform.position = C_Chest.transform.position + new Vector3(0f,0.2f,0.2f);

                this.gameObject.GetComponent<MJ_Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
            }
            else
            {
                itemdata.equip[4]++;
                for (int i = 0; i < myinven.invenitemname.Count; i++)
                {
                    if (myinven.invenitemname[i] == "Steak")
                    {
                        ItemContent.transform.Find("Steak").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[4];
                    }
                }
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("SyringeCluster"))
        {
            
            //playerStat.myCharacterStat.AttackSpeed += SyringeClusterSpeed;
            if (itemdata.equip[5] == 0)
            {
                itemdata.equip[5]++;
                myinven.AddItem(5, "SyringeCluster");
                myinven.invenitemname.Add("SyringeCluster");
                this.transform.rotation = Quaternion.Euler(0, 180, -30);
                this.transform.parent = C_LLeg.transform;
                this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                this.gameObject.transform.position = C_LLeg.transform.position + new Vector3(-0.15f, 0, 0);

                this.gameObject.GetComponent<MJ_Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
            }
            else
            {
                itemdata.equip[5]++;
                for (int i = 0; i < myinven.invenitemname.Count; i++)
                {
                    if (myinven.invenitemname[i] == "SyringeCluster")
                    {
                        ItemContent.transform.Find("SyringeCluster").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[5];
                    }
                }
                Destroy(this.gameObject);
            }
        }


        if (this.gameObject.name.Contains("Hopoo_Feather"))
        {
            C_playerStat.myCharacterStat.JumpCount += 1;
            if (itemdata.equip[6] == 0)
            {
                itemdata.equip[6]++;
                myinven.AddItem(6, "Hopoo_Feather");
                myinven.invenitemname.Add("Hopoo_Feather");
                this.transform.rotation = Quaternion.Euler(-10, 180, -20);
                this.transform.parent = C_LArm.transform;
                this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                this.gameObject.transform.position = C_LArm.transform.position + new Vector3(-0.15f , 0 , 0.1f); //위치 세부 조정

                this.gameObject.GetComponent<MJ_Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
            }
            else
            {
                itemdata.equip[6]++;
                for (int i = 0; i < myinven.invenitemname.Count; i++)
                {
                    if (myinven.invenitemname[i] == "Hopoo_Feather")
                    {
                        ItemContent.transform.Find("Hopoo_Feather").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[6];
                    }
                }
                Destroy(this.gameObject);
            }
        }

        if (this.gameObject.name.Contains("Seed"))
        {            
            if (itemdata.equip[7] == 0)
            {
                itemdata.equip[7]++;
                myinven.AddItem(7, "Seed");
                myinven.invenitemname.Add("Seed");
                this.transform.rotation = Quaternion.Euler(0, 40, 0);
                this.transform.parent = C_Plevis.transform;
                this.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                this.gameObject.transform.position = C_Plevis.transform.position + new Vector3(-0.2f,0,0); //위치 세부 조정

                this.gameObject.GetComponent<MJ_Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
            }
            else
            {
                itemdata.equip[7]++;
                for (int i = 0; i < myinven.invenitemname.Count; i++)
                {
                    if (myinven.invenitemname[i] == "Seed")
                    {
                        ItemContent.transform.Find("Seed").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[7];
                    }
                }
                Destroy(this.gameObject);
            }
        }
     
        if (this.gameObject.name.Contains("Titan_Knurl"))
        {
            C_playerStat.myCharacterStat.maxHP += 40.0f;
            C_playerStat.myCharacterStat.HP_Heal += 1.6f;

            if (itemdata.equip[8] == 0)
            {
                itemdata.equip[8]++;
                myinven.AddItem(8, "Titan_Knurl");
                myinven.invenitemname.Add("Titan_Knurl");
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.transform.parent = C_Head.transform;
                this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                this.gameObject.transform.position = C_Head.transform.position + new Vector3(0.2f, 0.03f, 0f);


                this.gameObject.GetComponent<MJ_Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
            }
            else
            {
                itemdata.equip[8]++;
                for (int i = 0; i < myinven.invenitemname.Count; i++)
                {
                    if (myinven.invenitemname[i] == "Titan_Knurl")
                    {
                        ItemContent.transform.Find("Titan_Knurl").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[8];
                    }
                }
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("LightningStrike"))
        {
            
            if (itemdata.equip[9] == 0)
            {
                itemdata.equip[9]++;
                myinven.AddItem(9, "LightningStrike");
                myinven.invenitemname.Add("LightningStrike");
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.transform.parent = C_Chest.transform;
                this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                this.gameObject.transform.position = C_Chest.transform.position; //위치 세부 조정

                this.gameObject.GetComponent<MJ_Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
            }
            else
            {
                itemdata.equip[9]++;
                for (int i = 0; i < myinven.invenitemname.Count; i++)
                {
                    if (myinven.invenitemname[i] == "LightningStrike")
                    {
                        ItemContent.transform.Find("LightningStrike").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[9];
                    }
                }
                Destroy(this.gameObject);
            }
        }
    }

    //=================================================================================================

    void L_Equip()
    {
        {
            if (this.gameObject.name.Contains("Crowbar"))
            {
                if (itemdata.equip[0] == 0)
                {
                    itemdata.equip[0]++;
                    myinven.AddItem(0, "Crowbar");
                    myinven.invenitemname.Add("Crowbar");

                    this.transform.parent = C_Chest.transform;
                    this.transform.rotation = Quaternion.Euler(C_Chest.transform.rotation.eulerAngles.x, C_Chest.transform.rotation.eulerAngles.y, C_Chest.transform.rotation.eulerAngles.z);
                    this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    this.transform.position = C_Chest.transform.position + new Vector3(0.15f, 0.15f, -0.2f); //위치 세부 조정

                    this.gameObject.GetComponent<MJ_Item>().enabled = false;
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
                }
                else
                {
                    itemdata.equip[0]++;
                    for (int i = 0; i < myinven.invenitemname.Count; i++)
                    {
                        if (myinven.invenitemname[i] == "Crowbar")
                        {
                            ItemContent.transform.Find("Crowbar").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[0];
                        }
                    }
                    Destroy(this.gameObject);
                }
            }
            if (this.gameObject.name.Contains("Glasses"))
            {
                //playerStat.myCharacterStat.critical += 0.1f; 

                if (itemdata.equip[1] >= 0)
                {
                    itemdata.equip[1]++;
                    myinven.AddItem(1, "Glasses");
                    myinven.invenitemname.Add("Glasses");
                    this.transform.rotation = Quaternion.Euler(C_Head.transform.rotation.eulerAngles.x, C_Head.transform.rotation.eulerAngles.y, C_Head.transform.rotation.eulerAngles.z); // 10,118,-10
                    Debug.Log(C_Head.transform.rotation.eulerAngles + "" + this.transform.rotation.eulerAngles);
                    this.transform.parent = C_Head.transform;
                    this.transform.localScale = new Vector3(1f, 1f, 1f);
                    this.gameObject.transform.position = C_Head.transform.position;


                    this.gameObject.GetComponent<MJ_Item>().enabled = false;
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");

                }
                else
                {
                    itemdata.equip[1]++;
                    for (int i = 0; i < myinven.invenitemname.Count; i++)
                    {
                        if (myinven.invenitemname[i] == "Glasses")
                        {
                            ItemContent.transform.Find("Glasses").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[1];
                        }
                    }
                    Destroy(this.gameObject);
                }
            }
            if (this.gameObject.name.Contains("Goat_Hoof"))
            {
                L_playerStat.myCharacterStat.WalkSpeed += 0.5f;
                if (itemdata.equip[2] == 0)
                {
                    itemdata.equip[2]++;
                    myinven.AddItem(2, "Goat_Hoof");
                    myinven.invenitemname.Add("Goat_Hoof");
                    this.transform.rotation = Quaternion.Euler(C_Foot.transform.rotation.eulerAngles.x, C_Foot.transform.rotation.eulerAngles.y, C_Foot.transform.rotation.eulerAngles.z); // 각도 조정
                    this.transform.parent = C_Foot.transform;
                    this.transform.localScale = new Vector3(1f, 1f, 1f);
                    this.gameObject.transform.position = C_Foot.transform.position;

                    this.gameObject.GetComponent<MJ_Item>().enabled = false;
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
                }
                else
                {
                    itemdata.equip[2]++;
                    for (int i = 0; i < myinven.invenitemname.Count; i++)
                    {
                        if (myinven.invenitemname[i] == "Goat_Hoof")
                        {
                            ItemContent.transform.Find("Goat_Hoof").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[2];
                        }
                    }
                    Destroy(this.gameObject);
                }
            }
            if (this.gameObject.name.Contains("Medkit"))
            {
                if (itemdata.equip[3] == 0)
                {
                    itemdata.equip[3]++;
                    myinven.AddItem(3, "Medkit");
                    myinven.invenitemname.Add("Medkit");
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    this.transform.parent = C_RLeg.transform;
                    this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    this.gameObject.transform.position = C_RLeg.transform.position + new Vector3(0.1f, 0f, 0f);

                    this.gameObject.GetComponent<MJ_Item>().enabled = false;
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");

                }
                else
                {
                    itemdata.equip[3]++;
                    for (int i = 0; i < myinven.invenitemname.Count; i++)
                    {
                        if (myinven.invenitemname[i] == "Medkit")
                        {
                            ItemContent.transform.Find("Medkit").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[3];
                        }
                    }
                    Destroy(this.gameObject);
                }
            }
            if (this.gameObject.name.Contains("Steak"))
            {

                L_playerStat.myCharacterStat.maxHP += 25.0f;
                if (itemdata.equip[4] == 0)
                {
                    itemdata.equip[4]++;
                    myinven.AddItem(4, "Steak");
                    myinven.invenitemname.Add("Steak");
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    this.transform.parent = C_Chest.transform;
                    this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    this.gameObject.transform.position = C_Chest.transform.position + new Vector3(0f, 0.2f, 0.2f);

                    this.gameObject.GetComponent<MJ_Item>().enabled = false;
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
                }
                else
                {
                    itemdata.equip[4]++;
                    for (int i = 0; i < myinven.invenitemname.Count; i++)
                    {
                        if (myinven.invenitemname[i] == "Steak")
                        {
                            ItemContent.transform.Find("Steak").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[4];
                        }
                    }
                    Destroy(this.gameObject);
                }
            }
            if (this.gameObject.name.Contains("SyringeCluster"))
            {

                //playerStat.myCharacterStat.AttackSpeed += SyringeClusterSpeed;
                if (itemdata.equip[5] == 0)
                {
                    itemdata.equip[5]++;
                    myinven.AddItem(5, "SyringeCluster");
                    myinven.invenitemname.Add("SyringeCluster");
                    this.transform.rotation = Quaternion.Euler(0, 180, -30);
                    this.transform.parent = C_LLeg.transform;
                    this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    this.gameObject.transform.position = C_LLeg.transform.position + new Vector3(-0.15f, 0, 0);

                    this.gameObject.GetComponent<MJ_Item>().enabled = false;
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
                }
                else
                {
                    itemdata.equip[5]++;
                    for (int i = 0; i < myinven.invenitemname.Count; i++)
                    {
                        if (myinven.invenitemname[i] == "SyringeCluster")
                        {
                            ItemContent.transform.Find("SyringeCluster").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[5];
                        }
                    }
                    Destroy(this.gameObject);
                }
            }


            if (this.gameObject.name.Contains("Hopoo_Feather"))
            {
                L_playerStat.myCharacterStat.JumpCount += 1;
                if (itemdata.equip[6] == 0)
                {
                    itemdata.equip[6]++;
                    myinven.AddItem(6, "Hopoo_Feather");
                    myinven.invenitemname.Add("Hopoo_Feather");
                    this.transform.rotation = Quaternion.Euler(-10, 180, -20);
                    this.transform.parent = C_LArm.transform;
                    this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    this.gameObject.transform.position = C_LArm.transform.position + new Vector3(-0.15f, 0, 0.1f); //위치 세부 조정

                    this.gameObject.GetComponent<MJ_Item>().enabled = false;
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
                }
                else
                {
                    itemdata.equip[6]++;
                    for (int i = 0; i < myinven.invenitemname.Count; i++)
                    {
                        if (myinven.invenitemname[i] == "Hopoo_Feather")
                        {
                            ItemContent.transform.Find("Hopoo_Feather").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[6];
                        }
                    }
                    Destroy(this.gameObject);
                }
            }

            if (this.gameObject.name.Contains("Seed"))
            {
                if (itemdata.equip[7] == 0)
                {
                    itemdata.equip[7]++;
                    myinven.AddItem(7, "Seed");
                    myinven.invenitemname.Add("Seed");
                    this.transform.rotation = Quaternion.Euler(0, 40, 0);
                    this.transform.parent = C_Plevis.transform;
                    this.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                    this.gameObject.transform.position = C_Plevis.transform.position + new Vector3(-0.2f, 0, 0); //위치 세부 조정

                    this.gameObject.GetComponent<MJ_Item>().enabled = false;
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
                }
                else
                {
                    itemdata.equip[7]++;
                    for (int i = 0; i < myinven.invenitemname.Count; i++)
                    {
                        if (myinven.invenitemname[i] == "Seed")
                        {
                            ItemContent.transform.Find("Seed").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[7];
                        }
                    }
                    Destroy(this.gameObject);
                }
            }

            if (this.gameObject.name.Contains("Titan_Knurl"))
            {
                L_playerStat.myCharacterStat.maxHP += 40.0f;
                L_playerStat.myCharacterStat.HP_Heal += 1.6f;

                if (itemdata.equip[8] == 0)
                {
                    itemdata.equip[8]++;
                    myinven.AddItem(8, "Titan_Knurl");
                    myinven.invenitemname.Add("Titan_Knurl");
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    this.transform.parent = C_Head.transform;
                    this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    this.gameObject.transform.position = C_Head.transform.position + new Vector3(0.2f, 0.03f, 0f);


                    this.gameObject.GetComponent<MJ_Item>().enabled = false;
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
                }
                else
                {
                    itemdata.equip[8]++;
                    for (int i = 0; i < myinven.invenitemname.Count; i++)
                    {
                        if (myinven.invenitemname[i] == "Titan_Knurl")
                        {
                            ItemContent.transform.Find("Titan_Knurl").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[8];
                        }
                    }
                    Destroy(this.gameObject);
                }
            }
            if (this.gameObject.name.Contains("LightningStrike"))
            {

                if (itemdata.equip[9] == 0)
                {
                    itemdata.equip[9]++;
                    myinven.AddItem(9, "LightningStrike");
                    myinven.invenitemname.Add("LightningStrike");
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    this.transform.parent = C_Chest.transform;
                    this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    this.gameObject.transform.position = C_Chest.transform.position; //위치 세부 조정

                    this.gameObject.GetComponent<MJ_Item>().enabled = false;
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
                }
                else
                {
                    itemdata.equip[9]++;
                    for (int i = 0; i < myinven.invenitemname.Count; i++)
                    {
                        if (myinven.invenitemname[i] == "LightningStrike")
                        {
                            ItemContent.transform.Find("LightningStrike").GetComponent<MJ_ItemCount>().Count.text = "x" + itemdata.equip[9];
                        }
                    }
                    Destroy(this.gameObject);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            if (player_Type == "Commando") C_Equip();
            else if (player_Type == "Loader") L_Equip();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MJ_Item : MonoBehaviour
{
    public LayerMask playerLayer; //아이템과 충돌처리해주기위한 레이어마스크

    KJH_Player C_playerStat; //코만도 스텟 받아오기 
    AttackSystem atk; // 코만도 공격력
    
    Loader L_playerStat;    //로더 스텟 받아오기
    public GameObject stat; //플레이어 존재 여부 체크

    public MJ_Inventory myinven;
    public MJ_ItemData itemdata;
    public Transform ItemContent;

    public Transform C_base;
    public Transform L_base;

    public Transform Head;   //아이템이 붙을 위치 
    public Transform RArm;
    public Transform LArm;
    public Transform RLeg;
    public Transform LLeg;
    public Transform Chest;
    public Transform Foot;
    public Transform Pelvis;

    public string player_Type;  //플레이어가 로더인지 코만도인지 확인

    void Start()
    {
        myinven = GameObject.Find("InGameUICanvas").GetComponent<MJ_Inventory>();
        itemdata = GameObject.Find("ItemDataBase").GetComponent<MJ_ItemData>();
        

        ItemContent = GameObject.Find("InGameUICanvas").transform.Find("Tab_InvenBase").transform.Find("Bottom").transform.Find("Content_Item").transform;

        stat = GameObject.Find("mdlCommandoDualies (merge)");
        if (stat != null)
        {
            C_base = stat.transform.Find("mdlCommandoDualies (merge)").Find("mdlCommandoDualies").Find("CommandoArmature").Find("ROOT").Find("base");
            C_playerStat = stat.GetComponent<KJH_Player>();
            atk = GameObject.Find("AttackSystem").GetComponent<AttackSystem>();
            player_Type = "Commando";

            Head = C_base.Find("stomach").Find("chest").Find("head").Find("head_end");      //GameObject.Find("head_end");
            RArm = C_base.Find("stomach").Find("chest").Find("upper_arm.r");          //GameObject.Find("upper_arm.r").transform;
            LArm = C_base.Find("stomach").Find("chest").Find("upper_arm.l");          //GameObject.Find("upper_arm.l").transform;
            RLeg = C_base.Find("pelvis").Find("thigh.r");                                                //GameObject.Find("thigh.r").transform;
            LLeg = C_base.Find("pelvis").Find("thigh.l");                                                //GameObject.Find("thigh.l").transform;
            Chest = C_base.Find("stomach").Find("chest");                                                //GameObject.Find("chest").transform;
            Foot = C_base.Find("pelvis").Find("thigh.r").Find("calf.r");                                 //GameObject.Find("calf.r").transform;
            Pelvis = C_base.Find("pelvis");                                                              //GameObject.Find("pelvis").transform;
        }
        if (stat == null)
        {
            stat = GameObject.Find("mdlLoader (merge)");
            L_base = stat.transform.Find("mdlLoader (merge)").Find("mdlLoader").Find("LoaderArmature").Find("ROOT").Find("base");
            L_playerStat = stat.GetComponent<Loader>();
            player_Type = "Loader";

            Head = L_base.Find("stomach").Find("chest").Find("neck").Find("head").Find("head_end");      //GameObject.Find("head_end");
            RArm = L_base.Find("stomach").Find("chest").Find("clavicle.r").Find("upper_arm.r");          //GameObject.Find("upper_arm.r").transform;
            LArm = L_base.Find("stomach").Find("chest").Find("clavicle.l").Find("upper_arm.l");          //GameObject.Find("upper_arm.l").transform;
            RLeg = L_base.Find("pelvis").Find("thigh.r");                                                //GameObject.Find("thigh.r").transform;
            LLeg = L_base.Find("pelvis").Find("thigh.l");                                                //GameObject.Find("thigh.l").transform;
            Chest = L_base.Find("stomach").Find("chest");                                                //GameObject.Find("chest").transform;
            Foot = L_base.Find("pelvis").Find("thigh.r").Find("calf.r");                                 //GameObject.Find("calf.r").transform;
            Pelvis = L_base.Find("pelvis");                                                              //GameObject.Find("pelvis").transform;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * 180.0f * Time.deltaTime);
    }


    #region commando_equip
    void C_Equip()
    {
        
        if (this.gameObject.name.Contains("Crowbar"))
        {
            atk.myBulletStat.BulletDamage += 2;
            if (itemdata.equip[0] == 0)
            {
                
                itemdata.equip[0]++;
                myinven.AddItem(0, "Crowbar");
                myinven.invenitemname.Add("Crowbar");

                this.transform.parent = Chest.transform;
                this.transform.rotation = Quaternion.Euler(Chest.transform.rotation.eulerAngles.x, Chest.transform.rotation.eulerAngles.y, Chest.transform.rotation.eulerAngles.z);
                this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                this.transform.localPosition = Chest.transform.localPosition + new Vector3(0.001f,0.002f,-0.002f); //위치 세부 조정


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
            

            if (itemdata.equip[1] == 0)
            {
                itemdata.equip[1]++;
                myinven.AddItem(1, "Glasses");
                myinven.invenitemname.Add("Glasses");
                this.transform.rotation = Quaternion.Euler(Head.transform.rotation.eulerAngles.x, Head.transform.rotation.eulerAngles.y , Head.transform.rotation.eulerAngles.z); // 10,118,-10

                this.transform.parent = Head.transform;
                this.transform.localScale = new Vector3(1f, 1f, 1f);
                this.gameObject.transform.localPosition = Head.transform.localPosition + new Vector3(0f, -0.003f, 0f);


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
            C_playerStat.myCharacterStat.RunSpeed += 0.5f;

            if (itemdata.equip[2] == 0)
            {
                itemdata.equip[2]++;
                myinven.AddItem(2, "Goat_Hoof");
                myinven.invenitemname.Add("Goat_Hoof");
                this.transform.localRotation = Quaternion.Euler(Foot.transform.rotation.eulerAngles.x, Foot.transform.rotation.eulerAngles.y, Foot.transform.rotation.eulerAngles.z); // 각도 조정
                this.transform.parent = Foot.transform ;
                this.transform.localScale = new Vector3(1.3f, 0.85f, 1.3f);
                this.gameObject.transform.localPosition = Foot.transform.localPosition + new Vector3(0.00035f,-0.0015f,-0.00065f);

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
            C_playerStat.myCharacterStat.HP_Heal += 1;
            if (itemdata.equip[3] == 0)
            {
                itemdata.equip[3]++;
                myinven.AddItem(3, "Medkit");
                myinven.invenitemname.Add("Medkit");
                this.transform.rotation = Quaternion.Euler(Pelvis.transform.rotation.eulerAngles.x, Pelvis.transform.rotation.eulerAngles.y, Pelvis.transform.rotation.eulerAngles.z);
                this.transform.parent = Pelvis.transform;
                this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                this.gameObject.transform.localPosition = Pelvis.transform.localPosition + new Vector3(0.002f, -0.0025f, 0f);

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
                this.transform.rotation = Quaternion.Euler(Chest.transform.rotation.eulerAngles.x, Chest.transform.rotation.eulerAngles.y, Chest.transform.rotation.eulerAngles.z);
                this.transform.parent = Chest.transform;
                this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                this.gameObject.transform.localPosition = Chest.transform.localPosition + new Vector3(0f,0.0005f,0.0023f);

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
            
            C_playerStat.myCharacterStat.AttackDelay -= 0.01f;
            if (C_playerStat.myCharacterStat.AttackDelay <= 0.05f) C_playerStat.myCharacterStat.AttackDelay = 0.05f;
            if (itemdata.equip[5] == 0)
            {
                itemdata.equip[5]++;
                myinven.AddItem(5, "SyringeCluster");
                myinven.invenitemname.Add("SyringeCluster");
                this.transform.rotation = Quaternion.Euler(LLeg.transform.rotation.eulerAngles.x, LLeg.transform.rotation.eulerAngles.y, LLeg.transform.rotation.eulerAngles.z);
                this.transform.parent = LLeg.transform;
                this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                this.gameObject.transform.localPosition = LLeg.transform.localPosition + new Vector3(0.002f,0,0);

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
            C_playerStat.myCharacterStat.JumpItem += 1;
            if (itemdata.equip[6] == 0)
            {
                itemdata.equip[6]++;
                myinven.AddItem(6, "Hopoo_Feather");
                myinven.invenitemname.Add("Hopoo_Feather");
                this.transform.rotation = Quaternion.Euler(Head.transform.rotation.eulerAngles.x, Head.transform.rotation.eulerAngles.y, Head.transform.rotation.eulerAngles.z);
                this.transform.parent = Head.transform;
                this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                this.gameObject.transform.localPosition = Head.transform.localPosition + new Vector3(0f , -0.001f , 0f); //위치 세부 조정

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
                this.transform.rotation = Quaternion.Euler(Pelvis.transform.rotation.eulerAngles.x, Pelvis.transform.rotation.eulerAngles.y, Pelvis.transform.rotation.eulerAngles.z);
                this.transform.parent = Pelvis.transform;
                this.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                this.gameObject.transform.localPosition = Pelvis.transform.localPosition + new Vector3(-0.0023f,-0.0025f,0); //위치 세부 조정

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
                this.transform.rotation = Quaternion.Euler(RArm.transform.rotation.eulerAngles.x, RArm.transform.rotation.eulerAngles.y, RArm.transform.rotation.eulerAngles.z);
                this.transform.parent = RArm.transform;
                this.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                this.gameObject.transform.localPosition = RArm.transform.localPosition + new Vector3(-0.0015f, 0.0032f, 0.002f);


                this.gameObject.GetComponent<MJ_Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
                this.gameObject.transform.Find("PickupKnurl").transform.Find("mdlKnurl").transform.Find("KnurlPebble").gameObject.SetActive(false);
                this.gameObject.transform.Find("PickupKnurl").transform.Find("mdlKnurl").transform.Find("KnurlPebble 1").gameObject.SetActive(false);
                this.gameObject.transform.Find("PickupKnurl").transform.Find("mdlKnurl").transform.Find("KnurlPebble 2").gameObject.SetActive(false);
                this.gameObject.transform.Find("PickupKnurl").transform.Find("mdlKnurl").transform.Find("KnurlPebble 3").gameObject.SetActive(false);

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
                this.transform.parent = Chest.transform;
                this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                this.gameObject.transform.position = Chest.transform.position; //위치 세부 조정

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

    #endregion
    //=================================================================================================

    #region loader_equip
    void L_Equip()
    {

        if (this.gameObject.name.Contains("Crowbar"))
        {
            
            if (itemdata.equip[0] == 0)
            {

                itemdata.equip[0]++;
                myinven.AddItem(0, "Crowbar");
                myinven.invenitemname.Add("Crowbar");

                this.transform.parent = Chest.transform;
                this.transform.rotation = Quaternion.Euler(Chest.transform.rotation.eulerAngles.x, Chest.transform.rotation.eulerAngles.y, Chest.transform.rotation.eulerAngles.z);
                this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                this.transform.localPosition = Chest.transform.localPosition + new Vector3(0.001f, 0.002f, -0.002f); //위치 세부 조정


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

            if (itemdata.equip[1] == 0)
            {
                itemdata.equip[1]++;
                myinven.AddItem(1, "Glasses");
                myinven.invenitemname.Add("Glasses");
                this.transform.rotation = Quaternion.Euler(Head.transform.rotation.eulerAngles.x, Head.transform.rotation.eulerAngles.y, Head.transform.rotation.eulerAngles.z); // 10,118,-10
                
                this.transform.parent = Head.transform;
                this.transform.localScale = new Vector3(1f, 1f, 1f);
                this.gameObject.transform.localPosition = Head.transform.localPosition + new Vector3(0f, -0.003f, 0f);


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
            L_playerStat.myCharacterStat.RunSpeed += 0.5f;

            if (itemdata.equip[2] == 0)
            {
                itemdata.equip[2]++;
                myinven.AddItem(2, "Goat_Hoof");
                myinven.invenitemname.Add("Goat_Hoof");
                this.transform.localRotation = Quaternion.Euler(Foot.transform.rotation.eulerAngles.x, Foot.transform.rotation.eulerAngles.y, Foot.transform.rotation.eulerAngles.z); // 각도 조정
                this.transform.parent = Foot.transform;
                this.transform.localScale = new Vector3(1.3f, 0.85f, 1.3f);
                this.gameObject.transform.localPosition = Foot.transform.localPosition + new Vector3(0.00035f, -0.0015f, -0.00065f);

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
            L_playerStat.myCharacterStat.HP_Heal += 1;
            if (itemdata.equip[3] == 0)
            {
                itemdata.equip[3]++;
                myinven.AddItem(3, "Medkit");
                myinven.invenitemname.Add("Medkit");
                this.transform.rotation = Quaternion.Euler(Pelvis.transform.rotation.eulerAngles.x, Pelvis.transform.rotation.eulerAngles.y, Pelvis.transform.rotation.eulerAngles.z);
                this.transform.parent = Pelvis.transform;
                this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                this.gameObject.transform.localPosition = Pelvis.transform.localPosition + new Vector3(0.002f, -0.0025f, 0f);

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
            if (itemdata.equip[4] >= 0)
            {
                itemdata.equip[4]++;
                myinven.AddItem(4, "Steak");
                myinven.invenitemname.Add("Steak");
                this.transform.rotation = Quaternion.Euler(Chest.transform.rotation.eulerAngles.x, Chest.transform.rotation.eulerAngles.y, Chest.transform.rotation.eulerAngles.z);
                this.transform.parent = Chest.transform;
                this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                this.gameObject.transform.localPosition = Chest.transform.localPosition + new Vector3(0f, 0.0005f, 0.0023f);

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

            L_playerStat.myCharacterStat.AttackDelay -= 0.01f;
            if (L_playerStat.myCharacterStat.AttackDelay <= 0.05f) L_playerStat.myCharacterStat.AttackDelay = 0.05f;
            if (itemdata.equip[5] == 0)
            {
                itemdata.equip[5]++;
                myinven.AddItem(5, "SyringeCluster");
                myinven.invenitemname.Add("SyringeCluster");
                this.transform.rotation = Quaternion.Euler(LLeg.transform.rotation.eulerAngles.x, LLeg.transform.rotation.eulerAngles.y, LLeg.transform.rotation.eulerAngles.z);
                this.transform.parent = LLeg.transform;
                this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                this.gameObject.transform.localPosition = LLeg.transform.localPosition + new Vector3(0.002f, 0, 0);

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
                this.transform.rotation = Quaternion.Euler(Head.transform.rotation.eulerAngles.x, Head.transform.rotation.eulerAngles.y, Head.transform.rotation.eulerAngles.z);
                this.transform.parent = Head.transform;
                this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                this.gameObject.transform.localPosition = Head.transform.localPosition + new Vector3(0f, -0.001f, 0f); //위치 세부 조정

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
                this.transform.rotation = Quaternion.Euler(Pelvis.transform.rotation.eulerAngles.x, Pelvis.transform.rotation.eulerAngles.y, Pelvis.transform.rotation.eulerAngles.z);
                this.transform.parent = Pelvis.transform;
                this.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                this.gameObject.transform.localPosition = Pelvis.transform.localPosition + new Vector3(-0.0023f, -0.0025f, 0); //위치 세부 조정

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
                this.transform.rotation = Quaternion.Euler(RArm.transform.rotation.eulerAngles.x, RArm.transform.rotation.eulerAngles.y, RArm.transform.rotation.eulerAngles.z);
                this.transform.parent = RArm.transform;
                this.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                this.gameObject.transform.localPosition = RArm.transform.localPosition + new Vector3(-0.0015f, 0.0032f, 0.002f);


                this.gameObject.GetComponent<MJ_Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
                this.gameObject.transform.Find("PickupKnurl").transform.Find("mdlKnurl").transform.Find("KnurlPebble").gameObject.SetActive(false);
                this.gameObject.transform.Find("PickupKnurl").transform.Find("mdlKnurl").transform.Find("KnurlPebble 1").gameObject.SetActive(false);
                this.gameObject.transform.Find("PickupKnurl").transform.Find("mdlKnurl").transform.Find("KnurlPebble 2").gameObject.SetActive(false);
                this.gameObject.transform.Find("PickupKnurl").transform.Find("mdlKnurl").transform.Find("KnurlPebble 3").gameObject.SetActive(false);

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
                this.transform.parent = Chest.transform;
                this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                this.gameObject.transform.position = Chest.transform.position; //위치 세부 조정

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

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            if (player_Type == "Commando") C_Equip();
            else if (player_Type == "Loader") L_Equip();
        }
    }


}

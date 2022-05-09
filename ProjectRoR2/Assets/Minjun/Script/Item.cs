using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject myPlayer;
    public LayerMask playerLayer;
    Player playerStat;
    float HoofSpeed;
    float SyringeClusterSpeed;
    void Start()
    {
        //myPlayer = GameObject.Find("mdlCommandoDualies");
        playerStat = GameObject.Find("mdlCommandoDualies").GetComponent<Player>(); //스탯을 받아주기 위해 
        SyringeClusterSpeed = playerStat.AttackSpeed * 0.15f;
        HoofSpeed = playerStat.MoveSpeed * 0.14f;
    }

    // Update is called once per frame
    void Update()
    {
       this.transform.Rotate(Vector3.up * 180.0f * Time.deltaTime);   
    }

    void Equip()
    {
        //1티어
        if (this.gameObject.name.Contains("Bullet"))
        {
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (오른쪽 허벅지)
        }
        if (this.gameObject.name.Contains("Crowbar"))
        {
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (등 오른쪽)
        }
        if (this.gameObject.name.Contains("Glasses"))
        {
            playerStat.critical += 0.1f;
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (얼굴)
        }
        if (this.gameObject.name.Contains("Goat_Hoof")) 
        {
            playerStat.MoveSpeed += HoofSpeed;
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (오른쪽 발)
        }
        if (this.gameObject.name.Contains("Medkit"))
        {
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (허리 오른쪽)
        }
        if (this.gameObject.name.Contains("Steak")) 
        {
            playerStat.Hp += 25.0f;
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (가슴)
        }
        if (this.gameObject.name.Contains("SyringeCluster"))
        {
            playerStat.AttackSpeed += SyringeClusterSpeed;
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기(오른쪽 허벅지)
        }

        //2티어
        if (this.gameObject.name.Contains("Guillotine"))
        {
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기  (등 가운데)
        }
        if (this.gameObject.name.Contains("Hopoo_Feather"))
        {
            playerStat.JumpCount += 1;
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (왼쪽 팔)
        }
        if (this.gameObject.name.Contains("MissileLauncher"))
        {
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (왼쪽 어꺠)
        }
        if (this.gameObject.name.Contains("Seed"))
        {
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (왼쪽 허리)
        }
        if (this.gameObject.name.Contains("Time_Bubble"))
        {
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (오른쪽 허벅지)
        }

        //보스유물
        if (this.gameObject.name.Contains("Titan_Knurl"))
        {
            int equip = 0;
            playerStat.Hp += 40.0f;
            playerStat.HpRecovery += 1.6f;
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (오른쪽 어깨)
            GameObject Head = GameObject.Find("head");
            if(equip == 0) 
            {
                equip++;
                this.transform.parent = Head.transform;
                this.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                this.gameObject.transform.position = Head.transform.position + new Vector3(0.145f, 0f, 0f);
                this.gameObject.GetComponent<Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
                
            }
            else
            {
                equip++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("LightningStrike"))
        {
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기(왼쪽 어깨)
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            //Destroy(this.gameObject);
            Equip();
        }
    }
}

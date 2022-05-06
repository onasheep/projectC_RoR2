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
        playerStat = GameObject.Find("mdlCommandoDualies").GetComponent<Player>();
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
            if (playerStat.eqiup[0] == 0)
            {
                playerStat.eqiup[0]++;
            }
            else
            {
                playerStat.eqiup[0]++;
                Destroy(this.gameObject);
            }
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (오른쪽 허벅지)
        }
        if (this.gameObject.name.Contains("Crowbar"))
        {
            if (playerStat.eqiup[1] == 0)
            {
                playerStat.eqiup[1]++;
            }
            else
            {
                playerStat.eqiup[1]++;
                Destroy(this.gameObject);
            }
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (등 오른쪽)
        }
        if (this.gameObject.name.Contains("Glasses"))
        {
            if (playerStat.eqiup[2] == 0)
            {
                playerStat.eqiup[2]++;
            }
            else
            {
                playerStat.eqiup[2]++;
                Destroy(this.gameObject);
            }
            playerStat.critical += 0.1f;
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (얼굴)
        }
        if (this.gameObject.name.Contains("Goat_Hoof")) 
        {
            playerStat.MoveSpeed += HoofSpeed;
            if (playerStat.eqiup[3] == 0)
            {
                playerStat.eqiup[3]++;
            }
            else
            {
                playerStat.eqiup[3]++;
                Destroy(this.gameObject);
            }
            
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (오른쪽 발)
        }
        if (this.gameObject.name.Contains("Medkit"))
        {
            if (playerStat.eqiup[4] == 0)
            {
                playerStat.eqiup[4]++;
            }
            else
            {
                playerStat.eqiup[4]++;
                Destroy(this.gameObject);
            }
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (허리 오른쪽)
        }
        if (this.gameObject.name.Contains("Steak")) 
        {
            if (playerStat.eqiup[5] == 0)
            {
                playerStat.eqiup[5]++;
            }
            else
            {
                playerStat.eqiup[5]++;
                Destroy(this.gameObject);
            }
            playerStat.Hp += 25.0f;
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (가슴)
        }
        if (this.gameObject.name.Contains("SyringeCluster"))
        {
            if (playerStat.eqiup[6] == 0)
            {
                playerStat.eqiup[6]++;
            }
            else
            {
                playerStat.eqiup[6]++;
                Destroy(this.gameObject);
            }
            playerStat.AttackSpeed += SyringeClusterSpeed;
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기(오른쪽 허벅지)
        }

        //2티어
        if (this.gameObject.name.Contains("Guillotine"))
        {
            if (playerStat.eqiup[7] == 0)
            {
                playerStat.eqiup[7]++;
            }
            else
            {
                playerStat.eqiup[7]++;
                Destroy(this.gameObject);
            }
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기  (등 가운데)
        }
        if (this.gameObject.name.Contains("Hopoo_Feather"))
        {
            playerStat.JumpCount += 1;
            if (playerStat.eqiup[8] == 0)
            {
                playerStat.eqiup[8]++;
            }
            else
            {
                playerStat.eqiup[8]++;
                Destroy(this.gameObject);
            }
            
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (왼쪽 팔)
        }
        if (this.gameObject.name.Contains("MissileLauncher"))
        {
            if (playerStat.eqiup[9] == 0)
            {
                playerStat.eqiup[9]++;
            }
            else
            {
                playerStat.eqiup[9]++;
                Destroy(this.gameObject);
            }
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (왼쪽 어꺠)
        }
        if (this.gameObject.name.Contains("Seed"))
        {
            if (playerStat.eqiup[10] == 0)
            {
                playerStat.eqiup[10]++;
            }
            else
            {
                playerStat.eqiup[10]++;
                Destroy(this.gameObject);
            }
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (왼쪽 허리)
        }
        if (this.gameObject.name.Contains("Time_Bubble"))
        {
            if (playerStat.eqiup[11] == 0)
            {
                playerStat.eqiup[11]++;
            }
            else
            {
                playerStat.eqiup[11]++;
                Destroy(this.gameObject);
            }
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
            if(playerStat.eqiup[12] == 0) 
            {
                playerStat.eqiup[12]++;
                this.transform.parent = Head.transform;
                this.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                this.gameObject.transform.position = Head.transform.position + new Vector3(0.145f, 0f, 0f);
                this.gameObject.GetComponent<Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
                
            }
            else
            {
                playerStat.eqiup[12]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("LightningStrike"))
        {
            if(playerStat.eqiup[13] == 0)
            {
                playerStat.eqiup[13]++;
            }
            else
            {
                playerStat.eqiup[13]++;
                Destroy(this.gameObject);
            }
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

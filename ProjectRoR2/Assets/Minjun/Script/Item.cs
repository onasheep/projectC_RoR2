using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject myPlayer;
    public LayerMask playerLayer;
    Player playerStat;
    float HoofWalkSpeed;
    float HoofRunSpeed;
    float SyringeClusterSpeed;
    void Start()
    {
        playerStat = GameObject.Find("mdlCommandoDualies").GetComponent<Player>(); //스탯을 받아주기 위해 
        SyringeClusterSpeed = playerStat.myCharacterStat.AttackSpeed * 0.15f;
        HoofWalkSpeed = playerStat.myCharacterStat.WalkSpeed * 0.14f;
        HoofRunSpeed = playerStat.myCharacterStat.RunSpeed * 0.14f;
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
            //보스에게 20%(중첩당 +20%)의 피해를 추가로 입힙니다.[합연산]
            if (playerStat.equip[0] == 0)
            {
                playerStat.equip[0]++;
                //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (오른쪽 허벅지)
            }
            else
            {
                playerStat.equip[0]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Crowbar"))
        {
            //체력이 90%를 넘는 적에게 75%(중첩당 +75%)의 피해를 입힙니다.[합연산]

            if (playerStat.equip[1] == 0)
            {
                playerStat.equip[1]++;
                //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (등 오른쪽)
            }
            else
            {
                playerStat.equip[1]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Glasses"))
        {
            //공격 시에 10 % (중첩당 + 10 %) 확률로 '치명타'가 발생하여, 두 배의 피해를 입힙니다.[합연산]
            //playerStat.myCharacterStat.critical += 0.1f;  크리티컬 미구현
            
            if (playerStat.equip[2] == 0)
            {
                playerStat.equip[2]++;
                //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (얼굴)
            }
            else
            {
                playerStat.equip[2]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Goat_Hoof")) 
        {
            //이동 속도가 14%(중첩당 +14%) 증가합니다.[합연산]
            playerStat.myCharacterStat.WalkSpeed += HoofWalkSpeed;
            playerStat.myCharacterStat.RunSpeed += HoofRunSpeed;

            if (playerStat.equip[3] == 0)
            {
                playerStat.equip[3]++;
                //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (오른쪽 발)
            }
            else
            {
                playerStat.equip[3]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Medkit"))
        {
            //피해를 받으면 2초 후 체력이 20 치유되고, 추가로 최대 체력의 5%(중첩당 +5%)가 치유됩니다.[합연산]

            if (playerStat.equip[4] == 0)
            {
                playerStat.equip[4]++;
                //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (허리 오른쪽)
            }
            else
            {
                playerStat.equip[4]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Steak")) 
        {
            //최대 체력이 25(중첩당 + 25) 증가합니다.[합연산]
            playerStat.myCharacterStat.Hp += 25.0f;

            if (playerStat.equip[5] == 0)
            {
                playerStat.equip[5]++;
                //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (가슴)
            }
            else
            {
                playerStat.equip[5]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("SyringeCluster"))
        {
            //공격 속도가 15 % (중첩당 + 15 %) 증가합니다.[합연산]
            playerStat.myCharacterStat.AttackSpeed += SyringeClusterSpeed;

            if (playerStat.equip[6] == 0)
            {
                playerStat.equip[6]++;
                //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기(오른쪽 허벅지)
            }
            else
            {
                playerStat.equip[6]++;
                Destroy(this.gameObject);
            }
        }

        //2티어
        if (this.gameObject.name.Contains("Guillotine"))
        {
            //체력이 13%(중첩당 +13%) 이하인 엘리트 몬스터들을 즉시 처치합니다.[곱연산][ 계산식 : 1 - 1 / (1 + 0.13 * 단두대 개수) ]

            if (playerStat.equip[7] == 0)
            {
                playerStat.equip[7]++;
                //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기  (등 가운데)
            }
            else
            {
                playerStat.equip[7]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Hopoo_Feather"))
        {
            //최대 점프 횟수가 1(중첩당 +1)회 증가합니다.[합연산]
            playerStat.myCharacterStat.JumpCount += 1;

            if (playerStat.equip[8] == 0)
            {
                playerStat.equip[8]++;
                //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (왼쪽 팔)
            }
            else
            {
                playerStat.equip[8]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("MissileLauncher"))
        {
            //10% 확률로 미사일을 발사하여 총 300%(중첩당 +300%)의 피해를 입힙니다.[합연산]

            if (playerStat.equip[9] == 0)
            {
                playerStat.equip[9]++;
                //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (왼쪽 어꺠)
            }
            else
            {
                playerStat.equip[9]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Seed"))
        {
            //피해를 입히면 1(중첩당 +1)의 체력이 치유됩니다.[합연산]

            if (playerStat.equip[10] == 0)
            {
                playerStat.equip[10]++;
                //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (왼쪽 허리)
            }
            else
            {
                playerStat.equip[10]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Time_Bubble"))
        {
            //공격 명중 시 그 적을 느리게 하여, 2초(중첩당 +2초) 동안 이동 속도를 60% 감소시킵니다.[합연산]

            if (playerStat.equip[11] == 0)
            {
                playerStat.equip[11]++;
                //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (오른쪽 허벅지)
            }
            else
            {
                playerStat.equip[11]++;
                Destroy(this.gameObject);
            }
        }

        //보스유물
        if (this.gameObject.name.Contains("Titan_Knurl"))
        {
            playerStat.myCharacterStat.Hp += 40.0f;
            playerStat.myCharacterStat.Hp_Heal += 1.6f;
            //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기 (오른쪽 어깨)
            GameObject Head = GameObject.Find("head");
            if(playerStat.equip[12]== 0) 
            {
                playerStat.equip[12]++;
                this.transform.parent = Head.transform;
                this.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                this.gameObject.transform.position = Head.transform.position + new Vector3(0.145f, 0f, 0f);
                this.gameObject.GetComponent<Item>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");
                
            }
            else
            {
                playerStat.equip[12]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("LightningStrike"))
        {
            //공격이 명중하면 10% 확률로 벼락이 쳐서 500%(중첩당 +500%)피해를 입힙니다.[합연산]
            if (playerStat.equip[13] == 0)
            {
                playerStat.equip[13]++;
                //아이템 먹으면 플레이어에게 붙게끔 위치 잡아주기(왼쪽 어깨)
            }
            else
            {
                playerStat.equip[13]++;
                Destroy(this.gameObject);
            }
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

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
        //1Ƽ��
        if (this.gameObject.name.Contains("Bullet"))
        {
            playerStat.eqiup[0]++;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (������ �����)
        }
        if (this.gameObject.name.Contains("Crowbar"))
        {
            playerStat.eqiup[1]++;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (�� ������)
        }
        if (this.gameObject.name.Contains("Glasses"))
        {
            playerStat.eqiup[2]++;
            playerStat.critical += 0.1f;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (��)
        }
        if (this.gameObject.name.Contains("Goat_Hoof")) 
        {
            playerStat.eqiup[3]++;
            playerStat.MoveSpeed += HoofSpeed;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (������ ��)
        }
        if (this.gameObject.name.Contains("Medkit"))
        {
            playerStat.eqiup[4]++;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (�㸮 ������)
        }
        if (this.gameObject.name.Contains("Steak")) 
        {
            playerStat.eqiup[5]++;
            playerStat.Hp += 25.0f;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (����)
        }
        if (this.gameObject.name.Contains("SyringeCluster"))
        {
            playerStat.eqiup[6]++;
            playerStat.AttackSpeed += SyringeClusterSpeed;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ�(������ �����)
        }

        //2Ƽ��
        if (this.gameObject.name.Contains("Guillotine"))
        {
            playerStat.eqiup[7]++;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ�  (�� ���)
        }
        if (this.gameObject.name.Contains("Hopoo_Feather"))
        {
            playerStat.eqiup[8]++;
            playerStat.JumpCount += 1;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (���� ��)
        }
        if (this.gameObject.name.Contains("MissileLauncher"))
        {
            playerStat.eqiup[9]++;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (���� ���)
        }
        if (this.gameObject.name.Contains("Seed"))
        {
            playerStat.eqiup[10]++;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (���� �㸮)
        }
        if (this.gameObject.name.Contains("Time_Bubble"))
        {
            playerStat.eqiup[11]++;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (������ �����)
        }

        //��������
        if (this.gameObject.name.Contains("Titan_Knurl"))
        {
            int equip = 0;
            playerStat.Hp += 40.0f;
            playerStat.HpRecovery += 1.6f;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (������ ���)
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
            playerStat.eqiup[13]++;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ�(���� ���)
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

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
        playerStat = GameObject.Find("mdlCommandoDualies").GetComponent<Player>(); //������ �޾��ֱ� ���� 
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
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (������ �����)
        }
        if (this.gameObject.name.Contains("Crowbar"))
        {
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (�� ������)
        }
        if (this.gameObject.name.Contains("Glasses"))
        {
            playerStat.critical += 0.1f;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (��)
        }
        if (this.gameObject.name.Contains("Goat_Hoof")) 
        {
            playerStat.MoveSpeed += HoofSpeed;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (������ ��)
        }
        if (this.gameObject.name.Contains("Medkit"))
        {
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (�㸮 ������)
        }
        if (this.gameObject.name.Contains("Steak")) 
        {
            playerStat.Hp += 25.0f;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (����)
        }
        if (this.gameObject.name.Contains("SyringeCluster"))
        {
            playerStat.AttackSpeed += SyringeClusterSpeed;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ�(������ �����)
        }

        //2Ƽ��
        if (this.gameObject.name.Contains("Guillotine"))
        {
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ�  (�� ���)
        }
        if (this.gameObject.name.Contains("Hopoo_Feather"))
        {
            playerStat.JumpCount += 1;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (���� ��)
        }
        if (this.gameObject.name.Contains("MissileLauncher"))
        {
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (���� ���)
        }
        if (this.gameObject.name.Contains("Seed"))
        {
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (���� �㸮)
        }
        if (this.gameObject.name.Contains("Time_Bubble"))
        {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{/*
    public GameObject myPlayer;
    public LayerMask playerLayer;
    JMJ.Player playerStat;
    float HoofSpeed;
    float SyringeClusterSpeed;
    void Start()
    {
        //myPlayer = GameObject.Find("mdlCommandoDualies");
        playerStat = GameObject.Find("mdlCommandoDualies").GetComponent<JMJ.Player>();
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
            //�������� 20%(��ø�� +20%)�� ���ظ� �߰��� �����ϴ�.[�տ���]
            if (playerStat.equip[0] == 0)
            {
                playerStat.equip[0]++;
                //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (������ �����)
            }
            else
            {
                playerStat.equip[0]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Crowbar"))
        {
            //ü���� 90%�� �Ѵ� ������ 75%(��ø�� +75%)�� ���ظ� �����ϴ�.[�տ���]

            if (playerStat.equip[1] == 0)
            {
                playerStat.equip[1]++;
                //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (�� ������)
            }
            else
            {
                playerStat.equip[1]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Glasses"))
        {
            //���� �ÿ� 10 % (��ø�� + 10 %) Ȯ���� 'ġ��Ÿ'�� �߻��Ͽ�, �� ���� ���ظ� �����ϴ�.[�տ���]
            //playerStat.myCharacterStat.critical += 0.1f;  ũ��Ƽ�� �̱���
            
            if (playerStat.equip[2] == 0)
            {
                playerStat.equip[2]++;
                //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (��)
            }
            else
            {
                playerStat.equip[2]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Goat_Hoof")) 
        {
            //�̵� �ӵ��� 14%(��ø�� +14%) �����մϴ�.[�տ���]
            playerStat.myCharacterStat.WalkSpeed += HoofWalkSpeed;
            playerStat.myCharacterStat.RunSpeed += HoofRunSpeed;
            if (playerStat.equip[3] == 0)
            {
                playerStat.equip[3]++;
                //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (������ ��)
            }
            else
            {
                playerStat.equip[3]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Medkit"))
        {
            //���ظ� ������ 2�� �� ü���� 20 ġ���ǰ�, �߰��� �ִ� ü���� 5%(��ø�� +5%)�� ġ���˴ϴ�.[�տ���]
            if (playerStat.equip[4] == 0)
            {
                playerStat.equip[4]++;
                //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (�㸮 ������)
            }
            else
            {
                playerStat.equip[4]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Steak")) 
        {
            //�ִ� ü���� 25(��ø�� + 25) �����մϴ�.[�տ���]
            playerStat.myCharacterStat.Hp += 25.0f;
            if (playerStat.equip[5] == 0)
            {
                playerStat.equip[5]++;
                //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (����)
            }
            else
            {
                playerStat.equip[5]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("SyringeCluster"))
        {
            //���� �ӵ��� 15 % (��ø�� + 15 %) �����մϴ�.[�տ���]
            playerStat.myCharacterStat.AttackSpeed += SyringeClusterSpeed;
            if (playerStat.equip[6] == 0)
            {
                playerStat.equip[6]++;
                //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ�(������ �����)
            }
            else
            {
                playerStat.equip[6]++;
                Destroy(this.gameObject);
            }
        }

        //2Ƽ��
        if (this.gameObject.name.Contains("Guillotine"))
        {
            //ü���� 13%(��ø�� +13%) ������ ����Ʈ ���͵��� ��� óġ�մϴ�.[������][ ���� : 1 - 1 / (1 + 0.13 * �ܵδ� ����) ]
            if (playerStat.equip[7] == 0)
            {
                playerStat.equip[7]++;
                //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ�  (�� ���)
            }
            else
            {
                playerStat.equip[7]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Hopoo_Feather"))
        {
            //�ִ� ���� Ƚ���� 1(��ø�� +1)ȸ �����մϴ�.[�տ���]
            playerStat.myCharacterStat.JumpCount += 1;
            if (playerStat.equip[8] == 0)
            {
                playerStat.equip[8]++;
                //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (���� ��)
            }
            else
            {
                playerStat.equip[8]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("MissileLauncher"))
        {
            //10% Ȯ���� �̻����� �߻��Ͽ� �� 300%(��ø�� +300%)�� ���ظ� �����ϴ�.[�տ���]

            if (playerStat.equip[9] == 0)
            {
                playerStat.equip[9]++;
                //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (���� ���)
            }
            else
            {
                playerStat.equip[9]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Seed"))
        {
            //���ظ� ������ 1(��ø�� +1)�� ü���� ġ���˴ϴ�.[�տ���]

            if (playerStat.equip[10] == 0)
            {
                playerStat.equip[10]++;
                //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (���� �㸮)
            }
            else
            {
                playerStat.equip[10]++;
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.name.Contains("Time_Bubble"))
        {
            //���� ���� �� �� ���� ������ �Ͽ�, 2��(��ø�� +2��) ���� �̵� �ӵ��� 60% ���ҽ�ŵ�ϴ�.[�տ���]

            if (playerStat.equip[11] == 0)
            {
                playerStat.equip[11]++;
                //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (������ �����)
            }
            else
            {
                playerStat.equip[11]++;
                Destroy(this.gameObject);
            }
        }

        //��������
        if (this.gameObject.name.Contains("Titan_Knurl"))
        {
            playerStat.myCharacterStat.Hp += 40.0f;
            playerStat.myCharacterStat.Hp_Heal += 1.6f;
            //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ� (������ ���)
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
            //������ �����ϸ� 10% Ȯ���� ������ �ļ� 500%(��ø�� +500%)���ظ� �����ϴ�.[�տ���]
            if (playerStat.equip[13] == 0)
            {
                playerStat.equip[13]++;
                //������ ������ �÷��̾�� �ٰԲ� ��ġ ����ֱ�(���� ���)
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
    */
}

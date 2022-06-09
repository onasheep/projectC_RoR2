using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEvent : UnityEngine.Events.UnityEvent<float, float> { }

public class AttackSystem : MonoBehaviour
{
    private Camera myCamera;
    public HPEvent onHPEvent = new HPEvent();
    public KJH_BulletStat myBulletStat;    
    public KJH_CharacterStat myCharacterStat;
    GameObject myplayer = null;
    [SerializeField]
    private AudioClip LMBSound;
    [SerializeField]
    private AudioClip RMBSound;
    public ParticleSystem muzzleFlashR;
    public ParticleSystem muzzleFlashL;
    public GameObject effectSource0 = null;
    public GameObject effectSource1 = null;
    private AudioSource audioSource;
    public LayerMask DamageMask;

    public MJ_ItemData MJ_It;
    public KJH_Player status;
    // Start is called before the first frame update
    private void Awake()
    {
        //myplayer = GetComponent<PlayerSpawner>().player;
        //myCharacterStat = myplayer.GetComponent<KJH_Player>().myCharacterStat;
        //myCharacterStat.curHP = myCharacterStat.maxHP;
        
    }

    public void SetPlayer(GameObject player)
    {
        myplayer = player;
        myCharacterStat = myplayer.GetComponentInChildren<KJH_Player>().myCharacterStat;
        
    }
    
    void Start()
    {
        myCharacterStat.curHP = myCharacterStat.maxHP;
        myCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();

        MJ_It = GameObject.Find("ItemDataBase").GetComponent<MJ_ItemData>();
        status = GameObject.Find("mdlCommandoDualies (merge)").GetComponent<KJH_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�Ѿ˱���
    public void ShotBullet(Vector3 attackDir, string BulletName, Transform bulletSpawnPoint, float attackRange)
    {
        GameObject intantBullet = Instantiate(Resources.Load("Prefeb/"+ BulletName), bulletSpawnPoint.position, Quaternion.identity) as GameObject;
        intantBullet.GetComponent<Bullet>().Shotting(attackDir, attackRange);
        Debug.Log("shot");
    }
    //ī�޶�Ray �ѱ� Ray ���
    public void TwoStepRaycast(Transform bulletSpawnPoint, string BulletName)
    {
        Ray ray;
        RaycastHit hit;
        Vector3 targetPoint = Vector3.zero;
        //ī�޶� ȭ�� �߾����� Ray�߻�
        ray = myCamera.ViewportPointToRay(Vector2.one * 0.5f);
        //���ݻ�Ÿ� �ȿ� �ε����� ��
        if (Physics.Raycast(ray, out hit, myBulletStat.BulletRange, DamageMask))
        {           
            targetPoint = hit.point;          
        }
        //���ݻ�Ÿ� �ȿ� �ε����°��� ������ �ִ� ��Ÿ�
        else
        {
            targetPoint = ray.origin + ray.direction * myBulletStat.BulletRange;
        }
        //Debug.DrawRay(ray.origin, ray.direction * myBulletStat.BulletRange, Color.red);

        //���ݻ�Ÿ��ȿ� Ÿ�ݵ� ��ǥ�� �ѱ� ��ǥ�� �������� ���
        Vector3 attackDirection = targetPoint - bulletSpawnPoint.position;
        //������ �Ÿ��� ���
        float attackRange = attackDirection.magnitude;
        attackDirection.Normalize();
        //�ѱ���ǥ���� Ÿ����ǥ���� Ray�� ����Ͽ� Aim ��Ȯ�� ���
        if (Physics.Raycast(bulletSpawnPoint.position, attackDirection, out hit, myBulletStat.BulletRange))
        {
            //Ÿ������Ʈ
            HitEffect(hit, BulletName);
            //�� �߻�
            
            //OnDamage
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Lemurian"))
            {
               //hit.transform.GetComponent<Lemurian>().OnDamagekkj(myBulletStat.BulletDamage);
            }
            // �������� ���ݽ�
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Beetle"))
            {
                hit.transform.GetComponent<Beetle>().HJSGetDamage(myBulletStat.BulletDamage);
                Debug.Log("������������ " + myBulletStat.BulletDamage + "��ŭ�� ������");
            }
            // �θ���� ���ݽ�
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Parent"))
            {
                //�Ȱ� ���� ���� ����
                if (MJ_It.equip[1] == 0 && MJ_It.equip[7] == 0)
                {
                    Debug.Log("�Ȱ� ���� ���� ����");
                    hit.transform.GetComponent<Parent>().HJSGetDamage(myBulletStat.BulletDamage);
                    Debug.Log("�θ���� ���ݽ�" + myBulletStat.BulletDamage + "��ŭ�� ������");
                }
                //�Ȱ� ���� ���� ����
                if (MJ_It.equip[1] == 0 && MJ_It.equip[7] != 0)
                {
                    Debug.Log("�Ȱ� ���� ���� ����");
                    hit.transform.GetComponent<Parent>().HJSGetDamage(myBulletStat.BulletDamage);
                    Debug.Log("�θ���� ���ݽ�" + myBulletStat.BulletDamage + "��ŭ�� ������");
                    status.myCharacterStat.curHP += 10 * MJ_It.equip[7];
                    if (status.myCharacterStat.curHP >= status.myCharacterStat.maxHP) status.myCharacterStat.curHP = status.myCharacterStat.maxHP;
                    Debug.Log("myCharacterStat.curHP=" + status.myCharacterStat.curHP);
                }
                //�Ȱ� ����
                if (MJ_It.equip[1] != 0)
                {
                    int rnd = Random.Range(0, 10);
                    //ũ�� �߻�
                    if (rnd <= MJ_It.equip[1])
                    {
                        //���� ����
                        if (MJ_It.equip[7] == 0)
                        {
                            Debug.Log("�Ȱ� �ְ� ���� ���� ũ��Ƽ��");
                            hit.transform.GetComponent<Parent>().HJSGetDamage(myBulletStat.BulletDamage * 2);
                            Debug.Log("�θ���� ���ݽ�" + myBulletStat.BulletDamage * 2 + "��ŭ�� ������");
                        }
                        //���� ����
                        else
                        {
                            Debug.Log("�Ȱ� �ְ� ���� ���� ũ��Ƽ��");
                            hit.transform.GetComponent<Parent>().HJSGetDamage(myBulletStat.BulletDamage * 2);
                            Debug.Log("�θ���� ���ݽ�" + myBulletStat.BulletDamage * 2 + "��ŭ�� ������");
                            status.myCharacterStat.curHP += 10 * MJ_It.equip[7];
                            if (status.myCharacterStat.curHP >= status.myCharacterStat.maxHP) status.myCharacterStat.curHP = status.myCharacterStat.maxHP;
                            Debug.Log("10ȸ��");
                        }

                    }
                    //ũ�� ������
                    if (rnd > MJ_It.equip[1])
                    {
                        //���� ����
                        if (MJ_It.equip[7] == 0)
                        {
                            Debug.Log("�Ȱ� �ְ� ���� ���� ũ�� ������");
                            hit.transform.GetComponent<Parent>().HJSGetDamage(myBulletStat.BulletDamage);
                            Debug.Log("�θ���� ���ݽ�" + myBulletStat.BulletDamage + "��ŭ�� ������");
                        }
                        //���� ����
                        else
                        {
                            Debug.Log("�Ȱ� �ְ� ���� ���� ũ�� ������");
                            hit.transform.GetComponent<Parent>().HJSGetDamage(myBulletStat.BulletDamage);
                            Debug.Log("�θ���� ���ݽ�" + myBulletStat.BulletDamage + "��ŭ�� ������");
                            status.myCharacterStat.curHP += 10 * MJ_It.equip[7];
                            if (status.myCharacterStat.curHP >= status.myCharacterStat.maxHP) status.myCharacterStat.curHP = status.myCharacterStat.maxHP;
                            Debug.Log("10ȸ��");
                        }
                    }
                }
            }
        }
        ShotBullet(attackDirection, BulletName, bulletSpawnPoint, attackRange);
        GunSound(BulletName, bulletSpawnPoint);      
        //Debug.DrawRay(bulletSpawnPoint.position, attackDirection * myBulletStat.BulletRange, Color.blue);
    }
    //�ǰ� ����Ʈ
    public void HitEffect(RaycastHit hit, string BulletName)
    {
        if (BulletName == "BulletMouse0")
        {
            Instantiate(effectSource0, hit.point, Quaternion.identity);
        }
        else if (BulletName == "BulletMouse1")
        {
            Instantiate(effectSource1, hit.point, Quaternion.identity);
        }
    }
    //�ѼҸ� ����
    public void GunSound(string BulletName, Transform bulletSpawnPoint)
    {
        if (BulletName == "BulletMouse0")
        {
            if (bulletSpawnPoint.name == "ShotPosR")
            {
                PlayShot(LMBSound, muzzleFlashR);
            }
            else if (bulletSpawnPoint.name == "ShotPosL")
            {
                PlayShot(LMBSound, muzzleFlashL);
            }
        }
        else
        {
            PlayShot(RMBSound);
        }
    }
    //�ѼҸ� �� ���� ���� 
    public void PlayShot(AudioClip _clip, ParticleSystem _ps = null)
    {
        audioSource.clip = _clip;
        audioSource.volume = 0.08f;
        audioSource.Play();
        if (_ps != null)
        {
            _ps.Play();
        }
    }

    public bool DecreaseHP(float damage)
    {
        float previousHP = myCharacterStat.curHP;
        myCharacterStat.curHP = myCharacterStat.curHP - damage > 0 ? myCharacterStat.curHP - damage : 0;
        onHPEvent.Invoke(previousHP, myCharacterStat.curHP);
        if (myCharacterStat.curHP == 0)
        {
            return true;
        }
        return false;
    }
}

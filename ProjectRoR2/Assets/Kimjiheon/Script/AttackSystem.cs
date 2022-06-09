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
    //총알구현
    public void ShotBullet(Vector3 attackDir, string BulletName, Transform bulletSpawnPoint, float attackRange)
    {
        GameObject intantBullet = Instantiate(Resources.Load("Prefeb/"+ BulletName), bulletSpawnPoint.position, Quaternion.identity) as GameObject;
        intantBullet.GetComponent<Bullet>().Shotting(attackDir, attackRange);
        Debug.Log("shot");
    }
    //카메라Ray 총구 Ray 계산
    public void TwoStepRaycast(Transform bulletSpawnPoint, string BulletName)
    {
        Ray ray;
        RaycastHit hit;
        Vector3 targetPoint = Vector3.zero;
        //카메라 화면 중앙으로 Ray발사
        ray = myCamera.ViewportPointToRay(Vector2.one * 0.5f);
        //공격사거리 안에 부딪히는 곳
        if (Physics.Raycast(ray, out hit, myBulletStat.BulletRange, DamageMask))
        {           
            targetPoint = hit.point;          
        }
        //공격사거리 안에 부딪히는곳이 없을때 최대 사거리
        else
        {
            targetPoint = ray.origin + ray.direction * myBulletStat.BulletRange;
        }
        //Debug.DrawRay(ray.origin, ray.direction * myBulletStat.BulletRange, Color.red);

        //공격사거리안에 타격된 좌표와 총구 좌표를 방향으로 계산
        Vector3 attackDirection = targetPoint - bulletSpawnPoint.position;
        //방향을 거리로 계산
        float attackRange = attackDirection.magnitude;
        attackDirection.Normalize();
        //총구좌표에서 타격좌표까지 Ray를 사용하여 Aim 정확도 계산
        if (Physics.Raycast(bulletSpawnPoint.position, attackDirection, out hit, myBulletStat.BulletRange))
        {
            //타격이펙트
            HitEffect(hit, BulletName);
            //총 발사
            
            //OnDamage
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Lemurian"))
            {
               //hit.transform.GetComponent<Lemurian>().OnDamagekkj(myBulletStat.BulletDamage);
            }
            // 딱정벌레 공격시
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Beetle"))
            {
                hit.transform.GetComponent<Beetle>().HJSGetDamage(myBulletStat.BulletDamage);
                Debug.Log("딱정벌레에게 " + myBulletStat.BulletDamage + "만큼의 데미지");
            }
            // 부모몬스터 공격시
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Parent"))
            {
                //안경 없고 씨앗 없음
                if (MJ_It.equip[1] == 0 && MJ_It.equip[7] == 0)
                {
                    Debug.Log("안경 없고 씨앗 없음");
                    hit.transform.GetComponent<Parent>().HJSGetDamage(myBulletStat.BulletDamage);
                    Debug.Log("부모몬스터 공격시" + myBulletStat.BulletDamage + "만큼의 데미지");
                }
                //안경 없고 씨앗 있음
                if (MJ_It.equip[1] == 0 && MJ_It.equip[7] != 0)
                {
                    Debug.Log("안경 없고 씨앗 있음");
                    hit.transform.GetComponent<Parent>().HJSGetDamage(myBulletStat.BulletDamage);
                    Debug.Log("부모몬스터 공격시" + myBulletStat.BulletDamage + "만큼의 데미지");
                    status.myCharacterStat.curHP += 10 * MJ_It.equip[7];
                    if (status.myCharacterStat.curHP >= status.myCharacterStat.maxHP) status.myCharacterStat.curHP = status.myCharacterStat.maxHP;
                    Debug.Log("myCharacterStat.curHP=" + status.myCharacterStat.curHP);
                }
                //안경 있음
                if (MJ_It.equip[1] != 0)
                {
                    int rnd = Random.Range(0, 10);
                    //크리 발생
                    if (rnd <= MJ_It.equip[1])
                    {
                        //씨앗 없음
                        if (MJ_It.equip[7] == 0)
                        {
                            Debug.Log("안경 있고 씨앗 없음 크리티컬");
                            hit.transform.GetComponent<Parent>().HJSGetDamage(myBulletStat.BulletDamage * 2);
                            Debug.Log("부모몬스터 공격시" + myBulletStat.BulletDamage * 2 + "만큼의 데미지");
                        }
                        //씨앗 있음
                        else
                        {
                            Debug.Log("안경 있고 씨앗 없음 크리티컬");
                            hit.transform.GetComponent<Parent>().HJSGetDamage(myBulletStat.BulletDamage * 2);
                            Debug.Log("부모몬스터 공격시" + myBulletStat.BulletDamage * 2 + "만큼의 데미지");
                            status.myCharacterStat.curHP += 10 * MJ_It.equip[7];
                            if (status.myCharacterStat.curHP >= status.myCharacterStat.maxHP) status.myCharacterStat.curHP = status.myCharacterStat.maxHP;
                            Debug.Log("10회복");
                        }

                    }
                    //크리 안터짐
                    if (rnd > MJ_It.equip[1])
                    {
                        //씨앗 없음
                        if (MJ_It.equip[7] == 0)
                        {
                            Debug.Log("안경 있고 씨앗 없음 크리 안터짐");
                            hit.transform.GetComponent<Parent>().HJSGetDamage(myBulletStat.BulletDamage);
                            Debug.Log("부모몬스터 공격시" + myBulletStat.BulletDamage + "만큼의 데미지");
                        }
                        //씨앗 있음
                        else
                        {
                            Debug.Log("안경 있고 씨앗 있음 크리 안터짐");
                            hit.transform.GetComponent<Parent>().HJSGetDamage(myBulletStat.BulletDamage);
                            Debug.Log("부모몬스터 공격시" + myBulletStat.BulletDamage + "만큼의 데미지");
                            status.myCharacterStat.curHP += 10 * MJ_It.equip[7];
                            if (status.myCharacterStat.curHP >= status.myCharacterStat.maxHP) status.myCharacterStat.curHP = status.myCharacterStat.maxHP;
                            Debug.Log("10회복");
                        }
                    }
                }
            }
        }
        ShotBullet(attackDirection, BulletName, bulletSpawnPoint, attackRange);
        GunSound(BulletName, bulletSpawnPoint);      
        //Debug.DrawRay(bulletSpawnPoint.position, attackDirection * myBulletStat.BulletRange, Color.blue);
    }
    //피격 이팩트
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
    //총소리 구현
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
    //총소리 및 섬광 실행 
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

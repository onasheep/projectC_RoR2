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
    // Start is called before the first frame update
    private void Awake()
    {
        myCharacterStat.curHP = myCharacterStat.maxHP;
    }
    void Start()
    {
        myCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
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

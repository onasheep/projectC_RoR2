using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct KJH_CharacterStat
{
    [Header("HP")]
    public float maxHP;
    public float curHP;
    public float HP_Heal;

    public int Level;  
    public float AttackDelay;
    [Header("MoveSpeed")]
    public float ApplySpeed;
    public float WalkSpeed;
    public float RunSpeed; 
    [Header("Jump")]
    public float JumpForce;
    public int JumpCount;
    public int JumpItem;
    [Header("CoolTime")]
    public float RollTime;  
    public float RMBTime;
    public float RKBTime;
    public int RKBNumber;
    [Header("LoaderDamage")]
    public float LoaderDamage;
}

[Serializable]
public struct KJH_BulletStat
{
    public float BulletRange;
    public float BulletDamage;
}

[Serializable]
public class KJH_CharacterData
{
    public bool isRun = false; 
    public bool isGround = true;
    public bool isJump = false;
    public bool isRoll = false;
    public bool isLookAround = true;
    public bool ismove = false;
    public bool isAttack = false;
    public bool onforward = false;
    public bool GunSwitch = false;
    public bool isBorder = false;
}

public struct KJH_ROTATEDATA
{
    public float Angle;
    public float Dir;
}

public class KJH_GameUtil : MonoBehaviour
{
    public static void CalAngle(Vector3 src, Vector3 des, Vector3 right, out KJH_ROTATEDATA data)
    {
        float r = Mathf.Acos(Vector3.Dot(src, des));
        data.Angle = 180.0f * (r / Mathf.PI);
        data.Dir = 1.0f;
        if (Vector3.Dot(right, des) < 0.1f)
        {
            data.Dir = -1.0f;
        }
    }
}
    


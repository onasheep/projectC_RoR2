using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct CharacterStat
{
    public float HP;
    public int Level;
    public float HP_Heal;
    public float AttackDamage;
    public float ApplySpeed;
    public float WalkSpeed;
    public float RunSpeed; 
    public float JumpForce;
    public int JumpCount;
    public int JumpItem;
    public float RollTime;   
}
[Serializable]
public class CharacterData
{
    public bool isRun = false;
    public bool isGround = true;
    public bool isRoll = false;
    public bool isLookAround = true;
    public bool ismove = false;
    public bool isAttack = false;
    public bool onforward = false;
}

public struct ROTATEDATA
{
    public float Angle;
    public float Dir;
}

public class GameUtil : MonoBehaviour
{
    public static void CalAngle(Vector3 src, Vector3 des, Vector3 right, out ROTATEDATA data)
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
    


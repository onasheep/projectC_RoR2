using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


//namespace GAMEUTILS
//{

[Serializable]

public struct CharacterStatkkj
{
    public float MoveSpeed;
    public float AttackRange;
    public float AttackDelay;
    public float HP;
    public float AP;
}
public struct ROTATEDATA
{
    public float Angle;
    public float Dir;
}

public class Gameutilkkj : MonoBehaviour
{
    public static void CalAngle(Vector3 src, Vector3 des, Vector3 right, out ROTATEDATA data)
    {
        float r = Mathf.Acos(Vector3.Dot(src, des));
        data.Angle = 180.0f * (r / Mathf.PI);
        data.Dir = 1.0f;
        if (Vector3.Dot(right, des) < 0.0f)
        {
            data.Dir = -1.0f;
        }
    }
}

public interface BattlecombatSystem
{
    
    Transform transform 
    {
        get;
    }
    
    void OnDamagekkj(float Damage);
    bool IsLivekkj();
}

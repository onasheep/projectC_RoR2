using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DumyPlayer : HJSMonster
{
    
    public HJSMonsterData PlayerData;
    void Start()
    {
        PlayerData.MaxHP = PlayerData.HP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetDamage(float Damage)
    {
        if (PlayerData.HP <= 0) return;
        PlayerData.HP -= Damage;
    }
}

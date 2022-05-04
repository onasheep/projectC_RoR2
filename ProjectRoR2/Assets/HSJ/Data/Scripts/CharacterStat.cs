using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public static CharacterStat instance;

    public int character_LV;
    public int[] needExp;
    public int CurrentExp;

    public float hp;
    public float currentHP;
    public int money;
    public float atk;
    public float def;
    



    void Start()
    {
        instance = this;
    }
    public void Hit(float _enemyAtk)
    {
        float dmg;

        if (def >= _enemyAtk)
            dmg = 1;
        else
            dmg = _enemyAtk - def;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

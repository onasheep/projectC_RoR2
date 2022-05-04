using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]
public class CharacterStat1
{
    public float HP;
    public float HPRegen;
    public float Defence;
    public float Attack;
    
}
public class GameUtill
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public interface BattleSystem
{
    void onDamage(float damage);

}

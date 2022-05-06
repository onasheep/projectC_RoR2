using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct CharacterStat
{
    
}

public interface CombatSystem
{
    void GetDamage(float Damage);
    bool IsAlive();
}

public class GameSystem : MonoBehaviour
{

}

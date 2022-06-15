using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public interface HJSCombatSystem
{
    void HJSGetDamage(float Damage, UnityAction dieAction);
    bool HJSIsAlive();
}

public class HJSGameSystem : MonoBehaviour
{

}

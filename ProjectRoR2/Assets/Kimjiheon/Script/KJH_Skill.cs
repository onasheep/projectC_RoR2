using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KJH_Skill : Character
{
    public float RollSpeed;
    public float defaultTime;
    private float RollTime;
    static KJH_Skill instance = null;
    public static KJH_Skill Ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(KJH_Skill)) as KJH_Skill;
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "KJH_Skill";
                    instance = obj.AddComponent<KJH_Skill>();
                }
            }
            return instance;
        }
    }

    public static void TryRoll(Vector3 dir, GameUtilKJH.CharacterData myCharacterdata, Rigidbody myRigid, Animator myAnim, CharacterStat myCharacterStat)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!myCharacterdata.isRoll)
            {
                if (myCharacterdata.isAttack)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        KJH_Skill.Roll("RollForward", dir, myRigid, myAnim, myCharacterdata);
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        //Roll("RollBackward");
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        // Roll("RollLeft");
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        //Roll("RollRight");
                    }
                }
                else
                {
                    KJH_Skill.Roll("RollForward", dir, myRigid, myAnim, myCharacterdata);
                }
            }           
        }
    }
    
    public static void Roll(string Name, Vector3 dir, Rigidbody myRigid, Animator myAnim, GameUtilKJH.CharacterData myCharacterdata)
    {
        myCharacterdata.ismove = false;
        myCharacterdata.isRoll = true;
        myRigid.AddForce(dir * 0.5f, ForceMode.Impulse);
        myAnim.SetTrigger("RollForward");
        Debug.Log("true");
    }
    public static void Rollout(GameUtilKJH.CharacterData myCharacterdata, CharacterStat myCharacterStat)
    {
        myCharacterdata.isRoll = false;
        myCharacterdata.ismove = true;
        Debug.Log("false");
    }


}

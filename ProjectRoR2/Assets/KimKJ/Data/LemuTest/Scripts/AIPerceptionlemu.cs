using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPerceptionlemu : MonoBehaviour
{
    public UnityAction FindTarget; //플레이어 감지용 
    public LayerMask myEnemyMask; //Layer
    public BattlecombatSystem Target; //로더 or 코만도 
    public List<GameObject> myEnemylist = new List<GameObject>(); //PlayerList

    private void OnTriggerEnter(Collider other) //들어왔을경우 
    {
        if ((myEnemyMask & (1 << other.gameObject.layer)) != 0)
        {
            myEnemylist.Add(other.gameObject);
     //     if (this.transform.position.GetComponent<Lemurian> == null)
         {
             //   this.transform.parent.GetComponent<Lemurian>().myTarget = myEnemylist[0].transform;
                Target = other.gameObject.GetComponent<BattlecombatSystem>();
                FindTarget?.Invoke(); //레무리안이 감지를 하고 공격을 하면서 계속 쫓아옴 
                Debug.Log("감지됨");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        myEnemylist.Remove(other.gameObject);
        if(this.transform.parent.GetComponent<Lemurian>().myTarget == other.gameObject.transform)
        {
            if(myEnemylist.Count == 0)
            {
            //   this.transform.GetComponent<Lemurian>().myTarget = null;
            }
            else
           
                this.transform.parent.GetComponent<Lemurian>().myTarget = myEnemylist[0].transform;
         //   Debug.Log("감지에서 벗어남");
            //나갔을 경우
        }
        
        Debug.Log("감지에서 벗어남");
    }
}
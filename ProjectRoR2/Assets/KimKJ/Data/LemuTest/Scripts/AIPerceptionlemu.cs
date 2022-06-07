using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPerceptionlemu : MonoBehaviour
{
    public UnityAction FindTarget;
    public LayerMask myEnemyMask;
    public BattleCombatSystem Target;
    public List<GameObject> myEnemylist = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if ((myEnemyMask & (1 << other.gameObject.layer)) != 0)
        {
            myEnemylist.Add(other.gameObject);
          if (this.transform.parent.GetComponent<Lemurian>().myTarget == null)
         {
                this.transform.parent.GetComponent<Lemurian>().myTarget = myEnemylist[0].transform;
                Target = other.gameObject.GetComponent<BattleCombatSystem>();
                FindTarget?.Invoke();
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
                this.transform.GetComponent<Lemurian>().myTarget = null;
            }
            else
           
                this.transform.parent.GetComponent<Lemurian>().myTarget = myEnemylist[0].transform;
           
        }
    }
}
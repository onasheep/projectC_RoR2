using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPerceptionGolem : MonoBehaviour
{
    public UnityAction FindTarget;
    public LayerMask myEnemyMask;
    public BattlecombatSystem Target;
    public List<GameObject> myEnemylist = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if ((myEnemyMask & (1 << other.gameObject.layer)) != 0)
        {
            myEnemylist.Add(other.gameObject);
            if (this.transform.parent.GetComponent<golem>().myTarget == null)
            {
                this.transform.parent.GetComponent<golem>().myTarget = myEnemylist[0].transform;
                Target = other.gameObject.GetComponent<BattlecombatSystem>();
                FindTarget?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myEnemylist.Remove(other.gameObject);
        if (this.transform.parent.GetComponent<golem>().myTarget == other.gameObject.transform)
        {
            if (myEnemylist.Count == 0)
            {
                this.transform.GetComponent<golem>().myTarget = null;
            }
            else

                this.transform.parent.GetComponent<golem>().myTarget = myEnemylist[0].transform;

        }
    }
}

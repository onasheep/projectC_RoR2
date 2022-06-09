using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPerceptionlemu : MonoBehaviour
{
    public UnityAction FindTarget; //�÷��̾� ������ 
    public LayerMask myEnemyMask; //Layer
    public BattlecombatSystem Target; //�δ� or �ڸ��� 
    public List<GameObject> myEnemylist = new List<GameObject>(); //PlayerList
    public Transform lemu = null;
    private void OnTriggerEnter(Collider other) //��������� 
    {

        if ((myEnemyMask & (1 << other.gameObject.layer)) != 0)
        {
            myEnemylist.Add(other.gameObject);
            if(GameObject.Find("Lemu").transform.GetComponent<Lemurian>().myTarget == null)
            {
                GameObject.Find("Lemu").GetComponent<Lemurian>().myTarget = myEnemylist[0].transform;
               Target = other.gameObject.GetComponent<BattlecombatSystem>();
               
                Debug.Log("������.");
                FindTarget?.Invoke();
            }
        }
    }
    private void Update()
    {
        this.transform.position = lemu.position;
    }
    private void OnTriggerExit(Collider other)
    {
        
        myEnemylist.Remove(other.gameObject);
        if(GameObject.Find("Lemu").GetComponent<Lemurian>().myTarget == other.gameObject.transform)
        {
            if(myEnemylist.Count == 0)
            {
                GameObject.Find("Lemu").GetComponent<Lemurian>().myTarget = null;
                GameObject.Find("Lemu").GetComponent<Lemurian>().mystate = (Lemurian.STATE.RUN);
            }
            else

                GameObject.Find("Lemu").GetComponent<Lemurian>().myTarget = myEnemylist[0].transform;
        }
        
        Debug.Log("�������� ���");
    }
}
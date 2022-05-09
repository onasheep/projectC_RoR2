using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Test : MonoBehaviour
{
    public float MoveSpeed=10.0f;
    public float Hp = 100.0f;
    public float Attack=10.0f;
    public float depense=10.0f;
    public float AttackSpeed=1.0f;
    public int JumpCount = 1;
    public float critical = 0;
    public float HpRecovery = 10f;
    public int[] eqiup = new int[14];
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
        }
    }
}

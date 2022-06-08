using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HJSSpawnManager : MonoBehaviour
{
    public GameObject[] Monsterlist;
    public float LilSpawnTime = 0.0f;
    public float BigSpawnTime = 0.0f;
    float LilSpawn = 10.0f;
    float BigSpawn = 20.0f;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        LilSpawnTime += Time.deltaTime;
        BigSpawnTime += Time.deltaTime;

        if (LilSpawnTime >= LilSpawn)
        {
            CreateBettleMonster();
            
            
        }

        if (BigSpawnTime >= BigSpawn)
        {
            CreateBettleMonster();


        }
    }

    IEnumerator wait()
    {
        yield return new WaitForEndOfFrame();
    }

    void CreateBettleMonster()
    {

        //Vector3 playerPosition = GameObject.Find("Loader").transform.position; 
        Vector3 playerPosition = this.gameObject.transform.position;
        float randomX = Random.Range(-10.0f, 10.0f);
        float randomZ = Random.Range(-10.0f, 10.0f);
        Vector3 SpPos = new Vector3(playerPosition.x + randomX, playerPosition.y, playerPosition.z + randomZ);
        Ray ray = new Ray(SpPos, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, LayerMask.NameToLayer("Ground")))
        { 
            Instantiate(Monsterlist[0], SpPos, Quaternion.identity);
            LilSpawnTime = 0.0f;
        }
        else
        {
            
        }

        

    }

    void CreateParnetMonster()
    {

        //Vector3 playerPosition = GameObject.Find("Loader").transform.position; 
        Vector3 playerPosition = this.gameObject.transform.position;
        float randomX = Random.Range(-10.0f, 10.0f);
        float randomZ = Random.Range(-10.0f, 10.0f);
        Vector3 SpPos = new Vector3(playerPosition.x + randomX, playerPosition.y, playerPosition.z + randomZ);
        Ray ray = new Ray(SpPos, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, LayerMask.NameToLayer("Ground")))
        {
            Instantiate(Monsterlist[1], SpPos, Quaternion.identity);
            BigSpawnTime = 0.0f;
        }
        else
        {

        }



    }
    /*void CreateMonster(int Monselect)
    {

        //Vector3 playerPosition = GameObject.Find("Loader").transform.position; 
        Vector3 playerPosition = this.gameObject.transform.position;
        float randomX = Random.Range(-8.0f, 8.0f);
        float randomZ = Random.Range(-8.0f, 8.0f);
        Vector3 SpPos = new Vector3(playerPosition.x + randomX, playerPosition.y, playerPosition.z + randomZ);
      
        //    Monsterlist.Add(Instantiate(Monster, new Vector3(playerPosition.x + randomX, playerPosition.y, playerPosition.z + randomZ), Quaternion.identity));
        GameObject enemy = Instantiate(Monsterlist[1], SpPos, Quaternion.identity);
        
    }*/
}

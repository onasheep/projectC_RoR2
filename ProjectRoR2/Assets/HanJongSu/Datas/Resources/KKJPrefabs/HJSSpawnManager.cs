using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HJSSpawnManager : MonoBehaviour
{
    public GameObject[] Monsterlist;
    public LayerMask MonsterLayer;
    public float LilSpawnTime = 0.0f;
    public float BigSpawnTime = 0.0f;
    float LilSpawn = 20.0f;
    float BigSpawn = 40.0f;
    void Start()
    {
        LilSpawnTime = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        LilSpawnTime += Time.deltaTime;
        BigSpawnTime += Time.deltaTime;

        if (LilSpawnTime >= LilSpawn)
        {

            CreateMonster(Random.Range(0, 2));


        }

        if (BigSpawnTime >= BigSpawn)
        {
            CreateMonster(Random.Range(2, 4), true);
        }
    }

    void CreateMonster(int num, bool IsBig = false)
    {
        Vector3 playerPosition = this.gameObject.transform.parent.position;
        float randomX = Random.Range(-10.0f, 10.0f);
        float randomZ = Random.Range(-10.0f, 10.0f);
        Vector3 SpPos = new Vector3(playerPosition.x + randomX, playerPosition.y + 1.0f, playerPosition.z + randomZ);
        //Ray GroundRay = new Ray(SpPos, Vector3.down);
        Ray MonsterRay = new Ray(SpPos + new Vector3(0.0f,10.0f,0.0f), Vector3.down);
        RaycastHit donthit = new RaycastHit();
        if (Physics.Raycast(MonsterRay, out RaycastHit hit, 1000.0f, 1 << LayerMask.NameToLayer("Ground")))     
        {
            if (Physics.Raycast(MonsterRay, out donthit, 100.0f, 1 << LayerMask.NameToLayer("Player")))
            {
                Debug.Log("failed - player");
            }
            else if (Physics.Raycast(MonsterRay, out donthit, 100.0f, 1 << MonsterLayer))
            {
                Debug.Log("failed - monster");
            }
            else
            {
                Instantiate(Monsterlist[num], hit.point, Quaternion.identity);
                if (IsBig)
                {
                    BigSpawnTime = 0.0f;
                }
                else
                {
                    LilSpawnTime = 0.0f;
                    if (Random.Range(0, 10) < 3)
                    {
                        BigSpawnTime += 4.0f;
                    }
                    else if (Random.Range(0, 10) < 5.0f)
                    {
                        LilSpawnTime += 5.0f;
                    }
                }
            }
        }
        else
        {
            Debug.Log("SPAWN RE TRY");
        }
    }

    /*void CreateParnetMonster()
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



    }*/
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

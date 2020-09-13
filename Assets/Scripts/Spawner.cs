using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Manager;
    public GameObject spawnTopLeft;
    public GameObject spawnBottomRight;
    // private GameObject selectedPoint;
    // private int index;

    public GameObject[] enemies;
    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;
    public bool canSpawn = true;
    public float spawnTimer;
    public int enemyCount;
 
    private int lower = 1;
    private int upper = 5;

    private void Start() 
    {
        Manager = GameObject.Find("GameManager");
        Invoke("Spawn", .5f);
    }

    void Spawn() 
    {
        // getting location of spawn
        print("OhImEn");
        float x = Random.Range(spawnTopLeft.transform.position.x, spawnBottomRight.transform.position.x);
        float y = Random.Range(spawnTopLeft.transform.position.y, spawnBottomRight.transform.position.y);
        // index = Random.Range(0, spawnPoints.Length);
        // selectedPoint = spawnPoints[index];
        float timeBetweenSpawns = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);

        if (canSpawn && enemyCount < 1000)
        {
            GameObject spawnedEnemy = Instantiate(enemies[Random.Range(0,enemies.Length)], new Vector3(x, y, 0), Quaternion.identity);
            spawnedEnemy.GetComponent<Enemy>().health = Random.Range(lower, upper);
            enemyCount++;
        }

        if (enemyCount > 500)
        {
            lower = 75;
            upper = 100;
        } 
        else if (enemyCount > 200)
        {
            lower = 30;
            upper = 50;
        } 
        else if (enemyCount > 75)
        {
            lower = 10;
            upper = 20;
        }
    
        Invoke("Spawn", timeBetweenSpawns);
        
    }

}

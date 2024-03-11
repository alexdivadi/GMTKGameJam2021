using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{

    public float spawnRadius = 20;
    public int bossSpawnThreshold = 10;
    public int numberOfEnemies = 20;
    public int[] objectSpawnRates;
    public int[] enemySpawnRates;

    private string[] objectPrefabList = { "Triangle", "Trapezoid", "Spike" };
    private string[] enemyPrefabList = { "SimpleEnemy", "MortarEnemy", "SpikeEnemy", "HeavyTurret", "LightningEnemy" };
    private string[] bossPrefabList = { "BlasterBoss", "MortarBoss", "LightningBoss" };
    private int deathCounter;
    private GameObject enemy;
    private string enemyPrefab;
    /*private List<GameObject> enemiesActive;
    private List<GameObject> bossesActive;*/

    // Start is called before the first frame update
    void Start()
    {
        /*enemiesActive = new List<GameObject>();
        bossesActive = new List<GameObject>();*/
        deathCounter = 0;

        for(int i = 0; i < enemySpawnRates.Length - 1; i++) {
            enemySpawnRates[i + 1] += enemySpawnRates[i];
        }

        for(int i = 0; i < objectSpawnRates.Length - 1; i++) {
            objectSpawnRates[i + 1] += objectSpawnRates[i];
        }

        for (int i = 0; i < numberOfEnemies; i++)
        {
            spawnEnemy();
            
            if (i % 2 == 0)
                spawnObject();
        }
        
        //spawnBoss();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnemyDestroyed()
    {
        UnityEngine.Debug.Log("Enemy Destroyed");
        deathCounter ++;
        /*enemiesActive.RemoveAll(x => !x);
        bossesActive.RemoveAll(x => !x);*/
        spawnEnemy();

        if (deathCounter >= bossSpawnThreshold)
        {
            spawnBoss();
            deathCounter -= bossSpawnThreshold;

        }
    }

    void spawnObject()
    {
        int objectIndex = 0;
        int randomNum = UnityEngine.Random.Range(0, 100);

        for (int i = 0; i < objectSpawnRates.Length; i++) {
            if (randomNum > objectSpawnRates[i]) {
                objectIndex++;
            } else {
                break;
            }
        }

        if (objectIndex >= objectSpawnRates.Length) {
            Debug.Log("Big Problemo");
        }

        enemyPrefab = objectPrefabList[objectIndex];
        enemy = Resources.Load("Prefabs/" + enemyPrefab) as GameObject;
        Instantiate(enemy, generateRandomSpawn(), generateRandomRotation());
    }

    void spawnEnemy()
    {
        int enemyIndex = 0;
        int randomNum = UnityEngine.Random.Range(0, 100);

        for (int i = 0; i < enemySpawnRates.Length; i++) {
            if (randomNum > enemySpawnRates[i]) {
                enemyIndex++;
            } else {
                break;
            }
        }

        if (enemyIndex >= enemySpawnRates.Length) {
            Debug.Log("Big Problemo");
        }

        enemyPrefab = enemyPrefabList[enemyIndex];
        enemy = Resources.Load("Prefabs/"+enemyPrefab) as GameObject;
        /*enemiesActive.Add(*/Instantiate(enemy, generateRandomSpawn(), generateRandomRotation())/*) as GameObject*/;
    }

    void spawnBoss()
    {
        enemyPrefab = bossPrefabList[UnityEngine.Random.Range(0, bossPrefabList.Length)];
        enemy = Resources.Load("Prefabs/" + enemyPrefab) as GameObject;
        /*bossesActive.Add(*/Instantiate(enemy, generateRandomSpawn(), generateRandomRotation())/*) as GameObject)*/;
        UnityEngine.Debug.Log("Boss Spawned!");
    }

    Vector2 generateRandomSpawn()
    {
        Vector2 position;
        do
        {
           position = UnityEngine.Random.insideUnitCircle * spawnRadius + (Vector2)gameObject.transform.position;
        } while (position.x <= 0 || position.y <= 0 || position.x >= 44 || position.y >= 61);
        return position;
    }

    Quaternion generateRandomRotation()
    {
        var position = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 180), Vector3.forward);
        return position;
    }


}

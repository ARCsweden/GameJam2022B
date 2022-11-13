using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomEnemySpawner : MonoBehaviour
{

    
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject bigBoss;
    [SerializeField]
    public Transform target;

    public int MaxEnemies = 5;
    public int max = 100;
    public int spawnIncrement = 10;
    public float spawnRate = 15.0f;
    private int EnemiesSpawned = 0;

    [SerializeField]
    private float spawnInterval = 3.5f;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
        InvokeRepeating("IncreaseEnemyCounter", 10f, spawnRate);
        InvokeRepeating("spawnBoss", 30f, 60f);
    }

    void spawnBoss()
    {

        float height = 2 * cam.orthographicSize;
        float width = height * cam.aspect;

        Vector3 spawnDir = new Vector3(UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f, 0.0f);
        spawnDir.Normalize();
        spawnDir.x *= cam.aspect;
        Vector3 spawnOffset = spawnDir * height;
        Vector3 camPos = target.position + spawnOffset;

        GameObject newEnemy = Instantiate(bigBoss, camPos, Quaternion.identity);
    }

    void IncreaseEnemyCounter()
    {
        MaxEnemies = Math.Min(MaxEnemies + spawnIncrement, max);
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        float height = 2 * cam.orthographicSize;
        float width = height * cam.aspect;

        yield return new WaitForSeconds(interval);
        if(EnemiesSpawned < MaxEnemies) 
        {
            Vector3 spawnDir = new Vector3(UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f, 0.0f);
            spawnDir.Normalize();
            spawnDir.x *= cam.aspect;
            Vector3 spawnOffset = spawnDir * height;
            Vector3 camPos = target.position + spawnOffset;

            GameObject newEnemy = Instantiate(enemy, camPos, Quaternion.identity);
            newEnemy.GetComponentInChildren<EnemyHitbox>().randomEnemySpawner = this;
            EnemiesSpawned++;
        }
        
        StartCoroutine(spawnEnemy(interval, enemy));
    }
    public void AddKill()
    {
        EnemiesSpawned--;
    }
}


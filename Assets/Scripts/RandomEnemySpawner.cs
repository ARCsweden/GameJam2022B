using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{

    
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    public Transform target;

    public int MaxEnemies = 5;
    private int EnemiesSpawned = 0;

    [SerializeField]
    private float spawnInterval = 3.5f;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
        InvokeRepeating("IncreaseEnemyCounter", 10f, 15f);
    }

    void IncreaseEnemyCounter()
    {
        MaxEnemies = MaxEnemies + 10;
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        float height = 2 * cam.orthographicSize;
        float width = height * cam.aspect;

        yield return new WaitForSeconds(interval);
        if(EnemiesSpawned < MaxEnemies) 
        {
            Vector3 spawnOffset = new Vector3(Random.Range(-width, width), Random.Range(-height, height), 0.0f);
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


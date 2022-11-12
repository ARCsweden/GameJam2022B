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
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        yield return new WaitForSeconds(interval);
        if(EnemiesSpawned < MaxEnemies) 
        {
            GameObject newEnemy = Instantiate(enemy, new Vector3(cam.transform.position.x + Random.Range(-width, width), cam.transform.position.y + Random.Range(-height, height), target.position.z), Quaternion.identity);
        }
        EnemiesSpawned++;
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
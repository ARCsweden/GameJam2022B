using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHitbox : MonoBehaviour
{
    public EnemyConfig enemyConfig;
    public RandomEnemySpawner randomEnemySpawner;
    private int Dead = 0;
    private GameObject cam;
    Vector3 viewPos;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        viewPos = cam.GetComponent<Camera>().WorldToViewportPoint(enemyConfig.transform.position);
        
        //OnBecameInvisible();
    }
    
    public void Hit(){
        enemyConfig.retreat = true;
        randomEnemySpawner.GetComponent<RandomEnemySpawner>().AddKill();
        Dead = 1;
        //GetComponent<Collider2D>().enabled = false;
    }

    void OnBecameInvisible()
    {
        if (Dead == 1)
        {
            Destroy(this.gameObject);
        }
    }

}

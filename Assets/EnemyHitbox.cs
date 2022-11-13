using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHitbox : MonoBehaviour
{
    public EnemyConfig enemyConfig;
    public RandomEnemySpawner randomEnemySpawner;

    void Start()
    {
        
    }
    
    public void Hit(){
        enemyConfig.retreat = true;
        randomEnemySpawner.GetComponent<RandomEnemySpawner>().AddKill();
        //GetComponent<Collider2D>().enabled = false;
    }
}

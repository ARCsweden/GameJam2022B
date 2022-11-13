using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public EnemyConfig enemyConfig;
    public void Hit(){
        enemyConfig.retreat = true;
        //GetComponent<Collider2D>().enabled = false;
    }
}

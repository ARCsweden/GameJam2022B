using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : MonoBehaviour
{
    // Start is called before the first frame update
    public FrenchBoss boss;
    public void Hit(){
        boss.retreat = true;
    }
}

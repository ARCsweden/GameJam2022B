using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLooseWeapon : MonoBehaviour
{

    public Animator anim;
    public Collider2D col;
    void Update(){
        if(anim.GetFloat("Variation") < 0.55){
            col.enabled = false;
        }
    }
    // Start is called before the first frame update
    void Hit(){
        
    }
}

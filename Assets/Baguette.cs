using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baguette : MonoBehaviour
{

    public GameObject target;

    public void Update(){

        //Vector3 towards = target.transform.position - transform.position;
        
    }
    
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player")
            col.gameObject.SendMessage("BauguetteHit");
    }
    

}

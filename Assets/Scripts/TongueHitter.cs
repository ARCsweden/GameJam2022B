using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class TongueHitter : MonoBehaviour
{
    public bool penetratable = false;
    public FrogAim frogAim;
    private void Start(){
        tCollider = GetComponent<Collider2D>();
    }
    public void Update(){
        if(frogAim.tongueState == FrogAim.TongueState.Launching){
            tCollider.enabled = true;
        }
        if(frogAim.tongueState == FrogAim.TongueState.Retracting || frogAim.tongueState == FrogAim.TongueState.Cooldown){
            tCollider.enabled = false;
        }
    }
    private Collider2D tCollider;
    public void OnTriggerEnter2D(Collider2D col){
        if(!penetratable || col.tag == "NonPenetrable"){
            frogAim.tongueState = FrogAim.TongueState.Retracting;
            tCollider.enabled = false;
        }
        if(col.tag == "Edible" || col.tag == "Weapon"){
            col.transform.SetParent(null,true);
            frogAim.pickUp = col.gameObject;
            return;
        }
        col.SendMessage("Hit");        
    }
}

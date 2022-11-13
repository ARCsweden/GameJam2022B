using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class TongueHitter : MonoBehaviour
{
    public bool penetratable = false;
    public FrogAim frogAim;
    public float powerUpTongueLength = 0.2f;
    public float powerUpCooldown = 0.2f;
    public int powerUpPenetrationAmount = 3;

    private int beesEaten = 0;
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
        float variation = 0;
        if(!penetratable || col.tag == "NonPenetrable"){
            frogAim.tongueState = FrogAim.TongueState.Retracting;
            tCollider.enabled = false;
        }
        if(col.tag == "Edible" || col.tag == "Weapon"){
            if(frogAim.pickUp == null){
                col.transform.SetParent(null,true);
                frogAim.pickUp = col.gameObject;
            }
        }
        if(col.tag == "Edible"){
            variation = col.GetComponent<Variation>().variation;
            if(variation < 0.75){
                Debug.Log("Eat Fly");
                frogAim.maxTongueLength += powerUpTongueLength;
            }
            else if(variation < 0.9){
                Debug.Log("Eat firefly");
                frogAim.tongueCooldown = Mathf.Clamp(frogAim.tongueCooldown - powerUpCooldown,0,float.MaxValue);

            }else{
                Debug.Log("Eat Bee");
                beesEaten ++;
                if(beesEaten >= powerUpPenetrationAmount){
                    penetratable = true;
                }
            }
        }
        if(col.tag == "Weapon"){
            variation = col.GetComponent<Animator>().GetFloat("Variation");

        }
        if(col.gameObject.layer == 6){
            col.SendMessage("Hit");        
        }
    }
}

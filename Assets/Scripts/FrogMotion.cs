using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FrogMotion : MonoBehaviour
{
    public float moveSpeed = 3;
    private bool canJump = true;
    private float jumpTime = 0;
    private Rigidbody2D rig;
    public Animator animator;
    public FrogAim fA;
    void Start(){
        rig = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && canJump){
            canJump = false;
            jumpTime = 1;
        }

        if(jumpTime == 0){
            Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);
            if (movementVector.magnitude > 1){
                movementVector = movementVector.normalized;
            }
            rig.velocity = new Vector2(movementVector.x*1.5f,movementVector.y);
        }

        transform.position = transform.position + Vector3.forward * (transform.position.y-transform.position.z);
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Enemy"){
            animator.SetBool("Death",true);
            fA.enabled = false;
            this.enabled = false;
            GetComponentInChildren<LineRenderer>().enabled = false;

        }
    }
    void BauguetteHit(){
            animator.SetBool("Death",true);
            fA.enabled = false;
            this.enabled = false;
            GetComponentInChildren<LineRenderer>().enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenchBoss : MonoBehaviour
{
public float speed = 0.5f;
    private GameObject target;
    public GameObject spriteHolder;
    public bool retreat = false;
    public float distance = 4;
    public Animator headAnim;
    public float cooldownMax = 3;
    public float cooldown = 0;
    public GameObject baguettePref;
    public float baguetteForce = 2;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = new Vector2(target.transform.position.x, target.transform.position.y);
        bool inRange = ((transform.position - target.transform.position).magnitude < distance);
        if(!inRange || retreat){
            transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime * (retreat ? -1.5f : 1));
            transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.y);
        }
        else{
            Vector3 vect = transform.position - target.transform.position;
            Vector3 vectFlat = new Vector3(vect.x,vect.y,0);
            Vector3 movement = Quaternion.Euler(0, 0, 90) * vectFlat;
            transform.position = transform.position + movement.normalized * speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.y);
        }
        if(target.transform.position.x < transform.position.x){
            spriteHolder.transform.localScale = new Vector3(-1* (retreat ? -1 : 1),1,1);
        }else{
            spriteHolder.transform.localScale = new Vector3(1* (retreat ? -1 : 1),1,1);
        }
        //headAnim.SetBool("Retreat",retreat);

        if(inRange && cooldown <= 0 && !retreat){
            cooldown = cooldownMax;
            StartCoroutine(ShootSequence());
                    Debug.Log("Shoot");

        }
        else{
            cooldown -= Time.deltaTime;
        }
        
    }
    IEnumerator  ShootSequence(){
        Debug.Log("Shot");
        for(int i = 0; i < 3; i++){
            Vector3 towards = (target.transform.position-spawnPoint.position).normalized;
            Vector3 towardsFlat = new Vector3(towards.x,towards.y,0);
            GameObject baguette = Instantiate(baguettePref,spawnPoint.position,transform.rotation);
            Quaternion quat = Quaternion.Euler(0,0,Mathf.Atan2(towardsFlat.y,towardsFlat.x) * 180 / Mathf.PI);
            baguette.transform.rotation = quat;
            baguette.GetComponent<Rigidbody2D>().AddForce((target.transform.position-spawnPoint.position).normalized*baguetteForce,ForceMode2D.Impulse);
            baguette.GetComponent<Baguette>().target = target;
            yield return new WaitForSeconds(0.5f);
        }
    }
}

    
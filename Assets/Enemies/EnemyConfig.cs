using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConfig : MonoBehaviour
{

    public float speed = 0.5f;
    private GameObject target;
    public GameObject spriteHolder;
    public bool retreat = false;
    public Animator headAnim;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = new Vector2(target.transform.position.x, target.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime * (retreat ? -1.5f : 1));
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.y);
        if(target.transform.position.x < transform.position.x){
            spriteHolder.transform.localScale = new Vector3(-1* (retreat ? -1 : 1),1,1);
        }else{
            spriteHolder.transform.localScale = new Vector3(1* (retreat ? -1 : 1),1,1);
        }
        headAnim.SetBool("Retreat",retreat);
    }
    
}

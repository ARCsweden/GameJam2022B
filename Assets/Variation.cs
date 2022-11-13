using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variation : MonoBehaviour
{
    public Animator anim;
    public float variation;
    // Start is called before the first frame update
    void Start()
    {
        variation = Random.value;
        anim.SetFloat("Variation",variation);
    }

}

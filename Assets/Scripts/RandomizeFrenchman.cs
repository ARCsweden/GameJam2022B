using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeFrenchman : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator[] animators;

    // Update is called once per frame
    void Start()
    {
        for(int i = 0; i < animators.Length; i++){
            float rand = Random.value;
            animators[i].SetFloat("Variation",rand);
            //Debug.Log(rand);
        }
    }
}

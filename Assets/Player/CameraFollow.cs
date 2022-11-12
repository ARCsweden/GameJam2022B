using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    //public Transform target;
    private GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.transform.position.x, target.transform.position.y, 10f);
        transform.position = Vector3.Slerp(transform.position, newPos, Time.deltaTime);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTextTimeout : MonoBehaviour
{
    public float timeout = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeout);
    }
}

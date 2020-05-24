using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAlt : MonoBehaviour
{
    private Color tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

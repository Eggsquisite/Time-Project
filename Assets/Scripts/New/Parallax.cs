using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] GameObject mainCam;
    [SerializeField] float parallaxEffect;

    private float length, startPos;
    

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x - 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float tmp = (mainCam.transform.position.x * (1 - parallaxEffect));
        float dist = (mainCam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.y);
        if (tmp > startPos + length)
        {
            startPos += length;
        }
        else if (tmp < startPos - length)
        {
            startPos -= length;
        }
    }
}

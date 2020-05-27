using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlicker : MonoBehaviour
{
    [SerializeField] float flickerInterval = 0.4f;

    private Text text;
    private float baseInterval;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        baseInterval = flickerInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (flickerInterval > 0)
        {
            flickerInterval -= Time.deltaTime;
        }
        else if (flickerInterval <= 0)
        {
            flickerInterval = baseInterval;
            text.enabled = !text.enabled;
        }
    }
}

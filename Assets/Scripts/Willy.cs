using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Willy : MonoBehaviour
{

    [SerializeField] GameObject stopPortal = null;
    [SerializeField] float stopTime = 2f;

    private GameObject tmp;
    private Collider2D coll;
    private bool stopStart;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopStart)
        {
            if (stopTime > 0)
                stopTime -= Time.deltaTime;
            else if (stopTime <= 0)
            {
                stopTime = 0;
                stopStart = false;
                coll.enabled = true;
                stopPortal.GetComponent<Animator>().SetBool("close", true);
            }
        }
    }

    private void OnMouseDown()
    {
        if (stopTime > 0)
        {
            stopStart = true;
            coll.enabled = false;
            stopPortal.SetActive(true);
            //tmp = Instantiate(stopPortal, gameObject.transform);
        }
    }
}

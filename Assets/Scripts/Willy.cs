using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Willy : MonoBehaviour
{

    //[SerializeField] GameObject stopPortal = null;
    [SerializeField] float stopTime = 2f;

    private GameObject tmp;
    private CivMovement civMovement;
    private Collider2D coll;
    private bool stopStart;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        civMovement = GetComponent<CivMovement>();
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
                civMovement.ResumeMovement();
                //coll.enabled = true;
                //stopPortal.GetComponent<Animator>().SetBool("close", true);
            }
        }
    }

    private void OnMouseDown()
    {
        if (stopTime > 0)
        {
            stopStart = true;
            civMovement.StopMovement();
            //tmp = Instantiate(stopPortal, gameObject.transform);
        }
    }
}

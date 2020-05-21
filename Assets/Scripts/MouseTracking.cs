using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracking : MonoBehaviour
{
    [SerializeField] GameObject target;

    bool ready = false;
    bool placed = false;
    Color tmp;

    // Start is called before the first frame update
    void Start()
    {
        // Set circle transparent first
        tmp = target.GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        target.GetComponent<SpriteRenderer>().color = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        if (!placed)
            target.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        if (Input.GetButtonDown("Fire2") && !ready && !placed)
        {
            ready = true;
            tmp.a = 0.3f;
            target.GetComponent<SpriteRenderer>().color = tmp;
        }
        else if (Input.GetButtonDown("Fire2") && ready && !placed)
        {
            ready = false;
            tmp.a = 0f;
            target.GetComponent<SpriteRenderer>().color = tmp;
        }

        if (Input.GetButtonDown("Fire1") && ready)
        {
            var tmp = target.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            target.GetComponent<SpriteRenderer>().color = tmp;

            target.GetComponent<SpeedUp>().SetReady(true);
            placed = true;
        }
    }
}

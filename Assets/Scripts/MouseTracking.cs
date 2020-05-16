using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracking : MonoBehaviour
{
    [SerializeField] GameObject target;

    bool placed = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set circle transparent first
        var tmp = target.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.3f;
        target.GetComponent<SpriteRenderer>().color = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        if (!placed)
            target.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        if (Input.GetButtonDown("Fire1"))
        {
            var tmp = target.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            target.GetComponent<SpriteRenderer>().color = tmp;

            target.GetComponent<SpeedUp>().SetReady(true);
            placed = true;
        }
    }
}

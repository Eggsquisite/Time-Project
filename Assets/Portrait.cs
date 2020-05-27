using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portrait : MonoBehaviour
{
    [SerializeField] Transform civ;
    [SerializeField] float moveSpeed = 10f;

    Transform t;
    private bool findTarget, followTarget;

    // Start is called before the first frame update
    void Start()
    {
        t = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A)         || 
            Input.GetKeyDown(KeyCode.LeftArrow) || 
            Input.GetKeyDown(KeyCode.D)         || 
            Input.GetKeyDown(KeyCode.RightArrow))
        {
            findTarget = false;
            followTarget = false;
        }

        if (findTarget && civ.position.x < t.position.x + 0.5 && civ.position.x > t.position.x - 0.5)
        {
            findTarget = false;
            followTarget = true;
        }
        else if (findTarget && civ.position.x >= t.position.x)
        {
            t.position = new Vector3(t.position.x + moveSpeed * Time.deltaTime, t.position.y, t.position.z);
        }
        else if (findTarget && civ.position.x < t.position.x)
        {
            t.position = new Vector3(t.position.x - moveSpeed * Time.deltaTime, t.position.y, t.position.z);
        }

        if (followTarget)
        {
            t.position = new Vector3(civ.position.x, t.position.y, t.position.z);
        }
    }

    public void FindTarget()
    {
        if (civ != null)
            findTarget = true;
    }
}
